namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabSwipeGestureRecognizer =
    inherit IFabGestureRecognizer

module SwipeGestureRecognizer =
    let WidgetKey = Widgets.register<SwipeGestureRecognizer>()

    let Direction =
        Attributes.defineBindableEnum<SwipeDirection> SwipeGestureRecognizer.DirectionProperty

    let Threshold =
        Attributes.defineBindableInt SwipeGestureRecognizer.ThresholdProperty

[<Extension>]
type SwipeGestureRecognizerModifiers =
    /// <summary>Set the direction of swipes to recognize.</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The direction of swipes</param>
    [<Extension>]
    static member inline direction(this: WidgetBuilder<'msg, #IFabSwipeGestureRecognizer>, value: SwipeDirection) =
        this.AddScalar(SwipeGestureRecognizer.Direction.WithValue(value))

    /// <summary>Set the minimum swipe distance that will cause the gesture to be recognized</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The minimum swipe distance</param>
    [<Extension>]
    static member inline threshold(this: WidgetBuilder<'msg, #IFabSwipeGestureRecognizer>, value: int) =
        this.AddScalar(SwipeGestureRecognizer.Threshold.WithValue(value))

    /// <summary>Link a ViewRef to access the direct SwipeGestureRecognizer control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSwipeGestureRecognizer>, value: ViewRef<SwipeGestureRecognizer>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
