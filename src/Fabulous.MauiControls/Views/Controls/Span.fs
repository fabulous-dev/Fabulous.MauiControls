namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabSpan =
    inherit IFabElement

module Span =
    let WidgetKey = Widgets.register<Span>()

    let BackgroundColor =
        Attributes.defineBindableAppThemeColor Span.BackgroundColorProperty

    let CharacterSpacing = Attributes.defineBindableFloat Span.CharacterSpacingProperty

    let FontAttributes =
        Attributes.defineBindableEnum<FontAttributes> Span.FontAttributesProperty

    let FontFamily =
        Attributes.defineBindableWithEquality<string> Span.FontFamilyProperty

    let FontSize = Attributes.defineBindableFloat Span.FontSizeProperty

    let LineHeight = Attributes.defineBindableFloat Span.LineHeightProperty

    let Style = Attributes.defineBindableWithEquality<Style> Span.StyleProperty

    let TextColor = Attributes.defineBindableAppThemeColor Span.TextColorProperty

    let TextDecorations =
        Attributes.defineBindableEnum<TextDecorations> Span.TextDecorationsProperty

    let TextTransform =
        Attributes.defineBindableEnum<TextTransform> Span.TextTransformProperty

    let Text = Attributes.defineBindableWithEquality<string> Span.TextProperty

    let FontAutoScalingEnabled =
        Attributes.defineBindableBool Span.FontAutoScalingEnabledProperty

    let GestureRecognizers =
        Attributes.defineListWidgetCollection<IGestureRecognizer> "Span_GestureRecognizers" (fun target -> (target :?> Span).GestureRecognizers)

[<AutoOpen>]
module SpanBuilders =
    type Fabulous.Maui.View with

        static member inline Span<'msg>(text: string) =
            WidgetBuilder<'msg, IFabSpan>(Span.WidgetKey, Span.Text.WithValue(text))

[<Extension>]
type SpanModifiers =

    [<Extension>]
    static member inline backgroundColor(this: WidgetBuilder<'msg, #IFabSpan>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(Span.BackgroundColor.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline characterSpacing(this: WidgetBuilder<'msg, #IFabSpan>, value: float) =
        this.AddScalar(Span.CharacterSpacing.WithValue(value))

    [<Extension>]
    static member inline font(this: WidgetBuilder<'msg, #IFabSpan>, ?size: float, ?attributes: FontAttributes, ?fontFamily: string, ?autoScalingEnabled: bool) =

        let mutable res = this

        match size with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontSize.WithValue(v))

        match attributes with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontAttributes.WithValue(v))

        match fontFamily with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontFamily.WithValue(v))

        match autoScalingEnabled with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontAutoScalingEnabled.WithValue(v))

        res

    [<Extension>]
    static member inline lineHeight(this: WidgetBuilder<'msg, #IFabSpan>, value: float) =
        this.AddScalar(Span.LineHeight.WithValue(value))

    [<Extension>]
    static member inline style(this: WidgetBuilder<'msg, #IFabSpan>, value: Style) =
        this.AddScalar(Span.Style.WithValue(value))

    [<Extension>]
    static member inline textColor(this: WidgetBuilder<'msg, #IFabSpan>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(Span.TextColor.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline textDecorations(this: WidgetBuilder<'msg, #IFabSpan>, value: TextDecorations) =
        this.AddScalar(Span.TextDecorations.WithValue(value))

    [<Extension>]
    static member inline textTransform(this: WidgetBuilder<'msg, #IFabSpan>, value: TextTransform) =
        this.AddScalar(Span.TextTransform.WithValue(value))

    [<Extension>]
    static member inline gestureRecognizers<'msg, 'marker when 'marker :> IFabSpan>(this: WidgetBuilder<'msg, 'marker>) =
        WidgetHelpers.buildAttributeCollection<'msg, 'marker, IFabGestureRecognizer> Span.GestureRecognizers this

    /// <summary>Link a ViewRef to access the direct Span control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSpan>, value: ViewRef<Span>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type SpanYieldExtensions =
    [<Extension>]
    static member inline Yield
        (
            _: AttributeCollectionBuilder<'msg, #IFabSpan, IFabGestureRecognizer>,
            x: WidgetBuilder<'msg, #IFabGestureRecognizer>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: AttributeCollectionBuilder<'msg, #IFabSpan, IFabGestureRecognizer>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabGestureRecognizer>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
