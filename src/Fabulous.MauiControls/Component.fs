namespace Fabulous.Maui

open System
open System.ComponentModel
open System.Runtime.CompilerServices
open Fabulous
open System.Collections.Generic
open Microsoft.Maui.Controls

////////////// CONTEXT //////////////
[<AllowNullLiteral>]
type Context() =
    let values = Dictionary<int, obj>()
    let mutable _current = -1
    
    let renderNeeded = Event<unit>()
    member this.RenderNeeded = renderNeeded.Publish
    member this.NeedsRender() = renderNeeded.Trigger()
    
    member this.MoveNext() =
        _current <- _current + 1
        _current
        
    member this.Current
        with get () =
            match values.TryGetValue(_current) with
            | false, _ -> ValueNone
            | true, value -> ValueSome value
    
    member this.SetCurrentValue(value: 'T) =
        values[_current] <- value
        
    member this.SetValue(key: int, value: 'T) =
        values[key] <- value
        this.NeedsRender()
        
    member this.AfterRender() =
        _current <- -1
        


////////////// ViewBuilder //////////////
type ComponentBody = delegate of Context -> Widget
type ComponentBodyBuilder<'msg, 'marker> = delegate of Context -> WidgetBuilder<'msg, 'marker>

type ViewBuilder() =        
    member inline this.Yield(widget: WidgetBuilder<'msg, 'marker>) =
        ComponentBodyBuilder(fun ctx -> widget)
        
    member inline this.Combine([<InlineIfLambda>] a: ComponentBodyBuilder<'msg, 'marker>, [<InlineIfLambda>] b: ComponentBodyBuilder<'msg, 'marker>) =
        ComponentBodyBuilder(fun ctx ->
            let _ = a.Invoke(ctx) // discard the previous widget in the chain
            let result = b.Invoke(ctx)
            result
        )
        
    member inline this.Delay([<InlineIfLambda>] fn: unit -> ComponentBodyBuilder<'msg, 'marker>) =
        ComponentBodyBuilder(fun ctx ->
            let sub = fn()
            sub.Invoke(ctx)
        )
        
    member inline this.Run([<InlineIfLambda>] result: ComponentBodyBuilder<'msg, 'marker>) =
        result

[<AutoOpen>]
module ViewBuilder =
    let view = ViewBuilder()
    
    
////////////// Component holder //////////////
type Component() as this =
    inherit Border()
    
    let mutable _widget = Unchecked.defaultof<_>
    let mutable _contextSubscription = Unchecked.defaultof<_>
    let mutable _body: ComponentBody = Unchecked.defaultof<_>
    
    do this.Loaded.Add(this.OnLoaded)
    do this.Unloaded.Add(this.OnUnloaded)
    
    member val Context: Context = null with get, set
    member this.Body
        with get () = _body
        and set (value) =
            _body <- value
            this.Initialize()
    
    member this.Initialize() =
        if this.Context = null then
            this.Context <- Context()
            
        this.CreateView()
        
    member this.CreateView() =
        let widget = this.Body.Invoke(this.Context)
        this.Context.AfterRender()
        
        let treeContext =
            { CanReuseView = MauiViewHelpers.canReuseView
              GetViewNode = ViewNode.get
              Logger = MauiViewHelpers.defaultLogger()
              Dispatch = ignore }

        let definition = WidgetDefinitionStore.get widget.Key

        let struct (_node, root) = definition.CreateView(widget, treeContext, ValueNone)
        
        this.Content <- root :?> View
        _widget <- widget
    
    member this.Render() =
        let prevWidget = _widget
        _widget <- this.Body.Invoke(this.Context)
        this.Context.AfterRender()
        
        let viewNode = ViewNode.get this.Content
        
        Reconciler.update MauiViewHelpers.canReuseView (ValueSome prevWidget) _widget viewNode
        
    member this.OnLoaded(_) =
        _contextSubscription <- this.Context.RenderNeeded.Subscribe(this.Render)
        
    member this.OnUnloaded(_) =
        if _contextSubscription <> null then
            _contextSubscription.Dispose()
            _contextSubscription <- null
        
    
////////////// Component widget //////////////
type IFabComponent = inherit IFabView

module Component =
    let WidgetKey = Widgets.register<Component>()
    
    let Body = Attributes.defineSimpleScalar "Component_Body" ScalarAttributeComparers.noCompare (fun _ currOpt node ->
        let target = node.Target :?> Component
        match currOpt with
        | ValueNone -> target.Body <- Unchecked.defaultof<_>
        | ValueSome body -> target.Body <- body
    )
    
[<AutoOpen>]
module ComponentBuilders =
    type Fabulous.Maui.View with
        static member inline Component<'msg, 'marker>([<InlineIfLambda>] body: ComponentBodyBuilder<'msg, 'marker>) =
            let compiledBody = ComponentBody(fun ctx ->
                let widgetBuilder = body.Invoke(ctx)
                widgetBuilder.Compile()
            )
            
            WidgetBuilder<'msg, IFabComponent>(
                Component.WidgetKey,
                Component.Body.WithValue(compiledBody)
            )
            
////////////// State //////////////

type StateRequest<'T> = delegate of unit -> 'T

/// DESIGN: State<'T> is meant to be very short lived.
/// It is created on Bind (let!) and destroyed at the end of a single ViewBuilder CE execution.
/// Due to its nature, it is very likely it will be captured by a closure and allocated to the memory heap when it's not needed.
/// e.g. Button("Increment", fun () -> state.Set(state.Current + 1))
///
/// The Set method is therefore marked inlinable to avoid creating a closure capturing State<'T>
/// The compiler will rewrite the lambda as follow:
/// Button("Increment", fun () -> ctx.SetValue(key, current + 1))
///
/// One constraint of inlining is to have all used fields public: Context, Key, Current
/// But we don't wish to expose the Context and Key fields to the user, so we mark them as EditorBrowsable.Never
type [<Struct>] State<'T>=
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    val public Context: Context
    
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    val public Key: int
    
    val public Current: 'T
    
    new (ctx, key, value) = { Context = ctx; Key = key; Current = value }
    
    member inline this.Set(value: 'T) =
        this.Context.SetValue(this.Key, value)

[<Extension>]
type StateExtensions =
    [<Extension>]
    static member inline Bind(_: ViewBuilder, [<InlineIfLambda>] fn: StateRequest<'T>, [<InlineIfLambda>] continuation: State<'T> -> ComponentBodyBuilder<'msg, 'marker>) =
        ComponentBodyBuilder(fun ctx ->
            let key = ctx.MoveNext()
            
            let value =
                match ctx.Current with
                | ValueSome value -> unbox<'T> value
                | ValueNone ->
                    let value = fn.Invoke()
                    ctx.SetCurrentValue(value)
                    value
                    
            let state = State(ctx, key, value)
            (continuation state).Invoke(ctx)
        )
        
[<AutoOpen>]
module StateHelpers =
    let inline state value = StateRequest(fun () -> value)