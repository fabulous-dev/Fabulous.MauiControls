namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module PanGestureRecognizerMvu =
    let PanUpdated =
        Attributes.Mvu.defineEvent<PanUpdatedEventArgs> "PanGestureRecognizerMvu_PanUpdated" (fun target -> (target :?> PanGestureRecognizer).PanUpdated)

[<AutoOpen>]
module PanGestureRecognizerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a PanGestureRecognizer that listens for Pan event</summary>
        /// <param name="onPanUpdated">Message to dispatch</param>
        static member inline PanGestureRecognizer<'msg when 'msg: equality>(onPanUpdated: PanUpdatedEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabPanGestureRecognizer>(
                PanGestureRecognizer.WidgetKey,
                PanGestureRecognizerMvu.PanUpdated.WithValue(fun args -> onPanUpdated args |> box)
            )
