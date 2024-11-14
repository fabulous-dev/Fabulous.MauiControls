namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module DropGestureRecognizerComponent =
    let Drop =
        Attributes.Component.defineEvent<DropEventArgs> "DropGestureRecognizerComponent_Drop" (fun target -> (target :?> DropGestureRecognizer).Drop)

    let DragOver =
        Attributes.Component.defineEvent<DragEventArgs> "DropGestureRecognizerComponent_DragOver" (fun target -> (target :?> DropGestureRecognizer).DragOver)

    let DragLeave =
        Attributes.Component.defineEvent<DragEventArgs> "DropGestureRecognizerComponent_DragLeave" (fun target -> (target :?> DropGestureRecognizer).DragLeave)

[<AutoOpen>]
module DropGestureRecognizerComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a DropGestureRecognizer that listens for Drop event</summary>
        /// <param name="onDrop">Message to dispatch</param>
        static member inline DropGestureRecognizer(onDrop: DropEventArgs -> unit) =
            WidgetBuilder<unit, IFabDropGestureRecognizer>(DropGestureRecognizer.WidgetKey, DropGestureRecognizerComponent.Drop.WithValue(onDrop))

[<Extension>]
type DropGestureRecognizerComponentModifiers =
    /// <summary>Listen for the DragOver event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragOver(this: WidgetBuilder<unit, #IFabDropGestureRecognizer>, fn: DragEventArgs -> unit) =
        this.AddScalar(DropGestureRecognizerComponent.DragOver.WithValue(fn))

    /// <summary>Listen for the DragLeave event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragLeave(this: WidgetBuilder<unit, #IFabDropGestureRecognizer>, fn: DragEventArgs -> unit) =
        this.AddScalar(DropGestureRecognizerComponent.DragLeave.WithValue(fn))
