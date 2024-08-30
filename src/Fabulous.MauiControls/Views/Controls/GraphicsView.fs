namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IGraphicsView =
    inherit IFabView

module GraphicsView =
    let WidgetKey = Widgets.register<GraphicsView>()

    let CancelInteractionMsg =
        Attributes.defineEventNoArg "GraphicsView_CancelInteractionMsg" (fun target -> (target :?> GraphicsView).CancelInteraction)

    let CancelInteractionFn =
        Attributes.defineEventNoArgNoDispatch "GraphicsView_CancelInteractionFn" (fun target -> (target :?> GraphicsView).CancelInteraction)

    let DragInteractionMsg =
        Attributes.defineEvent<TouchEventArgs> "GraphicsView_DragInteractionMsg" (fun target -> (target :?> GraphicsView).DragInteraction)

    let DragInteractionFn =
        Attributes.defineEventNoDispatch<TouchEventArgs> "GraphicsView_DragInteractionFn" (fun target -> (target :?> GraphicsView).DragInteraction)

    let Drawable =
        Attributes.defineBindableWithEquality<IDrawable> GraphicsView.DrawableProperty

    let EndHoverInteractionMsg =
        Attributes.defineEventNoArg "GraphicsView_EndHoverInteractionMsg" (fun target -> (target :?> GraphicsView).EndHoverInteraction)

    let EndHoverInteractionFn =
        Attributes.defineEventNoArgNoDispatch "GraphicsView_EndHoverInteractionFn" (fun target -> (target :?> GraphicsView).EndHoverInteraction)

    let EndInteractionMsg =
        Attributes.defineEvent<TouchEventArgs> "GraphicsView_EndInteractionMsg" (fun target -> (target :?> GraphicsView).EndInteraction)

    let EndInteractionFn =
        Attributes.defineEventNoDispatch<TouchEventArgs> "GraphicsView_EndInteractionFn" (fun target -> (target :?> GraphicsView).EndInteraction)

    let MoveHoverInteractionMsg =
        Attributes.defineEvent<TouchEventArgs> "GraphicsView_MoveHoverInteractionMsg" (fun target -> (target :?> GraphicsView).MoveHoverInteraction)

    let MoveHoverInteractionFn =
        Attributes.defineEventNoDispatch<TouchEventArgs> "GraphicsView_MoveHoverInteractionFn" (fun target -> (target :?> GraphicsView).MoveHoverInteraction)

    let StartHoverInteractionMsg =
        Attributes.defineEvent<TouchEventArgs> "GraphicsView_StartHoverInteractionMsg" (fun target -> (target :?> GraphicsView).StartHoverInteraction)

    let StartHoverInteractionFn =
        Attributes.defineEventNoDispatch<TouchEventArgs> "GraphicsView_StartHoverInteractionFn" (fun target -> (target :?> GraphicsView).StartHoverInteraction)

    let StartInteractionMsg =
        Attributes.defineEvent<TouchEventArgs> "GraphicsView_StartInteractionMsg" (fun target -> (target :?> GraphicsView).StartInteraction)

    let StartInteractionFn =
        Attributes.defineEventNoDispatch<TouchEventArgs> "GraphicsView_StartInteractionFn" (fun target -> (target :?> GraphicsView).StartInteraction)

[<AutoOpen>]
module GraphicsViewBuilders =
    /// <summary>GraphicsView defines the Drawable property, of type IDrawable, which specifies the content that will be drawn.</summary>
    type Fabulous.Maui.View with

        /// <summary>Create a GraphicsView widget with a drawable content</summary>
        /// <param name="drawable">The drawable content</param>
        static member inline GraphicsView(drawable: IDrawable) =
            WidgetBuilder<'msg, IGraphicsView>(GraphicsView.WidgetKey, GraphicsView.Drawable.WithValue(drawable))

[<Extension>]
type GraphicsViewModifiers =
    /// <summary>Listen for the CancelInteraction event, which is raised when the press that made contact with the GraphicsView loses contact</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCancelInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, msg: 'msg) =
        this.AddScalar(GraphicsView.CancelInteractionMsg.WithValue(MsgValue(msg)))
        
    /// <summary>Listen for the CancelInteraction event, which is raised when the press that made contact with the GraphicsView loses contact</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onCancelInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: unit -> unit) =
        this.AddScalar(GraphicsView.CancelInteractionFn.WithValue(fn))

    /// <summary>Listen for the DragInteraction event, with TouchEventArgs, which is raised when the GraphicsView is dragged</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.DragInteractionMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the DragInteraction event, with TouchEventArgs, which is raised when the GraphicsView is dragged</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsView.DragInteractionFn.WithValue(fn))

    /// <summary>Listen for the EndHoverInteraction event, which is raised when a pointer leaves the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onEndHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, msg: 'msg) =
        this.AddScalar(GraphicsView.EndHoverInteractionMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the EndHoverInteraction event, which is raised when a pointer leaves the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onEndHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: unit -> unit) =
        this.AddScalar(GraphicsView.EndHoverInteractionFn.WithValue(fn))

    /// <summary>Listen for the EndInteraction event, with TouchEventArgs, which is raised when the press that raised the StartInteraction event is released</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onEndInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.EndInteractionMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the EndInteraction event, with TouchEventArgs, which is raised when the press that raised the StartInteraction event is released</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onEndInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsView.EndInteractionFn.WithValue(fn))

    /// <summary>Listen for the MoveHoverInteraction event, with TouchEventArgs, which is raised when a pointer moves while the pointer remains within the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onMoveHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.MoveHoverInteractionMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the MoveHoverInteraction event, with TouchEventArgs, which is raised when a pointer moves while the pointer remains within the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onMoveHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsView.MoveHoverInteractionFn.WithValue(fn))

    /// <summary>Listen for the StartHoverInteraction event, with TouchEventArgs, which is raised when a pointer enters the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.StartHoverInteractionMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the StartHoverInteraction event, with TouchEventArgs, which is raised when a pointer enters the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsView.StartHoverInteractionFn.WithValue(fn))

    /// <summary>Listen for the StartInteraction event, with TouchEventArgs, which is raised when the GraphicsView is pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.StartInteractionMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the StartInteraction event, with TouchEventArgs, which is raised when the GraphicsView is pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsView.StartInteractionFn.WithValue(fn))
