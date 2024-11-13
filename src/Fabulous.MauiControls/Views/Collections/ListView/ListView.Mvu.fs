namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ListViewMvu =
    let ItemAppearing =
        Attributes.Mvu.defineEvent<ItemVisibilityEventArgs> "ListViewMvu_ItemAppearing" (fun target -> (target :?> ListView).ItemAppearing)

    let ItemDisappearing =
        Attributes.Mvu.defineEvent<ItemVisibilityEventArgs> "ListViewMvu_ItemDisappearing" (fun target -> (target :?> ListView).ItemDisappearing)

    let ItemSelected =
        Attributes.Mvu.defineEvent<SelectedItemChangedEventArgs> "ListViewMvu_ItemSelected" (fun target -> (target :?> ListView).ItemSelected)

    let ItemTapped =
        Attributes.Mvu.defineEvent<ItemTappedEventArgs> "ListViewMvu_ItemTapped" (fun target -> (target :?> ListView).ItemTapped)

    let Refreshing =
        Attributes.Mvu.defineEventNoArg "ListViewMvu_Refreshing" (fun target -> (target :?> ListView).Refreshing)

    let Scrolled =
        Attributes.Mvu.defineEvent<ScrolledEventArgs> "ListViewMvu_Scrolled" (fun target -> (target :?> ListView).Scrolled)

    let ScrollToRequested =
        Attributes.Mvu.defineEvent<ScrollToRequestedEventArgs> "ListViewMvu_ScrollToRequested" (fun target -> (target :?> ListView).ScrollToRequested)

[<Extension>]
type ListViewMvuModifiers =
    /// <summary>Listen for the ItemAppearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemAppearing(this: WidgetBuilder<'msg, #IFabListView>, fn: ItemVisibilityEventArgs -> 'msg) =
        this.AddScalar(ListViewMvu.ItemAppearing.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the ItemDisappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemDisappearing(this: WidgetBuilder<'msg, #IFabListView>, fn: ItemVisibilityEventArgs -> 'msg) =
        this.AddScalar(ListViewMvu.ItemDisappearing.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the ItemTapped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemTapped(this: WidgetBuilder<'msg, #IFabListView>, fn: int -> 'msg) =
        this.AddScalar(ListViewMvu.ItemTapped.WithValue(fun args -> fn args.ItemIndex |> box))

    /// <summary>Listen for the ItemSelected event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onItemSelected(this: WidgetBuilder<'msg, #IFabListView>, fn: int -> 'msg) =
        this.AddScalar(ListViewMvu.ItemSelected.WithValue(fun args -> fn args.SelectedItemIndex |> box))

    /// <summary>Listen for the Refreshing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onRefreshing(this: WidgetBuilder<'msg, #IFabListView>, msg: 'msg) =
        this.AddScalar(ListViewMvu.Refreshing.WithValue(MsgValue(msg)))
        
    /// <summary>Listen for the Scrolled event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrolled(this: WidgetBuilder<'msg, #IFabListView>, fn: ScrolledEventArgs -> 'msg) =
        this.AddScalar(ListViewMvu.Scrolled.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the ScrollToRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrollToRequested(this: WidgetBuilder<'msg, #IFabListView>, fn: ScrollToRequestedEventArgs -> 'msg) =
        this.AddScalar(ListViewMvu.ScrollToRequested.WithValue(fun args -> fn args |> box))
