namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

module SwipeGestureRecognizerMvu =
    let Swiped =
        Attributes.Mvu.defineEvent<SwipedEventArgs> "SwipeGestureRecognizerMvu_Swiped" (fun target -> (target :?> SwipeGestureRecognizer).Swiped)

[<AutoOpen>]
module SwipeGestureRecognizerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a SwipeGestureRecognizer that listens for Swipe event</summary>
        /// <param name="onSwiped">Message to dispatch</param>
        static member inline SwipeGestureRecognizer<'msg when 'msg: equality>(onSwiped: SwipeDirection -> 'msg) =
            WidgetBuilder<'msg, IFabSwipeGestureRecognizer>(
                SwipeGestureRecognizer.WidgetKey,
                SwipeGestureRecognizerMvu.Swiped.WithValue(fun args -> onSwiped args.Direction |> box)
            )
