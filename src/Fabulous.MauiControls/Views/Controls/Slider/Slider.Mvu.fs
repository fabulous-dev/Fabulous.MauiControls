namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module SliderMvu =
    let DragCompleted =
        Attributes.Mvu.defineEventNoArg "SliderMvu_DragCompleted" (fun target -> (target :?> Slider).DragCompleted)

    let DragStarted =
        Attributes.Mvu.defineEventNoArg "SliderMvu_DragStarted" (fun target -> (target :?> Slider).DragStarted)

    let ValueWithEvent =
        Attributes.Mvu.defineBindableWithEvent "SliderMvu_ValueWithEvent" Slider.ValueProperty (fun target -> (target :?> Slider).ValueChanged)

[<AutoOpen>]
module SliderMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Slider widget with a min/max bounds and a value, listen for the value changes</summary>
        /// <param name="min">The minimum bound</param>
        /// <param name="max">The maximum bound</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Slider(min: float, max: float, value: float, onValueChanged: float -> 'msg) =
            WidgetBuilder<'msg, IFabSlider>(
                Slider.WidgetKey,
                Slider.MinimumMaximum.WithValue(struct (min, max)),
                SliderMvu.ValueWithEvent.WithValue(MsgValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )

[<Extension>]
type SliderMvuModifiers =
    /// <summary>Listen for the DragCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragCompleted(this: WidgetBuilder<'msg, #IFabSlider>, msg: 'msg) =
        this.AddScalar(SliderMvu.DragCompleted.WithValue(MsgValue(msg)))

    /// <summary>Listen for the DragStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragStarted(this: WidgetBuilder<'msg, #IFabSlider>, msg: 'msg) =
        this.AddScalar(SliderMvu.DragStarted.WithValue(MsgValue(msg)))
