namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module CellMvu =
    let Appearing =
        Attributes.Mvu.defineEventNoArg "CellMvu_Appearing" (fun target -> (target :?> Cell).Appearing)

    let Disappearing =
        Attributes.Mvu.defineEventNoArg "CellMvu_Disappearing" (fun target -> (target :?> Cell).Disappearing)

    let Tapped =
        Attributes.Mvu.defineEventNoArg "CellMvu_Tapped" (fun target -> (target :?> Cell).Tapped)

[<Extension>]
type CellMvuModifiers =
    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<'msg, #IFabCell>, msg: 'msg) =
        this.AddScalar(CellMvu.Appearing.WithValue(MsgValue(msg)))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<'msg, #IFabCell>, msg: 'msg) =
        this.AddScalar(CellMvu.Disappearing.WithValue(MsgValue(msg)))

    /// <summary>Listen to the Tapped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onTapped(this: WidgetBuilder<'msg, #IFabCell>, msg: 'msg) =
        this.AddScalar(CellMvu.Tapped.WithValue(MsgValue(msg)))
