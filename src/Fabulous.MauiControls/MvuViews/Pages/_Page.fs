namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuPage =
    inherit IFabPage
    inherit IFabMvuVisualElement

module Page =
    let Appearing =
        Attributes.defineEventNoArg "Page_Appearing" (fun target -> (target :?> Page).Appearing)

    let Disappearing =
        Attributes.defineEventNoArg "Page_Disappearing" (fun target -> (target :?> Page).Disappearing)

    let NavigatedTo =
        Attributes.defineEvent "NavigatedTo" (fun target -> (target :?> Page).NavigatedTo)

    let NavigatedFrom =
        Attributes.defineEvent "NavigatedFrom" (fun target -> (target :?> Page).NavigatedFrom)
        
[<Extension>]
type PageModifiers =
    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<'msg, #IFabMvuPage>, msg: 'msg) =
        this.AddScalar(Page.Appearing.WithValue(MsgValue(msg)))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<'msg, #IFabMvuPage>, msg: 'msg) =
        this.AddScalar(Page.Disappearing.WithValue(MsgValue(msg)))

    [<Extension>]
    static member inline onNavigatedTo(this: WidgetBuilder<'msg, #IFabMvuPage>, msg: 'msg) =
        this.AddScalar(Page.NavigatedTo.WithValue(fun _ -> msg))

    [<Extension>]
    static member inline onNavigatedFrom(this: WidgetBuilder<'msg, #IFabMvuPage>, msg: 'msg) =
        this.AddScalar(Page.NavigatedFrom.WithValue(fun _ -> msg))