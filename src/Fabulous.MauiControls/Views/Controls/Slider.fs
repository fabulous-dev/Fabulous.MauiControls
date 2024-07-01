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

    let MaximumTrackColor =
        Attributes.defineBindableColor Slider.MaximumTrackColorProperty

    let MinimumMaximum =
        Attributes.defineSimpleScalarWithEquality<struct (float * float)> "Slider_MinimumMaximum" SliderUpdaters.updateSliderMinMax

    let MinimumTrackColor =
        Attributes.defineBindableColor Slider.MinimumTrackColorProperty

    let ThumbColor = Attributes.defineBindableColor Slider.ThumbColorProperty

    let ThumbImageSource =
        Attributes.defineBindableImageSource Slider.ThumbImageSourceProperty

[<Extension>]
type SliderModifiers =
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
