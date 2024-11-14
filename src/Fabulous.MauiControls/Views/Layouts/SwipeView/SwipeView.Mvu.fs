namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module SwipeViewMvu =
    let CloseRequested =
        Attributes.Mvu.defineEvent<CloseRequestedEventArgs> "SwipeViewMvu_CloseRequested" (fun target -> (target :?> SwipeView).CloseRequested)

    let OpenRequested =
        Attributes.Mvu.defineEvent<OpenRequestedEventArgs> "SwipeViewMvu_OpenRequested" (fun target -> (target :?> SwipeView).OpenRequested)

    let SwipeChanging =
        Attributes.Mvu.defineEvent<SwipeChangingEventArgs> "SwipeViewMvu_SwipeChanging" (fun target -> (target :?> SwipeView).SwipeChanging)

    let SwipeEnded =
        Attributes.Mvu.defineEvent<SwipeEndedEventArgs> "SwipeViewMvu_SwipeEnded" (fun target -> (target :?> SwipeView).SwipeEnded)

    let SwipeStarted =
        Attributes.Mvu.defineEvent<SwipeStartedEventArgs> "SwipeViewMvu_SwipeStarted" (fun target -> (target :?> SwipeView).SwipeStarted)

[<Extension>]
type SwipeViewMvuModifiers() =
    /// <summary>Listen to the SwipeStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeStarted(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeStartedEventArgs -> 'msg) =
        this.AddScalar(SwipeViewMvu.SwipeStarted.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the SwipeChanging event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeChanging(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeChangingEventArgs -> 'msg) =
        this.AddScalar(SwipeViewMvu.SwipeChanging.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the SwipeEnded event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSwipeEnded(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: SwipeEndedEventArgs -> 'msg) =
        this.AddScalar(SwipeViewMvu.SwipeEnded.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the OpenRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onOpenRequested(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: OpenRequestedEventArgs -> 'msg) =
        this.AddScalar(SwipeViewMvu.OpenRequested.WithValue(fun args -> fn args |> box))

    /// <summary>Listen to the CloseRequested event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onCloseRequested(this: WidgetBuilder<'msg, #IFabSwipeView>, fn: CloseRequestedEventArgs -> 'msg) =
        this.AddScalar(SwipeViewMvu.CloseRequested.WithValue(fun args -> fn args |> box))
