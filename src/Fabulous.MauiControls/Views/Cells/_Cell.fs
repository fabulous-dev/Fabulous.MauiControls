namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabCell =
    inherit IFabElement

module Cell =
    let Appearing =
        Attributes.defineEventNoArg "Cell_Appearing" (fun target -> (target :?> Cell).Appearing)

    let Disappearing =
        Attributes.defineEventNoArg "Cell_Disappearing" (fun target -> (target :?> Cell).Disappearing)

    let Height =
        Attributes.defineFloat "Cell_Height" (fun _ newValueOpt node ->
            let cell = node.Target :?> Cell

            let value =
                match newValueOpt with
                | ValueNone -> cell.Height
                | ValueSome v -> v

            cell.Height <- value)
        
    let IsEnabled = Attributes.defineBindableBool Cell.IsEnabledProperty

    let Tapped =
        Attributes.defineEventNoArg "Cell_Tapped" (fun target -> (target :?> Cell).Tapped)

[<Extension>]
type CellModifiers =
    /// <summary>Set a value that indicates whether the cell is enabled</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if the cell is enabled; otherwise, false</param>
    [<Extension>]
    static member inline isEnabled(this: WidgetBuilder<'msg, #IFabCell>, value: bool) =
        this.AddScalar(Cell.IsEnabled.WithValue(value))

    /// <summary>Set the desired height override of this widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The height this widget desires to be</param>
    [<Extension>]
    static member inline height(this: WidgetBuilder<'msg, #IFabCell>, value: float) =
        this.AddScalar(Cell.Height.WithValue(value))

    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<'msg, #IFabCell>, msg: 'msg) =
        this.AddScalar(Cell.Appearing.WithValue(msg))
        
    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<'msg, #IFabCell>, msg: 'msg) =
        this.AddScalar(Cell.Disappearing.WithValue(msg))

    /// <summary>Listen to the Tapped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onTapped(this: WidgetBuilder<'msg, #IFabCell>, msg: 'msg) =
        this.AddScalar(Cell.Tapped.WithValue(msg))
