namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ItemsViewComponent =
    let RemainingItemsThresholdReached =
        Attributes.Component.defineEventNoArg "ItemsViewComponent_RemainingItemsThresholdReached" (fun target ->
            (target :?> ItemsView).RemainingItemsThresholdReached)

    let Scrolled =
        Attributes.Component.defineEvent<ItemsViewScrolledEventArgs> "ItemsViewComponent_Scrolled" (fun target -> (target :?> ItemsView).Scrolled)

    let ScrollToRequested =
        Attributes.Component.defineEvent<ScrollToRequestEventArgs> "ItemsViewComponent_ScrolledRequested" (fun target ->
            (target :?> ItemsView).ScrollToRequested)

[<Extension>]
type ItemsViewComponentModifiers =
    /// <summary>Listen for the Scrolled event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrolled(this: WidgetBuilder<unit, #IFabItemsView>, fn: ItemsViewScrolledEventArgs -> unit) =
        this.AddScalar(ItemsViewComponent.Scrolled.WithValue(fn))

    /// <summary>Listen for the ScrollToRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrollToRequested(this: WidgetBuilder<unit, #IFabItemsView>, fn: ScrollToRequestEventArgs -> unit) =
        this.AddScalar(ItemsViewComponent.ScrollToRequested.WithValue(fn))

    /// <summary>Set the threshold of items not yet visible in the list at which the RemainingItemsThresholdReached event will be fired</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The threshold of items not yet visible in the list</param>
    /// <param name="onThresholdReached">Message to dispatch</param>
    [<Extension>]
    static member inline remainingItemsThreshold(this: WidgetBuilder<unit, #IFabItemsView>, value: int, onThresholdReached: unit -> unit) =
        this
            .AddScalar(ItemsView.RemainingItemsThreshold.WithValue(value))
            .AddScalar(ItemsViewComponent.RemainingItemsThresholdReached.WithValue(onThresholdReached))
