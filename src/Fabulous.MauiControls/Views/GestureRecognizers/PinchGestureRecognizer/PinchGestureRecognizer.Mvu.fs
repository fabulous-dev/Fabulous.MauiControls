namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module PinchGestureRecognizerMvu =
    let PinchUpdated =
        Attributes.Mvu.defineEvent<PinchGestureUpdatedEventArgs> "PinchGestureRecognizerMvu_PinchUpdated" (fun target ->
            (target :?> PinchGestureRecognizer).PinchUpdated)

[<AutoOpen>]
module PinchGestureRecognizerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a PinchGestureRecognizer that listens for Pinch event</summary>
        /// <param name="onPinchUpdated">Message to dispatch</param>
        static member inline PinchGestureRecognizer<'msg when 'msg: equality>(onPinchUpdated: PinchGestureUpdatedEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabPinchGestureRecognizer>(
                PinchGestureRecognizer.WidgetKey,
                PinchGestureRecognizerMvu.PinchUpdated.WithValue(fun args -> onPinchUpdated args |> box)
            )