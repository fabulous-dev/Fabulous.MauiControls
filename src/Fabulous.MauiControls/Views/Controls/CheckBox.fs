namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabCheckBox =
    inherit IFabView

module CheckBox =
    let WidgetKey = Widgets.register<CheckBox>()

    let Color = Attributes.defineBindableColor CheckBox.ColorProperty

[<Extension>]
type CheckBoxModifiers =
    /// <summary>Set the color of the check box</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the check box</param>
    [<Extension>]
    static member inline color(this: WidgetBuilder<'msg, #IFabCheckBox>, value: Color) =
        this.AddScalar(CheckBox.Color.WithValue(value))
