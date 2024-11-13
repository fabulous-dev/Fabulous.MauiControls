namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module SwipeViewComponent =
    let CloseRequested =
        Attributes.Component.defineEvent<CloseRequestedEventArgs> "SwipeViewComponent_CloseRequested" (fun target -> (target :?> SwipeView).CloseRequested)

    let OpenRequested =
        Attributes.Component.defineEvent<OpenRequestedEventArgs> "SwipeViewComponent_OpenRequested" (fun target -> (target :?> SwipeView).OpenRequested)

    let SwipeChanging =
        Attributes.Component.defineEvent<SwipeChangingEventArgs> "SwipeViewComponent_SwipeChanging" (fun target -> (target :?> SwipeView).SwipeChanging)

    let SwipeEnded =
        Attributes.Component.defineEvent<SwipeEndedEventArgs> "SwipeViewComponent_SwipeEnded" (fun target -> (target :?> SwipeView).SwipeEnded)

    let SwipeStarted =
        Attributes.Component.defineEvent<SwipeStartedEventArgs> "SwipeViewComponent_SwipeStarted" (fun target -> (target :?> SwipeView).SwipeStarted)

[<Extension>]
type SwipeViewComponentModifiers() =
    /// <summary>Listen to the SwipeStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeStarted(this: WidgetBuilder<unit, #IFabSwipeView>, fn: SwipeStartedEventArgs -> unit) =
        this.AddScalar(SwipeViewComponent.SwipeStarted.WithValue(fn))

    /// <summary>Listen to the SwipeChanging event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeChanging(this: WidgetBuilder<unit, #IFabSwipeView>, fn: SwipeChangingEventArgs -> unit) =
        this.AddScalar(SwipeViewComponent.SwipeChanging.WithValue(fn))

    /// <summary>Listen to the SwipeEnded event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeEnded(this: WidgetBuilder<unit, #IFabSwipeView>, fn: SwipeEndedEventArgs -> unit) =
        this.AddScalar(SwipeViewComponent.SwipeEnded.WithValue(fn))

    /// <summary>Listen to the OpenRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onOpenRequested(this: WidgetBuilder<unit, #IFabSwipeView>, fn: OpenRequestedEventArgs -> unit) =
        this.AddScalar(SwipeViewComponent.OpenRequested.WithValue(fn))

    /// <summary>Listen to the CloseRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onCloseRequested(this: WidgetBuilder<unit, #IFabSwipeView>, fn: CloseRequestedEventArgs -> unit) =
        this.AddScalar(SwipeViewComponent.CloseRequested.WithValue(fn))
