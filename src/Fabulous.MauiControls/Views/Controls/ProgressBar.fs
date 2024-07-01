namespace Fabulous.Maui

open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Controls
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Graphics

type IFabProgressBar =
    inherit IFabView

[<Struct>]
type ProgressToData =
    { Progress: float
      AnimationDuration: uint32
      Easing: Easing }

module ProgressBar =
    let WidgetKey = Widgets.register<ProgressBar>()

    let Progress = Attributes.defineBindableFloat ProgressBar.ProgressProperty

    let ProgressColor = Attributes.defineBindableColor ProgressBar.ProgressColorProperty

module ProgressBarAnimations =
    let ProgressTo =
        Attributes.defineSimpleScalarWithEquality<ProgressToData> "ProgressBar_ProgressTo" (fun _ newValueOpt node ->
            let view = node.Target :?> ProgressBar

            match newValueOpt with
            | ValueNone -> view.ProgressTo(0., uint32 0, Easing.Linear) |> ignore
            | ValueSome data -> view.ProgressTo(data.Progress, data.AnimationDuration, data.Easing) |> ignore)

[<Extension>]
type ProgressBarModifiers =
    /// <summary>Set the color of the progress bar</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the progress bar</param>
    [<Extension>]
    static member inline progressColor(this: WidgetBuilder<'msg, #IFabProgressBar>, value: Color) =
        this.AddScalar(ProgressBar.ProgressColor.WithValue(value))
