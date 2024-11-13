namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module DragGestureRecognizerComponent =
    let DragStarting =
        Attributes.Component.defineEvent<DragStartingEventArgs> "DragGestureRecognizerComponent_DragStarting" (fun target ->
            (target :?> DragGestureRecognizer).DragStarting)
        
    let DropCompleted =
        Attributes.Component.defineEvent<DropCompletedEventArgs> "DragGestureRecognizerComponent_DropCompleted" (fun target -> (target :?> DragGestureRecognizer).DropCompleted)

[<AutoOpen>]
module DragGestureRecognizerComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a DragGestureRecognizer that listens for DragStarting event</summary>
        /// <param name="onDragStarting">Message to dispatch</param>
        static member inline DragGestureRecognizer(onDragStarting: DragStartingEventArgs -> unit) =
            WidgetBuilder<unit, IFabDragGestureRecognizer>(DragGestureRecognizer.WidgetKey, DragGestureRecognizerComponent.DragStarting.WithValue(onDragStarting))

[<Extension>]
type DragGestureRecognizerComponentModifiers =
    /// <summary>Listen for DropCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDropCompleted(this: WidgetBuilder<unit, #IFabDragGestureRecognizer>, fn: unit -> unit) =
        this.AddScalar(DragGestureRecognizerComponent.DropCompleted.WithValue(fun _ -> fn()))
