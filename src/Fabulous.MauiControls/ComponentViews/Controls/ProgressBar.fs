namespace Fabulous.Maui.Components

open Microsoft.Maui
open Microsoft.Maui.Controls
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui

type IFabComponentProgressBar =
    inherit IFabComponentView
    inherit IFabProgressBar

[<AutoOpen>]
module ProgressBarBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a ProgressBar widget with a current progress value</summary>
        /// <param name="progress">The progress value</param>
        static member inline ProgressBar<'msg>(progress: float) =
            WidgetBuilder<'msg, IFabComponentProgressBar>(ProgressBar.WidgetKey, ProgressBar.Progress.WithValue(progress))

        /// <summary>Create a ProgressBar widget with a progress value that will animate when changed</summary>
        /// <param name="progress">The progress value</param>
        /// <param name="duration">The duration of the animation</param>
        /// <param name="easing">The easing of the animation</param>
        static member inline ProgressBar<'msg>(progress: float, duration: int, easing: Easing) =
            WidgetBuilder<'msg, IFabComponentProgressBar>(
                ProgressBar.WidgetKey,
                ProgressBarAnimations.ProgressTo.WithValue(
                    { Progress = progress
                      AnimationDuration = uint32 duration
                      Easing = easing }
                )
            )

[<Extension>]
type ProgressBarModifiers =
    /// <summary>Link a ViewRef to access the direct ProgressBar control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentProgressBar>, value: ViewRef<ProgressBar>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
