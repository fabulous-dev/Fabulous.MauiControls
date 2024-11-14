namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module PinchGestureRecognizerComponent =
    let PinchUpdated =
        Attributes.Component.defineEvent<PinchGestureUpdatedEventArgs> "PinchGestureRecognizerComponent_PinchUpdated" (fun target ->
            (target :?> PinchGestureRecognizer).PinchUpdated)

[<AutoOpen>]
module PinchGestureRecognizerComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a PinchGestureRecognizer that listens for Pinch event</summary>
        /// <param name="onPinchUpdated">Message to dispatch</param>
        static member inline PinchGestureRecognizer(onPinchUpdated: PinchGestureUpdatedEventArgs -> unit) =
            WidgetBuilder<unit, IFabPinchGestureRecognizer>(
                PinchGestureRecognizer.WidgetKey,
                PinchGestureRecognizerComponent.PinchUpdated.WithValue(onPinchUpdated)
            )
