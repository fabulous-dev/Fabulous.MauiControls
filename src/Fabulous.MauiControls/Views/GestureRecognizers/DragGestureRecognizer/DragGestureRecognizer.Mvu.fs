namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module DragGestureRecognizerMvu =
    let DragStarting =
        Attributes.Mvu.defineEvent<DragStartingEventArgs> "DragGestureRecognizerMvu_DragStarting" (fun target ->
            (target :?> DragGestureRecognizer).DragStarting)

    let DropCompleted =
        Attributes.Mvu.defineEvent<DropCompletedEventArgs> "DragGestureRecognizerMvu_DropCompleted" (fun target ->
            (target :?> DragGestureRecognizer).DropCompleted)

[<AutoOpen>]
module DragGestureRecognizerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a DragGestureRecognizer that listens for DragStarting event</summary>
        /// <param name="onDragStarting">Message to dispatch</param>
        static member inline DragGestureRecognizer<'msg when 'msg: equality>(onDragStarting: DragStartingEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabDragGestureRecognizer>(
                DragGestureRecognizer.WidgetKey,
                DragGestureRecognizerMvu.DragStarting.WithValue(fun args -> onDragStarting args |> box)
            )

[<Extension>]
type DragGestureRecognizerMvuModifiers =
    /// <summary>Listen for DropCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDropCompleted<'msg, 'marker when 'msg: equality and 'marker :> IFabDragGestureRecognizer>
        (this: WidgetBuilder<'msg, 'marker>, msg: 'msg)
        =
        this.AddScalar(DragGestureRecognizerMvu.DropCompleted.WithValue(fun _ -> box msg))
