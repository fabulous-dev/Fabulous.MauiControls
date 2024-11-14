namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module TapGestureRecognizerMvu =
    let Tapped =
        Attributes.Mvu.defineEvent "TapGestureRecognizerMvu_Tapped" (fun target -> (target :?> TapGestureRecognizer).Tapped)

[<AutoOpen>]
module TapGestureRecognizerBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a TapGestureRecognizer that listens for Tapped event</summary>
        /// <param name="onTapped">Message to dispatch</param>
        static member inline TapGestureRecognizer<'msg when 'msg: equality>(onTapped: 'msg) =
            WidgetBuilder<'msg, IFabTapGestureRecognizer>(TapGestureRecognizer.WidgetKey, TapGestureRecognizerMvu.Tapped.WithValue(fun _ -> box onTapped))
