namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabTapGestureRecognizer =
    inherit IFabGestureRecognizer

module TapGestureRecognizer =
    let WidgetKey = Widgets.register<TapGestureRecognizer>()

    let Tapped =
        Attributes.defineEvent "TapGestureRecognizer_Tapped" (fun target -> (target :?> TapGestureRecognizer).Tapped)

    let NumberOfTapsRequired =
        Attributes.defineBindableInt TapGestureRecognizer.NumberOfTapsRequiredProperty

[<AutoOpen>]
module TapGestureRecognizerBuilders =
    type Fabulous.Maui.View with

        static member inline TapGestureRecognizer<'msg>(onTapped: 'msg) =
            WidgetBuilder<'msg, IFabTapGestureRecognizer>(TapGestureRecognizer.WidgetKey, TapGestureRecognizer.Tapped.WithValue(fun _ -> box onTapped))

[<Extension>]
type TapGestureRecognizerModifiers =

    /// <summary>The number of taps required to trigger the callback</summary>
    /// <param name="value">The number of taps required</param>
    [<Extension>]
    static member inline numberOfTapsRequired(this: WidgetBuilder<'msg, #IFabTapGestureRecognizer>, value: int) =
        this.AddScalar(TapGestureRecognizer.NumberOfTapsRequired.WithValue(value))
