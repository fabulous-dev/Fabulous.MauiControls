namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module GraphicsViewComponent =
    let CancelInteraction =
        Attributes.Component.defineEventNoArg "GraphicsViewComponent_CancelInteraction" (fun target -> (target :?> GraphicsView).CancelInteraction)

    let DragInteraction =
        Attributes.Component.defineEvent<TouchEventArgs> "GraphicsViewComponent_DragInteraction" (fun target -> (target :?> GraphicsView).DragInteraction)

    let EndHoverInteraction =
        Attributes.Component.defineEventNoArg "GraphicsViewComponent_EndHoverInteraction" (fun target -> (target :?> GraphicsView).EndHoverInteraction)

    let EndInteraction =
        Attributes.Component.defineEvent<TouchEventArgs> "GraphicsViewComponent_EndInteraction" (fun target -> (target :?> GraphicsView).EndInteraction)

    let MoveHoverInteraction =
        Attributes.Component.defineEvent<TouchEventArgs> "GraphicsViewComponent_MoveHoverInteraction" (fun target ->
            (target :?> GraphicsView).MoveHoverInteraction)

    let StartHoverInteraction =
        Attributes.Component.defineEvent<TouchEventArgs> "GraphicsViewComponent_StartHoverInteraction" (fun target ->
            (target :?> GraphicsView).StartHoverInteraction)

    let StartInteraction =
        Attributes.Component.defineEvent<TouchEventArgs> "GraphicsViewComponent_StartInteraction" (fun target -> (target :?> GraphicsView).StartInteraction)

[<Extension>]
type GraphicsViewComponentModifiers =
    /// <summary>Listen for the CancelInteraction event, which is raised when the press that made contact with the GraphicsView loses contact</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onCancelInteraction(this: WidgetBuilder<unit, #IGraphicsView>, fn: unit -> unit) =
        this.AddScalar(GraphicsViewComponent.CancelInteraction.WithValue(fn))

    /// <summary>Listen for the DragInteraction event, with TouchEventArgs, which is raised when the GraphicsView is dragged</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragInteraction(this: WidgetBuilder<unit, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsViewComponent.DragInteraction.WithValue(fn))

    /// <summary>Listen for the EndHoverInteraction event, which is raised when a pointer leaves the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onEndHoverInteraction(this: WidgetBuilder<unit, #IGraphicsView>, fn: unit -> unit) =
        this.AddScalar(GraphicsViewComponent.EndHoverInteraction.WithValue(fn))

    /// <summary>Listen for the EndInteraction event, with TouchEventArgs, which is raised when the press that raised the StartInteraction event is released</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onEndInteraction(this: WidgetBuilder<unit, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsViewComponent.EndInteraction.WithValue(fn))

    /// <summary>Listen for the MoveHoverInteraction event, with TouchEventArgs, which is raised when a pointer moves while the pointer remains within the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onMoveHoverInteraction(this: WidgetBuilder<unit, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsViewComponent.MoveHoverInteraction.WithValue(fn))

    /// <summary>Listen for the StartHoverInteraction event, with TouchEventArgs, which is raised when a pointer enters the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartHoverInteraction(this: WidgetBuilder<unit, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsViewComponent.StartHoverInteraction.WithValue(fn))

    /// <summary>Listen for the StartInteraction event, with TouchEventArgs, which is raised when the GraphicsView is pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartInteraction(this: WidgetBuilder<unit, #IGraphicsView>, fn: TouchEventArgs -> unit) =
        this.AddScalar(GraphicsViewComponent.StartInteraction.WithValue(fn))
