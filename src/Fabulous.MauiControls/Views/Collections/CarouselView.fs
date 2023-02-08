namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabCarouselView =
    inherit IFabItemsView

module CarouselView =
    let WidgetKey = Widgets.register<CarouselView>()

    let IndicatorView =
        Attributes.defineSimpleScalarWithEquality<ViewRef<IndicatorView>> "CarouselView_IndicatorView" (fun oldValueOpt newValueOpt node ->
            let handlerOpt =
                node.TryGetHandler<EventHandler<IndicatorView>>(ViewRefAttributes.ViewRef.Name)

            // Clean up previous handler
            if handlerOpt.IsSome then
                match struct (oldValueOpt, newValueOpt) with
                | struct (ValueSome prev, _) -> prev.Attached.RemoveHandler(handlerOpt.Value)

                | struct (ValueNone, ValueSome curr) ->
                    // Despite not having a previous value, we might still be reusing the same ViewRef
                    // So we still need to clean up
                    curr.Attached.RemoveHandler(handlerOpt.Value)

                | struct (ValueNone, ValueNone) -> ()

                node.SetHandler(ViewRefAttributes.ViewRef.Name, ValueNone)

            let handler =
                match handlerOpt with
                | ValueSome handler -> handler
                | ValueNone ->
                    let newHandler =
                        EventHandler<IndicatorView>(fun viewRef indicatorView ->
                            let carouselView = node.Target :?> CarouselView

                            if carouselView <> null then
                                carouselView.IndicatorView <- indicatorView
                            else
                                // CarouselView has been disposed, clean up the handler
                                let handler =
                                    node
                                        .TryGetHandler<EventHandler<IndicatorView>>(ViewRefAttributes.ViewRef.Name)
                                        .Value

                                (viewRef :?> ViewRef<IndicatorView>).Attached.RemoveHandler(handler))

                    newHandler

            match newValueOpt with
            | ValueNone -> node.SetHandler(ViewRefAttributes.ViewRef.Name, ValueNone)
            | ValueSome curr ->
                curr.Attached.AddHandler(handler)
                node.SetHandler(ViewRefAttributes.ViewRef.Name, ValueSome handler))

    let IsBounceEnabled =
        Attributes.defineBindableBool CarouselView.IsBounceEnabledProperty

    let IsDragging = Attributes.defineBindableBool CarouselView.IsDraggingProperty

    let IsScrollAnimated =
        Attributes.defineBindableBool CarouselView.IsScrollAnimatedProperty

    let IsSwipeEnabled =
        Attributes.defineBindableBool CarouselView.IsSwipeEnabledProperty

    let Loop = Attributes.defineBindableBool CarouselView.LoopProperty

    let PeekAreaInsets =
        Attributes.defineBindableWithEquality<Thickness> CarouselView.PeekAreaInsetsProperty

[<AutoOpen>]
module CarouselViewBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a CarouselView widget with a list of items</summary>
        /// <param name="items">The items list</param>
        static member inline CarouselView<'msg, 'itemData, 'itemMarker when 'itemMarker :> IFabView>(items: seq<'itemData>) =
            WidgetHelpers.buildItems<'msg, IFabCarouselView, 'itemData, 'itemMarker> CarouselView.WidgetKey ItemsView.ItemsSource items

[<Extension>]
type CarouselViewModifiers =
    /// <summary>Set the linked indicator view</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The linked indicator view</param>
    [<Extension>]
    static member inline indicatorView(this: WidgetBuilder<'msg, #IFabCarouselView>, value: ViewRef<IndicatorView>) =
        this.AddScalar(CarouselView.IndicatorView.WithValue(value))
        
    /// <summary>Set whether the carousel bounces</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if bounce is enabled; otherwise, false</param>
    [<Extension>]
    static member inline isBounceEnabled(this: WidgetBuilder<'msg, #IFabCarouselView>, value: bool) =
        this.AddScalar(CarouselView.IsBounceEnabled.WithValue(value))

    /// <summary>Set whether the carousel is dragging</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if Dragging is enabled; otherwise, false</param>
    [<Extension>]
    static member inline isDragging(this: WidgetBuilder<'msg, #IFabCarouselView>, value: bool) =
        this.AddScalar(CarouselView.IsDragging.WithValue(value))

    /// <summary>Set whether the scroll is animated</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if scroll is animated; otherwise, false</param>
    [<Extension>]
    static member inline isScrollAnimated(this: WidgetBuilder<'msg, #IFabCarouselView>, value: bool) =
        this.AddScalar(CarouselView.IsScrollAnimated.WithValue(value))

    /// <summary>Set whether swipe is enabled</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if Swipe is enabled; otherwise, false</param>
    [<Extension>]
    static member inline isSwipeEnabled(this: WidgetBuilder<'msg, #IFabCarouselView>, value: bool) =
        this.AddScalar(CarouselView.IsSwipeEnabled.WithValue(value))

    /// <summary>Set whether the carousel loops between items once reaching the end</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if Loop is enabled; otherwise, false</param>
    [<Extension>]
    static member inline loop(this: WidgetBuilder<'msg, #IFabCarouselView>, value: bool) =
        this.AddScalar(CarouselView.Loop.WithValue(value))

    /// <summary>Set the peek area insets</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The peek area insets</param>
    [<Extension>]
    static member inline peekAreaInsets(this: WidgetBuilder<'msg, #IFabCarouselView>, value: Thickness) =
        this.AddScalar(CarouselView.PeekAreaInsets.WithValue(value))

    /// <summary>Link a ViewRef to access the direct CarouselView control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabCarouselView>, value: ViewRef<CarouselView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
      
[<Extension>]
type CarouselViewExtraModifiers =
    /// <summary>Set the peek area insets</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The peek area insets</param>
    [<Extension>]
    static member inline peekAreaInsets(this: WidgetBuilder<'msg, #IFabCarouselView>, value: float) =
        this.peekAreaInsets(Thickness(value))

    /// <summary>Set the peek area insets</summary>
    /// <param name="this">Current widget</param>
    /// <param name="left">The left component of the peek area insets</param>
    /// <param name="top">The top component of the peek area insets</param>
    /// <param name="right">The right component of the peek area insets</param>
    /// <param name="bottom">The bottom component of the peek area insets</param>
    [<Extension>]
    static member inline peekAreaInsets(this: WidgetBuilder<'msg, #IFabCarouselView>, left: float, top: float, right: float, bottom: float) =
        this.peekAreaInsets(Thickness(left, top, right, bottom))
