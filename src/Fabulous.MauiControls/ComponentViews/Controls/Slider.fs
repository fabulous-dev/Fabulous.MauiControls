namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentSlider =
    inherit IFabComponentView
    inherit IFabSlider

module Slider =
    let DragCompleted =
        ComponentAttributes.defineEventNoArg "Slider_DragCompleted" (fun target -> (target :?> Slider).DragCompleted)

    let DragStarted =
        ComponentAttributes.defineEventNoArg "Slider_DragStarted" (fun target -> (target :?> Slider).DragStarted)

    let ValueWithEvent =
        ComponentAttributes.defineBindableWithEvent "Slider_ValueWithEvent" Slider.ValueProperty (fun target -> (target :?> Slider).ValueChanged)

[<AutoOpen>]
module SliderBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a Slider widget with a min/max bounds and a value, listen for the value changes</summary>
        /// <param name="min">The minimum bound</param>
        /// <param name="max">The maximum bound</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Slider<'msg>(min: float, max: float, value: float, onValueChanged: float -> unit) =
            WidgetBuilder<'msg, IFabComponentSlider>(
                Slider.WidgetKey,
                Slider.MinimumMaximum.WithValue(struct (min, max)),
                Slider.ValueWithEvent.WithValue(ComponentValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )

[<Extension>]
type SliderModifiers =
    /// <summary>Listen for the DragCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragCompleted(this: WidgetBuilder<'msg, #IFabComponentSlider>, fn: unit -> unit) =
        this.AddScalar(Slider.DragCompleted.WithValue(fn))

    /// <summary>Listen for the DragStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragStarted(this: WidgetBuilder<'msg, #IFabComponentSlider>, fn: unit -> unit) =
        this.AddScalar(Slider.DragStarted.WithValue(fn))

    /// <summary>Link a ViewRef to access the direct Slider control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentSlider>, value: ViewRef<Slider>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
