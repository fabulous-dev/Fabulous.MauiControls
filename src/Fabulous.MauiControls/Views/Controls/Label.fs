namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabLabel =
    inherit IFabView

module Label =
    let WidgetKey = Widgets.register<Label>()

    let CharacterSpacing = Attributes.defineBindableFloat Label.CharacterSpacingProperty

    let FontAttributes =
        Attributes.defineBindableEnum<Microsoft.Maui.Controls.FontAttributes> Label.FontAttributesProperty

    let FontFamily =
        Attributes.defineBindableWithEquality<string> Label.FontFamilyProperty

    let FontSize = Attributes.defineBindableFloat Label.FontSizeProperty

    let HorizontalTextAlignment =
        Attributes.defineBindableEnum<TextAlignment> Label.HorizontalTextAlignmentProperty

    let LineBreakMode =
        Attributes.defineBindableEnum<Microsoft.Maui.LineBreakMode> Label.LineBreakModeProperty

    let LineHeight = Attributes.defineBindableFloat Label.LineHeightProperty

    let MaxLines = Attributes.defineBindableInt Label.MaxLinesProperty

    let Padding = Attributes.defineBindableWithEquality<Thickness> Label.PaddingProperty

    let TextColor = Attributes.defineBindableAppThemeColor Label.TextColorProperty

    let TextDecorations =
        Attributes.defineBindableEnum<TextDecorations> Label.TextDecorationsProperty

    let Text = Attributes.defineBindableWithEquality<string> Label.TextProperty

    let TextTransform =
        Attributes.defineBindableEnum<TextTransform> Label.TextTransformProperty

    let TextType = Attributes.defineBindableEnum<TextType> Label.TextTypeProperty

    let VerticalTextAlignment =
        Attributes.defineBindableEnum<TextAlignment> Label.VerticalTextAlignmentProperty

    let FontAutoScalingEnabled =
        Attributes.defineBindableBool Label.FontAutoScalingEnabledProperty


[<AutoOpen>]
module LabelBuilders =
    type Fabulous.Maui.View with

        static member inline Label<'msg>(text: string) =
            WidgetBuilder<'msg, IFabLabel>(Label.WidgetKey, Label.Text.WithValue(text))

[<Extension>]
type LabelModifiers =

    [<Extension>]
    static member inline characterSpacing(this: WidgetBuilder<'msg, #IFabLabel>, value: float) =
        this.AddScalar(Label.CharacterSpacing.WithValue(value))

    [<Extension>]
    static member inline font
        (
            this: WidgetBuilder<'msg, #IFabLabel>,
            ?size: float,
            ?attributes: FontAttributes,
            ?fontFamily: string,
            ?autoScalingEnabled: bool
        ) =

        let mutable res = this

        match size with
        | None -> ()
        | Some v -> res <- res.AddScalar(Label.FontSize.WithValue(v))

        match attributes with
        | None -> ()
        | Some v -> res <- res.AddScalar(Label.FontAttributes.WithValue(v))

        match fontFamily with
        | None -> ()
        | Some v -> res <- res.AddScalar(Label.FontFamily.WithValue(v))

        match autoScalingEnabled with
        | None -> ()
        | Some v -> res <- res.AddScalar(Label.FontAutoScalingEnabled.WithValue(v))

        res

    [<Extension>]
    static member inline horizontalTextAlignment(this: WidgetBuilder<'msg, #IFabLabel>, value: TextAlignment) =
        this.AddScalar(Label.HorizontalTextAlignment.WithValue(value))

    [<Extension>]
    static member inline lineBreakMode(this: WidgetBuilder<'msg, #IFabLabel>, value: LineBreakMode) =
        this.AddScalar(Label.LineBreakMode.WithValue(value))

    [<Extension>]
    static member inline lineHeight(this: WidgetBuilder<'msg, #IFabLabel>, value: float) =
        this.AddScalar(Label.LineHeight.WithValue(value))

    [<Extension>]
    static member inline maxLines(this: WidgetBuilder<'msg, #IFabLabel>, value: int) =
        this.AddScalar(Label.MaxLines.WithValue(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabLabel>, value: Thickness) =
        this.AddScalar(Label.Padding.WithValue(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabLabel>, value: float) =
        LabelModifiers.padding(this, Thickness(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabLabel>, left: float, top: float, right: float, bottom: float) =
        LabelModifiers.padding(this, Thickness(left, top, right, bottom))

    [<Extension>]
    static member inline textColor(this: WidgetBuilder<'msg, #IFabLabel>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(Label.TextColor.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline textDecorations(this: WidgetBuilder<'msg, #IFabLabel>, value: TextDecorations) =
        this.AddScalar(Label.TextDecorations.WithValue(value))

    [<Extension>]
    static member inline textTransform(this: WidgetBuilder<'msg, #IFabLabel>, value: TextTransform) =
        this.AddScalar(Label.TextTransform.WithValue(value))

    [<Extension>]
    static member inline textType(this: WidgetBuilder<'msg, #IFabLabel>, value: TextType) =
        this.AddScalar(Label.TextType.WithValue(value))

    [<Extension>]
    static member inline verticalTextAlignment(this: WidgetBuilder<'msg, #IFabLabel>, value: TextAlignment) =
        this.AddScalar(Label.VerticalTextAlignment.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Label control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabLabel>, value: ViewRef<Label>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type LabelExtraModifiers =
    [<Extension>]
    static member inline alignStartTextHorizontal(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.horizontalTextAlignment(TextAlignment.Start)

    [<Extension>]
    static member inline alignStartTextVertical(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.verticalTextAlignment(TextAlignment.Start)

    [<Extension>]
    static member inline alignStartText(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.alignStartTextHorizontal().alignStartTextVertical()

    [<Extension>]
    static member inline centerTextHorizontal(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.horizontalTextAlignment(TextAlignment.Center)

    [<Extension>]
    static member inline centerTextVertical(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.verticalTextAlignment(TextAlignment.Center)

    [<Extension>]
    static member inline centerText(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.centerTextHorizontal().centerTextVertical()

    [<Extension>]
    static member inline alignEndTextHorizontal(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.horizontalTextAlignment(TextAlignment.End)

    [<Extension>]
    static member inline alignEndTextVertical(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.verticalTextAlignment(TextAlignment.End)

    [<Extension>]
    static member inline alignEndText(this: WidgetBuilder<'msg, #IFabLabel>) =
        this.alignEndTextHorizontal().alignEndTextVertical()
