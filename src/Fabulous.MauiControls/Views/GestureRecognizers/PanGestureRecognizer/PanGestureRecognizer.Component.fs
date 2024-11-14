namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module PanGestureRecognizerComponent =
    let PanUpdated =
        Attributes.Component.defineEvent<PanUpdatedEventArgs> "PanGestureRecognizerComponent_PanUpdated" (fun target ->
            (target :?> PanGestureRecognizer).PanUpdated)

[<AutoOpen>]
module PanGestureRecognizerComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a PanGestureRecognizer that listens for Pan event</summary>
        /// <param name="onPanUpdated">Message to dispatch</param>
        static member inline PanGestureRecognizer(onPanUpdated: PanUpdatedEventArgs -> unit) =
            WidgetBuilder<unit, IFabPanGestureRecognizer>(PanGestureRecognizer.WidgetKey, PanGestureRecognizerComponent.PanUpdated.WithValue(onPanUpdated))
