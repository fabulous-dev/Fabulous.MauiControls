namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ItemsViewMvu =
    let RemainingItemsThresholdReached =
        Attributes.Mvu.defineEventNoArg "ItemsViewMvu_RemainingItemsThresholdReached" (fun target -> (target :?> ItemsView).RemainingItemsThresholdReached)

    let Scrolled =
        Attributes.Mvu.defineEvent<ItemsViewScrolledEventArgs> "ItemsViewMvu_Scrolled" (fun target -> (target :?> ItemsView).Scrolled)

    let ScrollToRequested =
        Attributes.Mvu.defineEvent<ScrollToRequestEventArgs> "ItemsViewMvu_ScrolledRequested" (fun target -> (target :?> ItemsView).ScrollToRequested)

[<Extension>]
type ItemsViewMvuModifiers =
    /// <summary>Listen for the Scrolled event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrolled(this: WidgetBuilder<'msg, #IFabItemsView>, fn: ItemsViewScrolledEventArgs -> 'msg) =
        this.AddScalar(ItemsViewMvu.Scrolled.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the ScrollToRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrollToRequested(this: WidgetBuilder<'msg, #IFabItemsView>, fn: ScrollToRequestEventArgs -> 'msg) =
        this.AddScalar(ItemsViewMvu.ScrollToRequested.WithValue(fun args -> fn args |> box))

    /// <summary>Set the threshold of items not yet visible in the list at which the RemainingItemsThresholdReached event will be fired</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The threshold of items not yet visible in the list</param>
    /// <param name="onThresholdReached">Message to dispatch</param>
    [<Extension>]
    static member inline remainingItemsThreshold(this: WidgetBuilder<'msg, #IFabItemsView>, value: int, onThresholdReached: 'msg) =
        this
            .AddScalar(ItemsView.RemainingItemsThreshold.WithValue(value))
            .AddScalar(ItemsViewMvu.RemainingItemsThresholdReached.WithValue(MsgValue(onThresholdReached)))
