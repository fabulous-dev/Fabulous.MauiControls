namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabSwipeView =
    inherit IFabContentView

module SwipeView =
    let WidgetKey = Widgets.register<SwipeView>()

    let BottomItems = Attributes.defineBindableWidget SwipeView.BottomItemsProperty

    let LeftItems = Attributes.defineBindableWidget SwipeView.LeftItemsProperty

    let RightItems = Attributes.defineBindableWidget SwipeView.RightItemsProperty

    let Threshold = Attributes.defineBindableInt SwipeView.ThresholdProperty

    let TopItems = Attributes.defineBindableWidget SwipeView.TopItemsProperty

[<AutoOpen>]
module SwipeViewBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a SwipeView widget with a content</summary>
        /// <param name="content">The content widget</param>
        static member inline SwipeView(content: WidgetBuilder<'msg, #IFabView>) =
            WidgetHelpers.buildWidgets<'msg, IFabSwipeView> SwipeView.WidgetKey [| ContentView.Content.WithValue(content.Compile()) |]

[<Extension>]
type SwipeViewModifiers() =
    /// <summary>Set the bottom swipe items</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The SwipeItems widget</param>
    [<Extension>]
    static member inline bottomItems(this: WidgetBuilder<'msg, #IFabSwipeView>, content: WidgetBuilder<'msg, #IFabSwipeItems>) =
        this.AddWidget(SwipeView.BottomItems.WithValue(content.Compile()))

    /// <summary>Set the left swipe items</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The SwipeItems widget</param>
    [<Extension>]
    static member inline leftItems(this: WidgetBuilder<'msg, #IFabSwipeView>, content: WidgetBuilder<'msg, #IFabSwipeItems>) =
        this.AddWidget(SwipeView.LeftItems.WithValue(content.Compile()))

    /// <summary>Set the right swipe items</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The SwipeItems widget</param>
    [<Extension>]
    static member inline rightItems(this: WidgetBuilder<'msg, #IFabSwipeView>, content: WidgetBuilder<'msg, #IFabSwipeItems>) =
        this.AddWidget(SwipeView.RightItems.WithValue(content.Compile()))

    /// <summary>Set the swipe threshold</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The threshold value</param>
    [<Extension>]
    static member inline threshold(this: WidgetBuilder<'msg, #IFabSwipeView>, value: int) =
        this.AddScalar(SwipeView.Threshold.WithValue(value))

    /// <summary>Set the top swipe items</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The SwipeItems widget</param>
    [<Extension>]
    static member inline topItems(this: WidgetBuilder<'msg, #IFabSwipeView>, content: WidgetBuilder<'msg, #IFabSwipeItems>) =
        this.AddWidget(SwipeView.TopItems.WithValue(content.Compile()))

    /// <summary>Link a ViewRef to access the direct SwipeView control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSwipeView>, value: ViewRef<SwipeView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
