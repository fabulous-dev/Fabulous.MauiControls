namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module DropGestureRecognizerMvu =
    let Drop =
        Attributes.Mvu.defineEvent<DropEventArgs> "DropGestureRecognizerMvu_Drop" (fun target -> (target :?> DropGestureRecognizer).Drop)

    let DragOver =
        Attributes.Mvu.defineEvent<DragEventArgs> "DropGestureRecognizerMvu_DragOver" (fun target -> (target :?> DropGestureRecognizer).DragOver)

    let DragLeave =
        Attributes.Mvu.defineEvent<DragEventArgs> "DropGestureRecognizerMvu_DragLeave" (fun target -> (target :?> DropGestureRecognizer).DragLeave)

[<AutoOpen>]
module DropGestureRecognizerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a DropGestureRecognizer that listens for Drop event</summary>
        /// <param name="onDrop">Message to dispatch</param>
        static member inline DropGestureRecognizer<'msg when 'msg: equality>(onDrop: DropEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabDropGestureRecognizer>(
                DropGestureRecognizer.WidgetKey,
                DropGestureRecognizerMvu.Drop.WithValue(fun args -> onDrop args |> box)
            )
            
[<Extension>]
type DropGestureRecognizerMvuModifiers =
    /// <summary>Listen for the DragOver event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragOver<'msg, 'marker when 'msg: equality and 'marker :> IFabDropGestureRecognizer>
        (
            this: WidgetBuilder<'msg, 'marker>,
            fn: DragEventArgs -> 'msg
        ) =
        this.AddScalar(DropGestureRecognizerMvu.DragOver.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the DragLeave event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onDragLeave<'msg, 'marker when 'msg: equality and 'marker :> IFabDragGestureRecognizer>
        (
            this: WidgetBuilder<'msg, 'marker>,
            fn: DragEventArgs -> 'msg
        ) =
        this.AddScalar(DropGestureRecognizerMvu.DragLeave.WithValue(fun args -> fn args |> box))
