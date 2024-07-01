namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabActivityIndicator =
    inherit IFabView

module ActivityIndicator =
    let WidgetKey = Widgets.register<ActivityIndicator>()

    let Color = Attributes.defineBindableColor ActivityIndicator.ColorProperty

    let IsRunning = Attributes.defineBindableBool ActivityIndicator.IsRunningProperty

[<Extension>]
type ActivityIndicatorModifiers =
    /// <summary>Set the activity indicator color</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the activity indicator</param>
    [<Extension>]
    static member inline color(this: WidgetBuilder<'msg, #IFabActivityIndicator>, value: Color) =
        this.AddScalar(ActivityIndicator.Color.WithValue(value))
