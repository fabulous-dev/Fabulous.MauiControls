namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module SliderComponent =
    let DragCompleted =
        Attributes.Component.defineEventNoArg "SliderComponent_DragCompleted" (fun target -> (target :?> Slider).DragCompleted)

    let DragStarted =
        Attributes.Component.defineEventNoArg "SliderComponent_DragStarted" (fun target -> (target :?> Slider).DragStarted)

    let ValueWithEvent =
        Attributes.Component.defineBindableWithEvent "SliderComponent_ValueWithEvent" Slider.ValueProperty (fun target -> (target :?> Slider).ValueChanged)

[<AutoOpen>]
module SliderComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Slider widget with a min/max bounds and a value, listen for the value changes</summary>
        /// <param name="min">The minimum bound</param>
        /// <param name="max">The maximum bound</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Slider(min: float, max: float, value: float, onValueChanged: float -> unit) =
            WidgetBuilder<unit, IFabSlider>(
                Slider.WidgetKey,
                Slider.MinimumMaximum.WithValue(struct (min, max)),
                SliderComponent.ValueWithEvent.WithValue(ValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )

[<Extension>]
type SliderComponentModifiers =
    /// <summary>Listen for the DragCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDragCompleted(this: WidgetBuilder<unit, #IFabSlider>, fn: unit -> unit) =
        this.AddScalar(SliderComponent.DragCompleted.WithValue(fn))

    /// <summary>Listen for the DragStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDragStarted(this: WidgetBuilder<unit, #IFabSlider>, fn: unit -> unit) =
        this.AddScalar(SliderComponent.DragStarted.WithValue(fn))
