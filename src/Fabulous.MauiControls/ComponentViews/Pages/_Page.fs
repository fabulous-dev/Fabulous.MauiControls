namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentPage =
    inherit IFabPage
    inherit IFabComponentVisualElement

module Page =
    let Appearing =
        ComponentAttributes.defineEventNoArg "Page_Appearing" (fun target -> (target :?> Page).Appearing)

    let Disappearing =
        ComponentAttributes.defineEventNoArg "Page_Disappearing" (fun target -> (target :?> Page).Disappearing)

    let NavigatedTo =
        ComponentAttributes.defineEvent "NavigatedTo" (fun target -> (target :?> Page).NavigatedTo)

    let NavigatedFrom =
        ComponentAttributes.defineEvent "NavigatedFrom" (fun target -> (target :?> Page).NavigatedFrom)

[<Extension>]
type PageModifiers =
    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<'msg, #IFabComponentPage>, fn: unit -> unit) =
        this.AddScalar(Page.Appearing.WithValue(fn))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<'msg, #IFabComponentPage>, fn: unit -> unit) =
        this.AddScalar(Page.Disappearing.WithValue(fn))

    [<Extension>]
    static member inline onNavigatedTo(this: WidgetBuilder<'msg, #IFabComponentPage>, fn: NavigatedToEventArgs -> unit) =
        this.AddScalar(Page.NavigatedTo.WithValue(fn))

    [<Extension>]
    static member inline onNavigatedFrom(this: WidgetBuilder<'msg, #IFabComponentPage>, fn: NavigatedFromEventArgs -> unit) =
        this.AddScalar(Page.NavigatedFrom.WithValue(fn))
