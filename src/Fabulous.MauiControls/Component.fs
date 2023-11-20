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
    
    // NOTE: Using Context and Body as properties is quite brittle as they need to be set in the right order.
    member val Context: Context = null with get, set
    member this.Body
        with get () = _body
        and set (value) = _body <- value
    
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
        this.Initialize()
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
    
    let Context = Attributes.defineSimpleScalar "Component_Context" ScalarAttributeComparers.equalityCompare (fun _ currOpt node ->
        let target = node.Target :?> Component
        match currOpt with
        | ValueNone -> target.Context <- Context()
        | ValueSome context -> target.Context <- context
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
            
        static member inline Component<'msg, 'marker>([<InlineIfLambda>] body: ComponentBodyBuilder<'msg, 'marker>, context: Context) =
            let compiledBody = ComponentBody(fun ctx ->
                let widgetBuilder = body.Invoke(ctx)
                widgetBuilder.Compile()
            )
            
            WidgetBuilder<'msg, IFabComponent>(
                Component.WidgetKey,
                Component.Body.WithValue(compiledBody),
                Component.Context.WithValue(context)
            )
            
////////////// State //////////////

type StateRequest<'T> = delegate of unit -> 'T

/// DESIGN: State<'T> is meant to be very short lived.
/// It is created on Bind (let!) and destroyed at the end of a single ViewBuilder CE execution.
/// Due to its nature, it is very likely it will be captured by a closure and allocated to the memory heap when it's not needed.
///
/// e.g.
///
/// Button("Increment", fun () -> state.Set(state.Current + 1))
///
/// will become
/// 
/// class Closure {
///     public State<int> state; // Storing a struct on a class will allocate it on the heap
///
///     public void Invoke() {
///         state.Set(state.Current + 1);
///     }
/// }
///
/// class Program {
///    public void View()
///    {
///       var state = new State<int>(...);
///
///       // This will allocate both the closure and the state on the heap
///       // which the GC will have to clean up later
///       var closure = new Closure(state = state);
///
///       return Button("Increment", closure);
///    }
/// }
/// 
/// 
/// The Set method is therefore marked inlinable to avoid creating a closure capturing State<'T>
/// Instead the closure will only capture Context (already a reference type), Key (int) and Current (can be consider to be obj).
/// The compiler will rewrite the lambda as follow:
/// Button("Increment", fun () -> ctx.SetValue(key, current + 1))
///
/// State<'T> is no longer involved in the closure and will be kept on the stack.
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
        ComponentBodyBuilder<'msg, 'marker>(fun ctx ->
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
    
    
////////////// Binding //////////////

(*

The idea of Binding is to listen to a State<'T> that is managed by another Context and be able to update it
while notifying the two Contexts involved (source and target)

let child (count: BindingRequest<int>) =
    view {
        let! boundCount = bind count
    
        Button($"Count is {boundCount.Value}", fun () -> boundCount.Set(boundCount.Value + 1))
    }
    
let parent =
    view {
        let! count = state 0
        
        VStack() {
            Text($"Count is {count.Value}")
            child (Binding.ofState count)
        }
    }

*)

type BindingRequest<'T> = delegate of unit -> State<'T>

type [<Struct>] Binding<'T> =
    val public Context: Context
    val public Source: State<'T>
    
    new (ctx, source) = { Context = ctx; Source = source }
        
    member inline this.Current = this.Source.Current
    
    member inline this.Set(value: 'T) =
        this.Source.Set(value)
        this.Context.NeedsRender()

[<Extension>]
type BindingExtensions =
    [<Extension>]
    static member inline Bind(_: ViewBuilder, [<InlineIfLambda>] request: BindingRequest<'T>, [<InlineIfLambda>] continuation: Binding<'T> -> ComponentBodyBuilder<'msg, 'marker>) =
        ComponentBodyBuilder(fun ctx ->
            let source = request.Invoke()
            let state = Binding<'T>(ctx, source)
            (continuation state).Invoke(ctx)
        )
        
[<AutoOpen>]
module BindingHelpers =
    let inline ofState (source: State<'T>) = BindingRequest(fun () -> source)
    let inline bind (binding: Binding<'T>) = binding