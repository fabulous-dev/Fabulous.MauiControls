namespace Fabulous.Maui.Components

open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Controls
open System

type IFabComponentImageButton =
    inherit IFabComponentView
    inherit IFabImageButton

module ImageButton =
    let Clicked =
        ComponentAttributes.defineEventNoArg "ImageButton_Clicked" (fun target -> (target :?> ImageButton).Clicked)

    let Pressed =
        ComponentAttributes.defineEventNoArg "ImageButton_Pressed" (fun target -> (target :?> ImageButton).Pressed)

    let Released =
        ComponentAttributes.defineEventNoArg "ImageButton_Released" (fun target -> (target :?> ImageButton).Released)

[<AutoOpen>]
module ImageButtonBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton<'msg>(source: ImageSource, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Source source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton<'msg>(source: ImageSource, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Source source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton<'msg>(source: string, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.File source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton<'msg>(source: string, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.File source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton<'msg>(source: Uri, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton<'msg>(source: Uri, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton<'msg>(source: Stream, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton<'msg>(source: Stream, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<'msg, IFabComponentImageButton>(
                ImageButton.WidgetKey,
                ImageButton.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source),
                ImageButton.Aspect.WithValue(aspect)
            )

[<Extension>]
type ImageButtonModifiers =
    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<'msg, #IFabComponentImageButton>, fn: unit -> unit) =
        this.AddScalar(ImageButton.Pressed.WithValue(fn))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<'msg, #IFabComponentImageButton>, fn: unit -> unit) =
        this.AddScalar(ImageButton.Released.WithValue(fn))

    /// <summary>Link a ViewRef to access the direct ImageButton control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentImageButton>, value: ViewRef<ImageButton>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
