namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module PageComponent =
    let Appearing =
        Attributes.Component.defineEventNoArg "PageComponent_Appearing" (fun target -> (target :?> Page).Appearing)

    let Disappearing =
        Attributes.Component.defineEventNoArg "PageComponent_Disappearing" (fun target -> (target :?> Page).Disappearing)

    let NavigatedTo =
        Attributes.Component.defineEvent "PageComponent_NavigatedTo" (fun target -> (target :?> Page).NavigatedTo)

    let NavigatedFrom =
        Attributes.Component.defineEvent "PageComponent_NavigatedFrom" (fun target -> (target :?> Page).NavigatedFrom)

[<Extension>]
type PageComponentModifiers =
    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<unit, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(PageComponent.Appearing.WithValue(fn))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<unit, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(PageComponent.Disappearing.WithValue(fn))

    [<Extension>]
    static member inline onNavigatedTo(this: WidgetBuilder<unit, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(PageComponent.NavigatedTo.WithValue(fun _ -> fn()))

    [<Extension>]
    static member inline onNavigatedFrom(this: WidgetBuilder<unit, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(PageComponent.NavigatedFrom.WithValue(fun _ -> fn()))
