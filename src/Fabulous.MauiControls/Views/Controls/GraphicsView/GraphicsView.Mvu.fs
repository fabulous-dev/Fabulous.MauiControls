namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module GraphicsViewMvu =
    let CancelInteraction =
        Attributes.Mvu.defineEventNoArg "GraphicsViewMvu_CancelInteraction" (fun target -> (target :?> GraphicsView).CancelInteraction)

    let DragInteraction =
        Attributes.Mvu.defineEvent<TouchEventArgs> "GraphicsViewMvu_DragInteraction" (fun target -> (target :?> GraphicsView).DragInteraction)

    let EndHoverInteraction =
        Attributes.Mvu.defineEventNoArg "GraphicsViewMvu_EndHoverInteraction" (fun target -> (target :?> GraphicsView).EndHoverInteraction)

    let EndInteraction =
        Attributes.Mvu.defineEvent<TouchEventArgs> "GraphicsViewMvu_EndInteraction" (fun target -> (target :?> GraphicsView).EndInteraction)

    let MoveHoverInteraction =
        Attributes.Mvu.defineEvent<TouchEventArgs> "GraphicsViewMvu_MoveHoverInteraction" (fun target -> (target :?> GraphicsView).MoveHoverInteraction)

    let StartHoverInteraction =
        Attributes.Mvu.defineEvent<TouchEventArgs> "GraphicsViewMvu_StartHoverInteraction" (fun target -> (target :?> GraphicsView).StartHoverInteraction)

    let StartInteraction =
        Attributes.Mvu.defineEvent<TouchEventArgs> "GraphicsViewMvu_StartInteraction" (fun target -> (target :?> GraphicsView).StartInteraction)

[<Extension>]
type GraphicsViewMvuModifiers =
    /// <summary>Listen for the CancelInteraction event, which is raised when the press that made contact with the GraphicsView loses contact</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCancelInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, msg: 'msg) =
        this.AddScalar(GraphicsViewMvu.CancelInteraction.WithValue(MsgValue(msg)))

    /// <summary>Listen for the DragInteraction event, with TouchEventArgs, which is raised when the GraphicsView is dragged</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsViewMvu.DragInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the EndHoverInteraction event, which is raised when a pointer leaves the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onEndHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, msg: 'msg) =
        this.AddScalar(GraphicsViewMvu.EndHoverInteraction.WithValue(MsgValue(msg)))

    /// <summary>Listen for the EndInteraction event, with TouchEventArgs, which is raised when the press that raised the StartInteraction event is released</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onEndInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsViewMvu.EndInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the MoveHoverInteraction event, with TouchEventArgs, which is raised when a pointer moves while the pointer remains within the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onMoveHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsViewMvu.MoveHoverInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the StartHoverInteraction event, with TouchEventArgs, which is raised when a pointer enters the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartHoverInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsViewMvu.StartHoverInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the StartInteraction event, with TouchEventArgs, which is raised when the GraphicsView is pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartInteraction(this: WidgetBuilder<'msg, #IGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsViewMvu.StartInteraction.WithValue(fun args -> fn args |> box))
