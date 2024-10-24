namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabDropGestureRecognizer =
    inherit IFabGestureRecognizer

module DropGestureRecognizer =
    let WidgetKey = Widgets.register<DropGestureRecognizer>()

    let AllowDrop =
        Attributes.defineBindableBool DropGestureRecognizer.AllowDropProperty

    let DropMsg =
        Attributes.defineEvent<DropEventArgs> "DropGestureRecognizer_DropMsg" (fun target -> (target :?> DropGestureRecognizer).Drop)

    let DropFn =
        Attributes.defineEventNoDispatch<DropEventArgs> "DropGestureRecognizer_DropFn" (fun target -> (target :?> DropGestureRecognizer).Drop)

    let DragOverMsg =
        Attributes.defineEvent<DragEventArgs> "DropGestureRecognizer_DragOverMsg" (fun target -> (target :?> DropGestureRecognizer).DragOver)

    let DragOverFn =
        Attributes.defineEvent<DragEventArgs> "DropGestureRecognizer_DragOverFn" (fun target -> (target :?> DropGestureRecognizer).DragOver)

    let DragLeaveMsg =
        Attributes.defineEvent<DragEventArgs> "DropGestureRecognizer_DragLeaveMsg" (fun target -> (target :?> DropGestureRecognizer).DragLeave)

    let DragLeaveFn =
        Attributes.defineEvent<DragEventArgs> "DropGestureRecognizer_DragLeaveFn" (fun target -> (target :?> DropGestureRecognizer).DragLeave)

[<AutoOpen>]
module DropGestureRecognizerBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a DropGestureRecognizer that listens for Drop event</summary>
        /// <param name="onDrop">Message to dispatch</param>
        static member inline DropGestureRecognizer<'msg when 'msg: equality>(onDrop: DropEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabDropGestureRecognizer>(
                DropGestureRecognizer.WidgetKey,
                DropGestureRecognizer.DropMsg.WithValue(fun args -> onDrop args |> box)
            )

        /// <summary>Create a DropGestureRecognizer that listens for Drop event</summary>
        /// <param name="onDrop">Message to dispatch</param>
        static member inline DropGestureRecognizer(onDrop: DropEventArgs -> unit) =
            WidgetBuilder<'msg, IFabDropGestureRecognizer>(DropGestureRecognizer.WidgetKey, DropGestureRecognizer.DropFn.WithValue(onDrop))

[<Extension>]
type DropGestureRecognizerModifiers =
    /// <summary>Set whether users are allowed to drop</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true to allow users to drop; otherwise, false</param>
    [<Extension>]
    static member inline allowDrop(this: WidgetBuilder<'msg, #IFabDropGestureRecognizer>, value: bool) =
        this.AddScalar(DropGestureRecognizer.AllowDrop.WithValue(value))

    /// <summary>Listen for the DragOver event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragOver<'msg, 'marker when 'msg: equality and 'marker :> IFabDropGestureRecognizer>
        (
            this: WidgetBuilder<'msg, 'marker>,
            fn: DragEventArgs -> 'msg
        ) =
        this.AddScalar(DropGestureRecognizer.DragOverMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the DragOver event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragOver(this: WidgetBuilder<'msg, #IFabDropGestureRecognizer>, fn: DragEventArgs -> unit) =
        this.AddScalar(DropGestureRecognizer.DragOverFn.WithValue(fn))

    /// <summary>Listen for the DragLeave event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragLeave<'msg, 'marker when 'msg: equality and 'marker :> IFabDragGestureRecognizer>
        (
            this: WidgetBuilder<'msg, 'marker>,
            fn: DragEventArgs -> 'msg
        ) =
        this.AddScalar(DropGestureRecognizer.DragLeaveMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the DragLeave event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragLeave(this: WidgetBuilder<'msg, #IFabDropGestureRecognizer>, fn: DragEventArgs -> unit) =
        this.AddScalar(DropGestureRecognizer.DragLeaveFn.WithValue(fn))

    /// <summary>Link a ViewRef to access the direct DropGestureRecognizer control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabDropGestureRecognizer>, value: ViewRef<DropGestureRecognizer>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
