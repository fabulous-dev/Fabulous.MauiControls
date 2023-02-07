namespace Fabulous.Maui

open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open System

type IFabImageButton =
    inherit IFabView

module ImageButton =
    let WidgetKey = Widgets.register<ImageButton>()

    let Aspect =
        Attributes.defineBindableEnum<Microsoft.Maui.Aspect> ImageButton.AspectProperty

    let BorderColor =
        Attributes.defineBindableAppThemeColor ImageButton.BorderColorProperty

    let BorderWidth = Attributes.defineBindableFloat ImageButton.BorderWidthProperty

    let CornerRadius = Attributes.defineBindableFloat ImageButton.CornerRadiusProperty

    let IsLoading = Attributes.defineBindableBool ImageButton.IsLoadingProperty

    let IsOpaque = Attributes.defineBindableBool ImageButton.IsOpaqueProperty

    let IsPressed = Attributes.defineBindableBool ImageButton.IsPressedProperty

    let Padding =
        Attributes.defineBindableWithEquality<Thickness> ImageButton.PaddingProperty

    let Source =
        Attributes.defineBindableWithEquality ImageButton.SourceProperty

    let Clicked =
        Attributes.defineEventNoArg "ImageButton_Clicked" (fun target -> (target :?> ImageButton).Clicked)

    let Pressed =
        Attributes.defineEventNoArg "ImageButton_Pressed" (fun target -> (target :?> ImageButton).Pressed)

    let Released =
        Attributes.defineEventNoArg "ImageButton_Released" (fun target -> (target :?> ImageButton).Released)

[<AutoOpen>]
module ImageButtonBuilders =
    type Fabulous.Maui.View with
        static member inline ImageButton<'msg>(source: ImageSource, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(source)
            )

        static member inline ImageButton<'msg>(source: string, onClicked: 'msg) =
            View.ImageButton(ImageSource.FromFile(source), onClicked)
            
        static member inline ImageButton<'msg>(source: Uri, onClicked: 'msg) =
            View.ImageButton(ImageSource.FromUri(source), onClicked)
            
        static member inline ImageButton<'msg>(source: Stream, onClicked: 'msg) =
            View.ImageButton(ImageSource.FromStream(fun () -> source), onClicked)
            
        static member inline ImageButton<'msg>(source: ImageSource, onClicked: 'msg, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(source),
                ImageButton.Aspect.WithValue(aspect)
            )
            
        static member inline ImageButton<'msg>(source: string, onClicked: 'msg, aspect: Aspect) =
            View.ImageButton(ImageSource.FromFile(source), onClicked, aspect)
            
        static member inline ImageButton<'msg>(source: Uri, onClicked: 'msg, aspect: Aspect) =
            View.ImageButton(ImageSource.FromUri(source), onClicked, aspect)
            
        static member inline ImageButton<'msg>(source: Stream, onClicked: 'msg, aspect: Aspect) =
            View.ImageButton(ImageSource.FromStream(fun () -> source), onClicked, aspect)

[<Extension>]
type ImageButtonModifiers =
    /// <summary>Set the color of the image button border color</summary>
    /// <param name="light">The color of the image button border in the light theme.</param>
    /// <param name="dark">The color of the image button border in the dark theme.</param>
    [<Extension>]
    static member inline borderColor(this: WidgetBuilder<'msg, #IFabImageButton>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(ImageButton.BorderColor.WithValue(AppTheme.create light dark))

    /// <summary>Set the width of the image button border</summary>
    /// <param name="width">The width of the image button border.</param>
    [<Extension>]
    static member inline borderWidth(this: WidgetBuilder<'msg, #IFabImageButton>, value: float) =
        this.AddScalar(ImageButton.BorderWidth.WithValue(value))

    /// <summary>Set the corner radius of the image button</summary>
    /// <param name="radius">The corner radius of the image button.</param>
    [<Extension>]
    static member inline cornerRadius(this: WidgetBuilder<'msg, #IFabImageButton>, value: float) =
        this.AddScalar(ImageButton.CornerRadius.WithValue(value))

    [<Extension>]
    static member inline isLoading(this: WidgetBuilder<'msg, #IFabImageButton>, value: bool) =
        this.AddScalar(ImageButton.IsLoading.WithValue(value))

    [<Extension>]
    static member inline isOpaque(this: WidgetBuilder<'msg, #IFabImageButton>, value: bool) =
        this.AddScalar(ImageButton.IsOpaque.WithValue(value))

    [<Extension>]
    static member inline isPressed(this: WidgetBuilder<'msg, #IFabImageButton>, value: bool) =
        this.AddScalar(ImageButton.IsPressed.WithValue(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, value: Thickness) =
        this.AddScalar(ImageButton.Padding.WithValue(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, value: float) =
        ImageButtonModifiers.padding(this, Thickness(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabImageButton>, left: float, top: float, right: float, bottom: float) =
        ImageButtonModifiers.padding(this, Thickness(left, top, right, bottom))

    /// <summary>Event that is fired when image button is pressed.</summary>
    /// <param name="onPressed">Msg to dispatch when image button is pressed</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<'msg, #IFabImageButton>, onPressed: 'msg) =
        this.AddScalar(ImageButton.Pressed.WithValue(onPressed))

    /// <summary>Event that is fired when image button is released.</summary>
    /// <param name="onReleased">Msg to dispatch when image button is released.</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<'msg, #IFabImageButton>, onReleased: 'msg) =
        this.AddScalar(ImageButton.Released.WithValue(onReleased))

    /// <summary>Link a ViewRef to access the direct ImageButton control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabImageButton>, value: ViewRef<ImageButton>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
