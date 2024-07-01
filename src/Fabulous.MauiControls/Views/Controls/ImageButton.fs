namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabImageButton =
    inherit IFabView

module ImageButton =
    let WidgetKey = Widgets.register<ImageButton>()

    let Aspect =
        Attributes.defineBindableEnum<Microsoft.Maui.Aspect> ImageButton.AspectProperty

    let BorderColor = Attributes.defineBindableColor ImageButton.BorderColorProperty

    let BorderWidth = Attributes.defineBindableFloat ImageButton.BorderWidthProperty

    let CornerRadius = Attributes.defineBindableFloat ImageButton.CornerRadiusProperty

    let IsLoading = Attributes.defineBindableBool ImageButton.IsLoadingProperty

    let IsOpaque = Attributes.defineBindableBool ImageButton.IsOpaqueProperty

    let IsPressed = Attributes.defineBindableBool ImageButton.IsPressedProperty

    let Padding =
        Attributes.defineBindableWithEquality<Thickness> ImageButton.PaddingProperty

    let Source = Attributes.defineBindableImageSource ImageButton.SourceProperty

[<Extension>]
type ImageButtonModifiers =
    /// <summary>Set the color of the image button border</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the image button border</param>
    [<Extension>]
    static member inline borderColor(this: WidgetBuilder<'msg, #IFabImageButton>, value: Color) =
        this.AddScalar(ImageButton.BorderColor.WithValue(value))

    /// <summary>Set the width of the image button border</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The width of the image button border</param>
    [<Extension>]
    static member inline borderWidth(this: WidgetBuilder<'msg, #IFabImageButton>, value: float) =
        this.AddScalar(ImageButton.BorderWidth.WithValue(value))

    /// <summary>Set the corner radius of the image button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The corner radius of the image button</param>
    [<Extension>]
    static member inline cornerRadius(this: WidgetBuilder<'msg, #IFabImageButton>, value: float) =
        this.AddScalar(ImageButton.CornerRadius.WithValue(value))

    /// <summary>Set the loading status of the image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The loading status of the image</param>
    [<Extension>]
    static member inline isLoading(this: WidgetBuilder<'msg, #IFabImageButton>, value: bool) =
        this.AddScalar(ImageButton.IsLoading.WithValue(value))

    /// <summary>Set whether the rendering engine may treat the image as opaque while rendering it</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the rendering engine may treat the image as opaque while rendering it</param>
    [<Extension>]
    static member inline isOpaque(this: WidgetBuilder<'msg, #IFabImageButton>, value: bool) =
        this.AddScalar(ImageButton.IsOpaque.WithValue(value))

    /// <summary>Set whether the button is pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the button is pressed</param>
    [<Extension>]
    static member inline isPressed(this: WidgetBuilder<'msg, #IFabImageButton>, value: bool) =
        this.AddScalar(ImageButton.IsPressed.WithValue(value))

    /// <summary>Set the padding inside the button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The padding inside the button</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, value: Thickness) =
        this.AddScalar(ImageButton.Padding.WithValue(value))

    /// <summary>Link a ViewRef to access the direct ImageButton control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabImageButton>, value: ViewRef<ImageButton>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type ImageButtonExtraModifiers =
    /// <summary>Set the padding inside the button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="uniformSize">The uniform padding inside the button</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, uniformSize: float) = this.padding(Thickness(uniformSize))

    /// <summary>Set the padding inside the button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="left">The left component of the padding inside the button</param>
    /// <param name="top">The top component of the padding inside the button</param>
    /// <param name="right">The right component of the padding inside the button</param>
    /// <param name="bottom">The bottom component of the padding inside the button</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, left: float, top: float, right: float, bottom: float) =
        this.padding(Thickness(left, top, right, bottom))
