namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ListViewComponent =
    let ItemAppearing =
        Attributes.Component.defineEvent<ItemVisibilityEventArgs> "ListViewComponent_ItemAppearing" (fun target -> (target :?> ListView).ItemAppearing)

    let ItemDisappearing =
        Attributes.Component.defineEvent<ItemVisibilityEventArgs> "ListViewComponent_ItemDisappearing" (fun target -> (target :?> ListView).ItemDisappearing)

    let ItemSelected =
        Attributes.Component.defineEvent<SelectedItemChangedEventArgs> "ListViewComponent_ItemSelected" (fun target -> (target :?> ListView).ItemSelected)

    let ItemTapped =
        Attributes.Component.defineEvent<ItemTappedEventArgs> "ListViewComponent_ItemTapped" (fun target -> (target :?> ListView).ItemTapped)

    let Refreshing =
        Attributes.Component.defineEventNoArg "ListViewComponent_Refreshing" (fun target -> (target :?> ListView).Refreshing)

    let Scrolled =
        Attributes.Component.defineEvent<ScrolledEventArgs> "ListViewComponent_Scrolled" (fun target -> (target :?> ListView).Scrolled)

    let ScrollToRequested =
        Attributes.Component.defineEvent<ScrollToRequestedEventArgs> "ListViewComponent_ScrollToRequested" (fun target -> (target :?> ListView).ScrollToRequested)

[<Extension>]
type ListViewComponentModifiers =
    /// <summary>Listen for the ItemAppearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemAppearing(this: WidgetBuilder<unit, #IFabListView>, fn: ItemVisibilityEventArgs -> unit) =
        this.AddScalar(ListViewComponent.ItemAppearing.WithValue(fn))

    /// <summary>Listen for the ItemDisappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemDisappearing(this: WidgetBuilder<unit, #IFabListView>, fn: ItemVisibilityEventArgs -> unit) =
        this.AddScalar(ListViewComponent.ItemDisappearing.WithValue(fn))

    /// <summary>Listen for the ItemTapped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemTapped(this: WidgetBuilder<unit, #IFabListView>, fn: int -> unit) =
        this.AddScalar(ListViewComponent.ItemTapped.WithValue(fun args -> fn args.ItemIndex))

    /// <summary>Listen for the ItemSelected event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemSelected(this: WidgetBuilder<unit, #IFabListView>, fn: int -> unit) =
        this.AddScalar(ListViewComponent.ItemSelected.WithValue(fun args -> fn args.SelectedItemIndex))

    /// <summary>Listen for the Refreshing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onRefreshing(this: WidgetBuilder<unit, #IFabListView>, fn: unit -> unit) =
        this.AddScalar(ListViewComponent.Refreshing.WithValue(fn))

    /// <summary>Listen for the Scrolled event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrolled(this: WidgetBuilder<unit, #IFabListView>, fn: ScrolledEventArgs -> unit) =
        this.AddScalar(ListViewComponent.Scrolled.WithValue(fn))

    /// <summary>Listen for the ScrollToRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrollToRequested(this: WidgetBuilder<unit, #IFabListView>, fn: ScrollToRequestedEventArgs -> unit) =
        this.AddScalar(ListViewComponent.ScrollToRequested.WithValue(fn))
