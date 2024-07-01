namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabSwitch =
    inherit IFabView

module Switch =
    let WidgetKey = Widgets.register<Switch>()

    let ColorOn = Attributes.defineBindableColor Switch.OnColorProperty

    let ThumbColor = Attributes.defineBindableColor Switch.ThumbColorProperty

[<Extension>]
type SwitchModifiers =
    /// <summary>Set the color of the on state</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the on state</param>
    [<Extension>]
    static member inline colorOn(this: WidgetBuilder<'msg, #IFabSwitch>, value: Color) =
        this.AddScalar(Switch.ColorOn.WithValue(value))

    /// <summary>Set the color of the thumb</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the thumb</param>
    [<Extension>]
    static member inline thumbColor(this: WidgetBuilder<'msg, #IFabSwitch>, value: Color) =
        this.AddScalar(Switch.ThumbColor.WithValue(value))
