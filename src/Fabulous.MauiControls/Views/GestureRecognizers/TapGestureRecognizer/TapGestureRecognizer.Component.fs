namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module TapGestureRecognizerComponent =
    let Tapped =
        Attributes.Component.defineEvent "TapGestureRecognizerComponent_Tapped" (fun target -> (target :?> TapGestureRecognizer).Tapped)

[<AutoOpen>]
module TapGestureRecognizerComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a TapGestureRecognizer that listens for Tapped event</summary>
        /// <param name="onTapped">Message to dispatch</param>
        static member inline TapGestureRecognizer(onTapped: unit -> unit) =
            WidgetBuilder<unit, IFabTapGestureRecognizer>(TapGestureRecognizer.WidgetKey, TapGestureRecognizerComponent.Tapped.WithValue(fun _ -> onTapped()))
