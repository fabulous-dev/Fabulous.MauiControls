namespace Fabulous.Maui

open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open System
open Microsoft.Maui.Graphics

type IFabImageButton =
    inherit IFabView

module ImageButton =
    let WidgetKey = Widgets.register<ImageButton>()

    let Aspect =
        Attributes.defineBindableEnum<Microsoft.Maui.Aspect> ImageButton.AspectProperty

    let BorderColor = Attributes.defineBindableColor ImageButton.BorderColorProperty

    let BorderWidth = Attributes.defineBindableFloat ImageButton.BorderWidthProperty

    let ClickedMsg =
        Attributes.defineEventNoArg "ImageButton_ClickedMsg" (fun target -> (target :?> ImageButton).Clicked)

    let ClickedFn =
        Attributes.defineEventNoArgNoDispatch "ImageButton_ClickedFn" (fun target -> (target :?> ImageButton).Clicked)

    let CornerRadius = Attributes.defineBindableFloat ImageButton.CornerRadiusProperty

    let IsLoading = Attributes.defineBindableBool ImageButton.IsLoadingProperty

    let IsOpaque = Attributes.defineBindableBool ImageButton.IsOpaqueProperty

    let IsPressed = Attributes.defineBindableBool ImageButton.IsPressedProperty

    let Padding =
        Attributes.defineBindableWithEquality<Thickness> ImageButton.PaddingProperty

    let PressedMsg =
        Attributes.defineEventNoArg "ImageButton_PressedMsg" (fun target -> (target :?> ImageButton).Pressed)

    let PressedFn =
        Attributes.defineEventNoArgNoDispatch "ImageButton_PressedFn" (fun target -> (target :?> ImageButton).Pressed)

    let ReleasedMsg =
        Attributes.defineEventNoArg "ImageButton_ReleasedMsg" (fun target -> (target :?> ImageButton).Released)

    let ReleasedFn =
        Attributes.defineEventNoArgNoDispatch "ImageButton_ReleasedFn" (fun target -> (target :?> ImageButton).Released)

    let Source = Attributes.defineBindableImageSource ImageButton.SourceProperty

[<AutoOpen>]
module ImageButtonBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: ImageSource, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.Source source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: ImageSource, onClicked: 'msg, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.Source source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: string, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.File source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: string, onClicked: 'msg, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.File source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: Uri, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: Uri, onClicked: 'msg, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: Stream, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: Stream, onClicked: 'msg, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedMsg.WithValue(MsgValue(onClicked)),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: ImageSource, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Source source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: ImageSource, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Source source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: string, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.File source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: string, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.File source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: Uri, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: Uri, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: Stream, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: Stream, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.ClickedFn.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source),
                ImageButton.Aspect.WithValue(aspect)
            )

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

    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<'msg, #IFabImageButton>, msg: 'msg) =
        this.AddScalar(ImageButton.PressedMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<'msg, #IFabImageButton>, fn: unit -> unit) =
        this.AddScalar(ImageButton.PressedFn.WithValue(fn))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<'msg, #IFabImageButton>, msg: 'msg) =
        this.AddScalar(ImageButton.ReleasedMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<'msg, #IFabImageButton>, fn: unit -> unit) =
        this.AddScalar(ImageButton.ReleasedFn.WithValue(fn))

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

    /// <summary>Set the padding inside the widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="horizontalSize">The padding value that will be applied to both left and right sides</param>
    /// <param name="verticalSize">The padding value that will be applied to both top and bottom sides</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, horizontalSize: float, verticalSize: float) =
        this.padding(Thickness(horizontalSize, verticalSize))

    /// <summary>Set the padding inside the button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="left">The left component of the padding inside the button</param>
    /// <param name="top">The top component of the padding inside the button</param>
    /// <param name="right">The right component of the padding inside the button</param>
    /// <param name="bottom">The bottom component of the padding inside the button</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, left: float, top: float, right: float, bottom: float) =
        this.padding(Thickness(left, top, right, bottom))
