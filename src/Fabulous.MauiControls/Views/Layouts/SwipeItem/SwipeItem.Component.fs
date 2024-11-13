namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module SwipeItemComponent =
    let Invoked =
        Attributes.Component.defineEvent "SwipeItemComponent_Invoked" (fun target -> (target :?> SwipeItem).Invoked)
        
[<AutoOpen>]
module SwipeItemComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a SwipeItem widget and listen for the Invoke event</summary>
        /// <param name="onInvoked">Message to dispatch</param>
        static member inline SwipeItem(onInvoked: unit -> unit) =
            WidgetBuilder<unit, IFabSwipeItem>(SwipeItem.WidgetKey, SwipeItemComponent.Invoked.WithValue(fun _ -> onInvoked()))
