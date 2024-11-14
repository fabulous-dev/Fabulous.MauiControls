namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module SwipeItemMvu =
    let Invoked =
        Attributes.Mvu.defineEvent "SwipeItemMvu_Invoked" (fun target -> (target :?> SwipeItem).Invoked)

[<AutoOpen>]
module SwipeItemMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a SwipeItem widget and listen for the Invoke event</summary>
        /// <param name="onInvoked">Message to dispatch</param>
        static member inline SwipeItem(onInvoked: 'msg) =
            WidgetBuilder<'msg, IFabSwipeItem>(SwipeItem.WidgetKey, SwipeItemMvu.Invoked.WithValue(fun _ -> box onInvoked))
