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

    let SwipedMsg =
        Attributes.defineEvent<SwipedEventArgs> "SwipeGestureRecognizer_SwipedMsg" (fun target -> (target :?> SwipeGestureRecognizer).Swiped)

    let SwipedFn =
        Attributes.defineEventNoDispatch<SwipedEventArgs> "SwipeGestureRecognizer_SwipedFn" (fun target -> (target :?> SwipeGestureRecognizer).Swiped)

    let Threshold =
        Attributes.defineBindableInt SwipeGestureRecognizer.ThresholdProperty

[<AutoOpen>]
module SwipeGestureRecognizerBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a SwipeGestureRecognizer that listens for Swipe event</summary>
        /// <param name="onSwiped">Message to dispatch</param>
        static member inline SwipeGestureRecognizer<'msg when 'msg : equality>(onSwiped: SwipeDirection -> 'msg) =
            WidgetBuilder<'msg, IFabSwipeGestureRecognizer>(
                SwipeGestureRecognizer.WidgetKey,
                SwipeGestureRecognizer.SwipedMsg.WithValue(fun args -> onSwiped args.Direction |> box)
            )

        /// <summary>Create a SwipeGestureRecognizer that listens for Swipe event</summary>
        /// <param name="onSwiped">Message to dispatch</param>
        static member inline SwipeGestureRecognizer(onSwiped: SwipeDirection -> unit) =
            WidgetBuilder<'msg, IFabSwipeGestureRecognizer>(
                SwipeGestureRecognizer.WidgetKey,
                SwipeGestureRecognizer.SwipedFn.WithValue(fun args -> onSwiped args.Direction)
            )

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
