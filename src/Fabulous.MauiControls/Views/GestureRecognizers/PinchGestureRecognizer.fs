namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabPinchGestureRecognizer =
    inherit IFabGestureRecognizer

module PinchGestureRecognizer =
    let WidgetKey = Widgets.register<PinchGestureRecognizer>()

    let PinchUpdatedMsg =
        Attributes.defineEvent<PinchGestureUpdatedEventArgs> "PinchGestureRecognizer_PinchUpdatedMsg" (fun target ->
            (target :?> PinchGestureRecognizer).PinchUpdated)

    let PinchUpdatedFn =
        Attributes.defineEventNoDispatch<PinchGestureUpdatedEventArgs> "PinchGestureRecognizer_PinchUpdatedFn" (fun target ->
            (target :?> PinchGestureRecognizer).PinchUpdated)

[<AutoOpen>]
module PinchGestureRecognizerBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a PinchGestureRecognizer that listens for Pinch event</summary>
        /// <param name="onPinchUpdated">Message to dispatch</param>
        static member inline PinchGestureRecognizer<'msg when 'msg : equality>(onPinchUpdated: PinchGestureUpdatedEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabPinchGestureRecognizer>(
                PinchGestureRecognizer.WidgetKey,
                PinchGestureRecognizer.PinchUpdatedMsg.WithValue(fun args -> onPinchUpdated args |> box)
            )

        /// <summary>Create a PinchGestureRecognizer that listens for Pinch event</summary>
        /// <param name="onPinchUpdated">Message to dispatch</param>
        static member inline PinchGestureRecognizer(onPinchUpdated: PinchGestureUpdatedEventArgs -> unit) =
            WidgetBuilder<'msg, IFabPinchGestureRecognizer>(
                PinchGestureRecognizer.WidgetKey,
                PinchGestureRecognizer.PinchUpdatedFn.WithValue(onPinchUpdated)
            )

[<Extension>]
type PinchGestureRecognizerModifiers =
    /// <summary>Link a ViewRef to access the direct PinchGestureRecognizer control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabPinchGestureRecognizer>, value: ViewRef<PinchGestureRecognizer>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
