namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabTapGestureRecognizer =
    inherit IFabGestureRecognizer

module TapGestureRecognizer =
    let WidgetKey = Widgets.register<TapGestureRecognizer>()

    let NumberOfTapsRequired =
        Attributes.defineBindableInt TapGestureRecognizer.NumberOfTapsRequiredProperty

    let TappedMsg =
        Attributes.defineEvent "TapGestureRecognizer_TappedMsg" (fun target -> (target :?> TapGestureRecognizer).Tapped)

    let TappedFn =
        Attributes.defineEventNoDispatch "TapGestureRecognizer_TappedFn" (fun target -> (target :?> TapGestureRecognizer).Tapped)

[<AutoOpen>]
module TapGestureRecognizerBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a TapGestureRecognizer that listens for Tapped event</summary>
        /// <param name="onTapped">Message to dispatch</param>
        static member inline TapGestureRecognizer<'msg when 'msg : equality>(onTapped: 'msg) =
            WidgetBuilder<'msg, IFabTapGestureRecognizer>(TapGestureRecognizer.WidgetKey, TapGestureRecognizer.TappedMsg.WithValue(fun _ -> box onTapped))

        /// <summary>Create a TapGestureRecognizer that listens for Tapped event</summary>
        /// <param name="onTapped">Message to dispatch</param>
        static member inline TapGestureRecognizer(onTapped: unit -> unit) =
            WidgetBuilder<'msg, IFabTapGestureRecognizer>(TapGestureRecognizer.WidgetKey, TapGestureRecognizer.TappedFn.WithValue(fun _ -> onTapped()))

[<Extension>]
type TapGestureRecognizerModifiers =
    /// <summary>Set the number of taps required to trigger the gesture</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The number of taps required</param>
    [<Extension>]
    static member inline numberOfTapsRequired(this: WidgetBuilder<'msg, #IFabTapGestureRecognizer>, value: int) =
        this.AddScalar(TapGestureRecognizer.NumberOfTapsRequired.WithValue(value))

    /// <summary>Link a ViewRef to access the direct TapGestureRecognizer control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabTapGestureRecognizer>, value: ViewRef<TapGestureRecognizer>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
