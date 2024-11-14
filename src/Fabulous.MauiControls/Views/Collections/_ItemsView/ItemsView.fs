namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabItemsView =
    inherit IFabView

module ItemsView =
    let EmptyView = Attributes.defineBindableWidget ItemsView.EmptyViewProperty

    let HorizontalScrollBarVisibility =
        Attributes.defineBindableEnum<ScrollBarVisibility> ItemsView.HorizontalScrollBarVisibilityProperty

    let ItemsSource =
        Attributes.defineSimpleScalar<WidgetItems>
            "ItemsView_ItemsSource"
            (fun a b -> ScalarAttributeComparers.equalityCompare a.OriginalItems b.OriginalItems)
            (fun _ newValueOpt node ->
                let itemsView = node.Target :?> ItemsView

                match newValueOpt with
                | ValueNone ->
                    itemsView.ClearValue(ItemsView.ItemTemplateProperty)
                    itemsView.ClearValue(ItemsView.ItemsSourceProperty)
                | ValueSome value ->
                    itemsView.SetValue(ItemsView.ItemTemplateProperty, WidgetDataTemplateSelector(node, unbox >> value.Template))

                    itemsView.SetValue(ItemsView.ItemsSourceProperty, value.OriginalItems))

    let ItemsUpdatingScrollMode =
        Attributes.defineBindableEnum<ItemsUpdatingScrollMode> ItemsView.ItemsUpdatingScrollModeProperty

    let RemainingItemsThreshold =
        Attributes.defineBindableInt ItemsView.RemainingItemsThresholdProperty

    let VerticalScrollBarVisibility =
        Attributes.defineBindableEnum<ScrollBarVisibility> ItemsView.VerticalScrollBarVisibilityProperty

[<Extension>]
type ItemsViewModifiers =
    /// <summary>Set the empty view widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The empty view widget</param>
    [<Extension>]
    static member inline emptyView(this: WidgetBuilder<'msg, #IFabItemsView>, content: WidgetBuilder<'msg, #IFabView>) =
        this.AddWidget(ItemsView.EmptyView.WithValue(content.Compile()))

    /// <summary>Set the visibility of the horizontal scroll bar</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if the horizontal scroll is enabled; otherwise, false</param>
    [<Extension>]
    static member inline horizontalScrollBarVisibility(this: WidgetBuilder<'msg, #IFabItemsView>, value: ScrollBarVisibility) =
        this.AddScalar(ItemsView.HorizontalScrollBarVisibility.WithValue(value))

    /// <summary>Set the items updating scroll mode</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The items updating scroll mode</param>
    [<Extension>]
    static member inline itemsUpdatingScrollMode(this: WidgetBuilder<'msg, #IFabItemsView>, value: ItemsUpdatingScrollMode) =
        this.AddScalar(ItemsView.ItemsUpdatingScrollMode.WithValue(value))

    /// <summary>Set the visibility of the vertical scroll bar</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if the vertical scroll is enabled; otherwise, false</param>
    [<Extension>]
    static member inline verticalScrollBarVisibility(this: WidgetBuilder<'msg, #IFabItemsView>, value: ScrollBarVisibility) =
        this.AddScalar(ItemsView.VerticalScrollBarVisibility.WithValue(value))
