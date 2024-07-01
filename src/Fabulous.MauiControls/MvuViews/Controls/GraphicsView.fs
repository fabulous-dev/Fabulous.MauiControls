namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabMvuGraphicsView =
    inherit IFabMvuView
    inherit IFabGraphicsView

module GraphicsView =
    let CancelInteraction =
        MvuAttributes.defineEventNoArg "GraphicsView_CancelInteraction" (fun target -> (target :?> GraphicsView).CancelInteraction)

    let DragInteraction =
        MvuAttributes.defineEvent<TouchEventArgs> "GraphicsView_DragInteraction" (fun target -> (target :?> GraphicsView).DragInteraction)

    let EndHoverInteraction =
        MvuAttributes.defineEventNoArg "GraphicsView_EndHoverInteraction" (fun target -> (target :?> GraphicsView).EndHoverInteraction)

    let EndInteraction =
        MvuAttributes.defineEvent<TouchEventArgs> "GraphicsView_EndInteraction" (fun target -> (target :?> GraphicsView).EndInteraction)

    let MoveHoverInteraction =
        MvuAttributes.defineEvent<TouchEventArgs> "GraphicsView_MoveHoverInteraction" (fun target -> (target :?> GraphicsView).MoveHoverInteraction)

    let StartHoverInteraction =
        MvuAttributes.defineEvent<TouchEventArgs> "GraphicsView_StartHoverInteraction" (fun target -> (target :?> GraphicsView).StartHoverInteraction)

    let StartInteraction =
        MvuAttributes.defineEvent<TouchEventArgs> "GraphicsView_StartInteraction" (fun target -> (target :?> GraphicsView).StartInteraction)

[<AutoOpen>]
module GraphicsViewBuilders =
    /// <summary>GraphicsView defines the Drawable property, of type IDrawable, which specifies the content that will be drawn.</summary>
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a GraphicsView widget with a drawable content</summary>
        /// <param name="drawable">The drawable content</param>
        static member inline GraphicsView<'msg>(drawable: IDrawable) =
            WidgetBuilder<'msg, IFabMvuGraphicsView>(GraphicsView.WidgetKey, GraphicsView.Drawable.WithValue(drawable))

[<Extension>]
type GraphicsViewModifiers =
    /// <summary>Listen for the CancelInteraction event, which is raised when the press that made contact with the GraphicsView loses contact</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCancelInteraction(this: WidgetBuilder<'msg, #IFabMvuGraphicsView>, msg: 'msg) =
        this.AddScalar(GraphicsView.CancelInteraction.WithValue(MsgValue(msg)))

    /// <summary>Listen for the DragInteraction event, with TouchEventArgs, which is raised when the GraphicsView is dragged</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragInteraction(this: WidgetBuilder<'msg, #IFabMvuGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.DragInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the EndHoverInteraction event, which is raised when a pointer leaves the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onEndHoverInteraction(this: WidgetBuilder<'msg, #IFabMvuGraphicsView>, msg: 'msg) =
        this.AddScalar(GraphicsView.EndHoverInteraction.WithValue(MsgValue(msg)))

    /// <summary>Listen for the EndInteraction event, with TouchEventArgs, which is raised when the press that raised the StartInteraction event is released</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onEndInteraction(this: WidgetBuilder<'msg, #IFabMvuGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.EndInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the MoveHoverInteraction event, with TouchEventArgs, which is raised when a pointer moves while the pointer remains within the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onMoveHoverInteraction(this: WidgetBuilder<'msg, #IFabMvuGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.MoveHoverInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the StartHoverInteraction event, with TouchEventArgs, which is raised when a pointer enters the hit test area of the GraphicsView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartHoverInteraction(this: WidgetBuilder<'msg, #IFabMvuGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.StartHoverInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the StartInteraction event, with TouchEventArgs, which is raised when the GraphicsView is pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onStartInteraction(this: WidgetBuilder<'msg, #IFabMvuGraphicsView>, fn: TouchEventArgs -> 'msg) =
        this.AddScalar(GraphicsView.StartInteraction.WithValue(fun args -> fn args |> box))

    /// <summary>Link a ViewRef to access the direct RadioButton control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuGraphicsView>, value: ViewRef<GraphicsView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
