namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabSwipeView =
    inherit IFabContentView

module SwipeView =
    let WidgetKey = Widgets.register<SwipeView>()

    let BottomSwipeItems = Attributes.defineBindableWidget SwipeView.BottomItemsProperty

    let CloseRequestedMsg =
        Attributes.defineEvent<CloseRequestedEventArgs> "SwipeView_CloseRequestedMsg" (fun target -> (target :?> SwipeView).CloseRequested)

    let CloseRequestedFn =
        Attributes.defineEventNoDispatch<CloseRequestedEventArgs> "SwipeView_CloseRequestedFn" (fun target -> (target :?> SwipeView).CloseRequested)

    let LeftSwipeItems = Attributes.defineBindableWidget SwipeView.LeftItemsProperty

    let OpenRequestedMsg =
        Attributes.defineEvent<OpenRequestedEventArgs> "SwipeView_OpenRequestedMsg" (fun target -> (target :?> SwipeView).OpenRequested)

    let OpenRequestedFn =
        Attributes.defineEventNoDispatch<OpenRequestedEventArgs> "SwipeView_OpenRequestedFn" (fun target -> (target :?> SwipeView).OpenRequested)

    let RightSwipeItems = Attributes.defineBindableWidget SwipeView.RightItemsProperty

    let SwipeChangingMsg =
        Attributes.defineEvent<SwipeChangingEventArgs> "SwipeView_SwipeChangingMsg" (fun target -> (target :?> SwipeView).SwipeChanging)

    let SwipeChangingFn =
        Attributes.defineEventNoDispatch<SwipeChangingEventArgs> "SwipeView_SwipeChangingFn" (fun target -> (target :?> SwipeView).SwipeChanging)

    let SwipeEndedMsg =
        Attributes.defineEvent<SwipeEndedEventArgs> "SwipeView_SwipeEndedMsg" (fun target -> (target :?> SwipeView).SwipeEnded)

    let SwipeEndedFn =
        Attributes.defineEventNoDispatch<SwipeEndedEventArgs> "SwipeView_SwipeEndedFn" (fun target -> (target :?> SwipeView).SwipeEnded)

    let SwipeStartedMsg =
        Attributes.defineEvent<SwipeStartedEventArgs> "SwipeView_SwipeStartedMsg" (fun target -> (target :?> SwipeView).SwipeStarted)

    let SwipeStartedFn =
        Attributes.defineEventNoDispatch<SwipeStartedEventArgs> "SwipeView_SwipeStartedFn" (fun target -> (target :?> SwipeView).SwipeStarted)

    let SwipeThreshold = Attributes.defineBindableInt SwipeView.ThresholdProperty

    let TopSwipeItems = Attributes.defineBindableWidget SwipeView.TopItemsProperty

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
        this.AddWidget(SwipeView.BottomSwipeItems.WithValue(content.Compile()))

    /// <summary>Set the left swipe items</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The SwipeItems widget</param>
    [<Extension>]
    static member inline leftItems(this: WidgetBuilder<'msg, #IFabSwipeView>, content: WidgetBuilder<'msg, #IFabSwipeItems>) =
        this.AddWidget(SwipeView.LeftSwipeItems.WithValue(content.Compile()))

    /// <summary>Set the right swipe items</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The SwipeItems widget</param>
    [<Extension>]
    static member inline rightItems(this: WidgetBuilder<'msg, #IFabSwipeView>, content: WidgetBuilder<'msg, #IFabSwipeItems>) =
        this.AddWidget(SwipeView.RightSwipeItems.WithValue(content.Compile()))

    /// <summary>Set the swipe threshold</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The threshold value</param>
    [<Extension>]
    static member inline threshold(this: WidgetBuilder<'msg, #IFabSwipeView>, value: int) =
        this.AddScalar(SwipeView.SwipeThreshold.WithValue(value))

    /// <summary>Listen to the SwipeStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeStarted(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeStartedEventArgs -> 'msg) =
        this.AddScalar(SwipeView.SwipeStartedMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the SwipeStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeStarted(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeStartedEventArgs -> unit) =
        this.AddScalar(SwipeView.SwipeStartedFn.WithValue(fn))

    /// <summary>Listen to the SwipeChanging event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeChanging(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeChangingEventArgs -> 'msg) =
        this.AddScalar(SwipeView.SwipeChangingMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the SwipeChanging event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeChanging(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeChangingEventArgs -> unit) =
        this.AddScalar(SwipeView.SwipeChangingFn.WithValue(fn))

    /// <summary>Listen to the SwipeEnded event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeEnded(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeEndedEventArgs -> 'msg) =
        this.AddScalar(SwipeView.SwipeEndedMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the SwipeEnded event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeEnded(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeEndedEventArgs -> unit) =
        this.AddScalar(SwipeView.SwipeEndedFn.WithValue(fn))

    /// <summary>Listen to the OpenRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onOpenRequested(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: OpenRequestedEventArgs -> 'msg) =
        this.AddScalar(SwipeView.OpenRequestedMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the OpenRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onOpenRequested(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: OpenRequestedEventArgs -> unit) =
        this.AddScalar(SwipeView.OpenRequestedFn.WithValue(fn))

    /// <summary>Listen to the CloseRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onCloseRequested(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: CloseRequestedEventArgs -> 'msg) =
        this.AddScalar(SwipeView.CloseRequestedMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the CloseRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onCloseRequested(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: CloseRequestedEventArgs -> unit) =
        this.AddScalar(SwipeView.CloseRequestedFn.WithValue(fn))

    /// <summary>Set the top swipe items</summary>
    /// <param name="this">Current widget</param>
    /// <param name="content">The SwipeItems widget</param>
    [<Extension>]
    static member inline topItems(this: WidgetBuilder<'msg, #IFabSwipeView>, content: WidgetBuilder<'msg, #IFabSwipeItems>) =
        this.AddWidget(SwipeView.TopSwipeItems.WithValue(content.Compile()))

    /// <summary>Link a ViewRef to access the direct SwipeView control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSwipeView>, value: ViewRef<SwipeView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
