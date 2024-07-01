namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuSlider =
    inherit IFabMvuView
    inherit IFabSlider

module Slider =
    let DragCompleted =
        MvuAttributes.defineEventNoArg "Slider_DragCompleted" (fun target -> (target :?> Slider).DragCompleted)

    let DragStarted =
        MvuAttributes.defineEventNoArg "Slider_DragStarted" (fun target -> (target :?> Slider).DragStarted)

    let ValueWithEvent =
        MvuAttributes.defineBindableWithEvent "Slider_ValueWithEvent" Slider.ValueProperty (fun target -> (target :?> Slider).ValueChanged)

[<AutoOpen>]
module SliderBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a Slider widget with a min/max bounds and a value, listen for the value changes</summary>
        /// <param name="min">The minimum bound</param>
        /// <param name="max">The maximum bound</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Slider<'msg>(min: float, max: float, value: float, onValueChanged: float -> 'msg) =
            WidgetBuilder<'msg, IFabMvuSlider>(
                Slider.WidgetKey,
                Slider.MinimumMaximum.WithValue(struct (min, max)),
                Slider.ValueWithEvent.WithValue(MvuValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )

[<Extension>]
type SliderModifiers =
    /// <summary>Listen for the DragCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragCompleted(this: WidgetBuilder<'msg, #IFabMvuSlider>, msg: 'msg) =
        this.AddScalar(Slider.DragCompleted.WithValue(MsgValue(msg)))

    /// <summary>Listen for the DragStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragStarted(this: WidgetBuilder<'msg, #IFabMvuSlider>, msg: 'msg) =
        this.AddScalar(Slider.DragStarted.WithValue(MsgValue(msg)))

    /// <summary>Link a ViewRef to access the direct Slider control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuSlider>, value: ViewRef<Slider>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
