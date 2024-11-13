namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module CellComponent =
    let Appearing =
        Attributes.Component.defineEventNoArg "CellComponent_Appearing" (fun target -> (target :?> Cell).Appearing)

    let Disappearing =
        Attributes.Component.defineEventNoArg "CellComponent_Disappearing" (fun target -> (target :?> Cell).Disappearing)

    let Tapped =
        Attributes.Component.defineEventNoArg "CellComponent_Tapped" (fun target -> (target :?> Cell).Tapped)

[<Extension>]
type CellComponentModifiers =
    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<unit, #IFabCell>, fn: unit -> unit) =
        this.AddScalar(CellComponent.Appearing.WithValue(fn))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<unit, #IFabCell>, fn: unit -> unit) =
        this.AddScalar(CellComponent.Disappearing.WithValue(fn))

    /// <summary>Listen to the Tapped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onTapped(this: WidgetBuilder<unit, #IFabCell>, fn: unit -> unit) =
        this.AddScalar(CellComponent.Tapped.WithValue(fn))
