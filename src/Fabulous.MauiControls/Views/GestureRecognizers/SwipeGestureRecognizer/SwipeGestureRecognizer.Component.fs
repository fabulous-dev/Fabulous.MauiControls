namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

module SwipeGestureRecognizerComponent =
    let Swiped =
        Attributes.Component.defineEvent<SwipedEventArgs> "SwipeGestureRecognizerComponent_Swiped" (fun target -> (target :?> SwipeGestureRecognizer).Swiped)

[<AutoOpen>]
module SwipeGestureRecognizerComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a SwipeGestureRecognizer that listens for Swipe event</summary>
        /// <param name="onSwiped">Message to dispatch</param>
        static member inline SwipeGestureRecognizer(onSwiped: SwipeDirection -> unit) =
            WidgetBuilder<unit, IFabSwipeGestureRecognizer>(
                SwipeGestureRecognizer.WidgetKey,
                SwipeGestureRecognizerComponent.Swiped.WithValue(fun args -> onSwiped args.Direction)
            )