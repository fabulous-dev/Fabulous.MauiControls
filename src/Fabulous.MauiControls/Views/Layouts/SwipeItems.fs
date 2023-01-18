namespace Fabulous.Maui

open System.Collections.Generic
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabSwipeItems =
    inherit IFabElement

module SwipeItems =

    let WidgetKey = Widgets.register<SwipeItems>()

    let SwipeItems =
        Attributes.defineListWidgetCollection "SwipeItems_SwipeItems" (fun target -> (target :?> SwipeItems) :> IList<_>)

    let SwipeMode =
        Attributes.defineBindableEnum<SwipeMode> Microsoft.Maui.Controls.SwipeItems.ModeProperty

    let SwipeBehaviorOnInvoked =
        Attributes.defineBindableEnum<SwipeBehaviorOnInvoked> Microsoft.Maui.Controls.SwipeItems.SwipeBehaviorOnInvokedProperty

[<AutoOpen>]
module SwipeItemsBuilders =

    type Fabulous.Maui.View with

        static member inline SwipeItems<'msg>() =
            CollectionBuilder<'msg, IFabSwipeItems, IFabSwipeItem>(SwipeItems.WidgetKey, SwipeItems.SwipeItems)

[<Extension>]
type SwipeItemsModifiers() =
    [<Extension>]
    static member inline swipeMode(this: WidgetBuilder<'msg, #IFabSwipeItems>, value: SwipeMode) =
        this.AddScalar(SwipeItems.SwipeMode.WithValue(value))

    [<Extension>]
    static member inline swipeBehaviorOnInvoked(this: WidgetBuilder<'msg, #IFabSwipeItems>, value: SwipeBehaviorOnInvoked) =
        this.AddScalar(SwipeItems.SwipeBehaviorOnInvoked.WithValue(value))

    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSwipeItems>, value: ViewRef<SwipeItems>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
