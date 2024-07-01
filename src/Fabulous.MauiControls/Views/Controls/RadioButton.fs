namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabRadioButton =
    inherit IFabTemplatedView

module RadioButton =
    let WidgetKey = Widgets.register<RadioButton>()

    let BorderColor = Attributes.defineBindableColor RadioButton.BorderColorProperty

    let BorderWidth = Attributes.defineBindableFloat RadioButton.BorderWidthProperty

    let CharacterSpacing =
        Attributes.defineBindableFloat RadioButton.CharacterSpacingProperty

    let CornerRadius = Attributes.defineBindableFloat RadioButton.CornerRadiusProperty

    let ContentString =
        Attributes.defineBindableWithEquality<string> RadioButton.ContentProperty

    let ContentWidget = Attributes.defineBindableWidget RadioButton.ContentProperty

    let FontAttributes =
        Attributes.defineBindableEnum<FontAttributes> RadioButton.FontAttributesProperty

    let FontAutoScalingEnabled =
        Attributes.defineBindableBool RadioButton.FontAutoScalingEnabledProperty

    let FontFamily =
        Attributes.defineBindableWithEquality<string> RadioButton.FontFamilyProperty

    let FontSize = Attributes.defineBindableFloat RadioButton.FontSizeProperty

    let GroupName =
        Attributes.defineBindableWithEquality<string> RadioButton.GroupNameProperty

    let TextColor = Attributes.defineBindableColor RadioButton.TextColorProperty

    let TextTransform =
        Attributes.defineBindableEnum<TextTransform> RadioButton.TextTransformProperty

module RadioButtonAttached =
    let RadioButtonGroupName =
        Attributes.defineBindableWithEquality<string> RadioButtonGroup.GroupNameProperty

[<Extension>]
type RadioButtonModifiers =
    /// <summary>Set the border color of the radio button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The border color of the radio button</param>
    [<Extension>]
    static member inline borderColor(this: WidgetBuilder<'msg, #IFabRadioButton>, value: Color) =
        this.AddScalar(RadioButton.BorderColor.WithValue(value))

    /// <summary>Set the border width of the radio button.</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The border width of the radio button</param>
    [<Extension>]
    static member inline borderWidth(this: WidgetBuilder<'msg, #IFabRadioButton>, value: float) =
        this.AddScalar(RadioButton.BorderWidth.WithValue(value))

    /// <summary>Set the character spacing</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The character spacing</param>
    [<Extension>]
    static member inline characterSpacing(this: WidgetBuilder<'msg, #IFabRadioButton>, value: float) =
        this.AddScalar(RadioButton.CharacterSpacing.WithValue(value))

    /// <summary>Set the corner radius of the radio button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The corner radius of the radio button</param>
    [<Extension>]
    static member inline cornerRadius(this: WidgetBuilder<'msg, #IFabRadioButton>, value: float) =
        this.AddScalar(RadioButton.CornerRadius.WithValue(value))

    /// <summary>Set the name that specifies which RadioButton controls are mutually exclusive</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The name that specifies which RadioButton controls are mutually exclusive</param>
    [<Extension>]
    static member inline groupName(this: WidgetBuilder<'msg, #IFabRadioButton>, value: string) =
        this.AddScalar(RadioButton.GroupName.WithValue(value))

    /// <summary>Set the font</summary>
    /// <param name="this">Current widget</param>
    /// <param name="size">The font size</param>
    /// <param name="attributes">The font attributes</param>
    /// <param name="fontFamily">The font family</param>
    /// <param name="autoScalingEnabled">The value indicating whether auto-scaling is enabled</param>
    [<Extension>]
    static member inline font
        (
            this: WidgetBuilder<'msg, #IFabRadioButton>,
            ?size: float,
            ?attributes: FontAttributes,
            ?fontFamily: string,
            ?autoScalingEnabled: bool
        ) =

        let mutable res = this

        match size with
        | None -> ()
        | Some v -> res <- res.AddScalar(RadioButton.FontSize.WithValue(v))

        match attributes with
        | None -> ()
        | Some v -> res <- res.AddScalar(RadioButton.FontAttributes.WithValue(v))

        match fontFamily with
        | None -> ()
        | Some v -> res <- res.AddScalar(RadioButton.FontFamily.WithValue(v))

        match autoScalingEnabled with
        | None -> ()
        | Some v -> res <- res.AddScalar(RadioButton.FontAutoScalingEnabled.WithValue(v))

        res

    /// <summary>Set the color of the text</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the text</param>
    [<Extension>]
    static member inline textColor(this: WidgetBuilder<'msg, #IFabRadioButton>, value: Color) =
        this.AddScalar(RadioButton.TextColor.WithValue(value))

    /// <summary>Set the transformation of the text</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The transformation of the text</param>
    [<Extension>]
    static member inline textTransform(this: WidgetBuilder<'msg, #IFabRadioButton>, value: TextTransform) =
        this.AddScalar(RadioButton.TextTransform.WithValue(value))

[<Extension>]
type RadioButtonAttachedModifiers =
    /// <summary>Set the group name to apply to all radio buttons inside the layout</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The group name</param>
    [<Extension>]
    static member inline radioButtonGroupName(this: WidgetBuilder<'msg, #IFabLayoutOfView>, value: string) =
        this.AddScalar(RadioButtonAttached.RadioButtonGroupName.WithValue(value))
