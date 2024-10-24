namespace Fabulous.Maui

open System
open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabSlider =
    inherit IFabView

module SliderUpdaters =
    let updateSliderMinMax _ (newValueOpt: struct (float * float) voption) (node: IViewNode) =
        let slider = node.Target :?> Slider

        match newValueOpt with
        | ValueNone ->
            slider.ClearValue(Slider.MinimumProperty)
            slider.ClearValue(Slider.MaximumProperty)
        | ValueSome(min, max) ->
            let currMax = slider.GetValue(Slider.MaximumProperty) :?> float

            if min > currMax then
                slider.SetValue(Slider.MaximumProperty, max)
                slider.SetValue(Slider.MinimumProperty, min)
            else
                slider.SetValue(Slider.MinimumProperty, min)
                slider.SetValue(Slider.MaximumProperty, max)

module Slider =
    let WidgetKey = Widgets.register<Slider>()

    let DragCompletedMsg =
        Attributes.defineEventNoArg "Slider_DragCompletedMsg" (fun target -> (target :?> Slider).DragCompleted)

    let DragCompletedFn =
        Attributes.defineEventNoArgNoDispatch "Slider_DragCompletedFn" (fun target -> (target :?> Slider).DragCompleted)

    let DragStartedMsg =
        Attributes.defineEventNoArg "Slider_DragStartedMsg" (fun target -> (target :?> Slider).DragStarted)

    let DragStartedFn =
        Attributes.defineEventNoArgNoDispatch "Slider_DragStartedFn" (fun target -> (target :?> Slider).DragStarted)

    let MaximumTrackColor =
        Attributes.defineBindableColor Slider.MaximumTrackColorProperty

    let MinimumMaximum =
        Attributes.defineSimpleScalarWithEquality<struct (float * float)> "Slider_MinimumMaximum" SliderUpdaters.updateSliderMinMax

    let MinimumTrackColor =
        Attributes.defineBindableColor Slider.MinimumTrackColorProperty

    let ThumbColor = Attributes.defineBindableColor Slider.ThumbColorProperty

    let ThumbImageSource =
        Attributes.defineBindableImageSource Slider.ThumbImageSourceProperty

    let ValueWithEventMsg =
        Attributes.defineBindableWithEvent "Slider_ValueWithEventMsg" Slider.ValueProperty (fun target -> (target :?> Slider).ValueChanged)

    let ValueWithEventFn =
        Attributes.defineBindableWithEventNoDispatch "Slider_ValueWithEventFn" Slider.ValueProperty (fun target -> (target :?> Slider).ValueChanged)

[<AutoOpen>]
module SliderBuilders =
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
                Slider.ValueWithEventMsg.WithValue(MsgValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )

        /// <summary>Create a Slider widget with a min/max bounds and a value, listen for the value changes</summary>
        /// <param name="min">The minimum bound</param>
        /// <param name="max">The maximum bound</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Slider(min: float, max: float, value: float, onValueChanged: float -> unit) =
            WidgetBuilder<'msg, IFabSlider>(
                Slider.WidgetKey,
                Slider.MinimumMaximum.WithValue(struct (min, max)),
                Slider.ValueWithEventFn.WithValue(ValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )

[<Extension>]
type SliderModifiers =
    /// <summary>Listen for the DragCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragCompleted(this: WidgetBuilder<'msg, #IFabSlider>, msg: 'msg) =
        this.AddScalar(Slider.DragCompletedMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the DragCompleted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDragCompleted(this: WidgetBuilder<'msg, #IFabSlider>, fn: unit -> unit) =
        this.AddScalar(Slider.DragCompletedFn.WithValue(fn))

    /// <summary>Listen for the DragStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDragStarted(this: WidgetBuilder<'msg, #IFabSlider>, msg: 'msg) =
        this.AddScalar(Slider.DragStartedMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the DragStarted event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDragStarted(this: WidgetBuilder<'msg, #IFabSlider>, fn: unit -> unit) =
        this.AddScalar(Slider.DragStartedFn.WithValue(fn))

    /// <summary>Set the color of the maximum track</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the maximum track</param>
    [<Extension>]
    static member inline maximumTrackColor(this: WidgetBuilder<'msg, #IFabSlider>, value: Color) =
        this.AddScalar(Slider.MaximumTrackColor.WithValue(value))

    /// <summary>Set the color of the minimum track</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the minimum track</param>
    [<Extension>]
    static member inline minimumTrackColor(this: WidgetBuilder<'msg, #IFabSlider>, value: Color) =
        this.AddScalar(Slider.MinimumTrackColor.WithValue(value))

    /// <summary>Set the color of the thumb</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the thumb</param>
    [<Extension>]
    static member inline thumbColor(this: WidgetBuilder<'msg, #IFabSlider>, value: Color) =
        this.AddScalar(Slider.ThumbColor.WithValue(value))

    /// <summary>Set the image source of the thumb</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source of the thumb</param>
    [<Extension>]
    static member inline thumbImage(this: WidgetBuilder<'msg, #IFabSlider>, value: ImageSource) =
        this.AddScalar(Slider.ThumbImageSource.WithValue(ImageSourceValue.Source value))

    /// <summary>Link a ViewRef to access the direct Slider control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSlider>, value: ViewRef<Slider>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type SliderExtraModifiers =
    /// <summary>Set the image source of the thumb</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source of the thumb</param>
    [<Extension>]
    static member inline thumbImage(this: WidgetBuilder<'msg, #IFabSlider>, value: string) =
        this.AddScalar(Slider.ThumbImageSource.WithValue(ImageSourceValue.File value))

    /// <summary>Set the image source of the thumb</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source of the thumb</param>
    [<Extension>]
    static member inline thumbImage(this: WidgetBuilder<'msg, #IFabSlider>, value: Uri) =
        this.AddScalar(Slider.ThumbImageSource.WithValue(ImageSourceValue.Uri value))

    /// <summary>Set the image source of the thumb</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source of the thumb</param>
    [<Extension>]
    static member inline thumbImage(this: WidgetBuilder<'msg, #IFabSlider>, value: Stream) =
        this.AddScalar(Slider.ThumbImageSource.WithValue(ImageSourceValue.Stream value))
