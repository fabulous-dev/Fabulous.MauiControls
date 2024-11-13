namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module PageMvu =
    let Appearing =
        Attributes.Mvu.defineEventNoArg "PageMvu_Appearing" (fun target -> (target :?> Page).Appearing)

    let Disappearing =
        Attributes.Mvu.defineEventNoArg "PageMvu_Disappearing" (fun target -> (target :?> Page).Disappearing)

    let NavigatedTo =
        Attributes.Mvu.defineEvent "PageMvu_NavigatedTo" (fun target -> (target :?> Page).NavigatedTo)

    let NavigatedFrom =
        Attributes.Mvu.defineEvent "PageMvu_NavigatedFrom" (fun target -> (target :?> Page).NavigatedFrom)

[<Extension>]
type PageMvuModifiers =
    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(PageMvu.Appearing.WithValue(MsgValue(msg)))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(PageMvu.Disappearing.WithValue(MsgValue(msg)))

    [<Extension>]
    static member inline onNavigatedTo(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(PageMvu.NavigatedTo.WithValue(fun _ -> msg))

    [<Extension>]
    static member inline onNavigatedFrom(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(PageMvu.NavigatedFrom.WithValue(fun _ -> msg))
