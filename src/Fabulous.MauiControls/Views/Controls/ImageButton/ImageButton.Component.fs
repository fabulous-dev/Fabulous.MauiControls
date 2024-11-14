namespace Fabulous.Maui

open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open System

module ImageButtonComponent =
    let Clicked =
        Attributes.Component.defineEventNoArg "ImageButtonComponent_Clicked" (fun target -> (target :?> ImageButton).Clicked)

    let Pressed =
        Attributes.Component.defineEventNoArg "ImageButtonComponent_Pressed" (fun target -> (target :?> ImageButton).Pressed)

    let Released =
        Attributes.Component.defineEventNoArg "ImageButtonComponent_Released" (fun target -> (target :?> ImageButton).Released)

[<AutoOpen>]
module ImageButtonComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: ImageSource, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Source source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: ImageSource, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Source source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: string, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.File source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: string, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.File source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: Uri, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: Uri, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Uri source),
                ImageButton.Aspect.WithValue(aspect)
            )

        /// <summary>Create an ImageButton with an image source and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline ImageButton(source: Stream, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source)
            )

        /// <summary>Create an ImageButton with an image source and an aspect and listen for the Click event</summary>
        /// <param name="source">The image source</param>
        /// <param name="onClicked">Message to dispatch</param>
        /// <param name="aspect">The aspect value</param>
        static member inline ImageButton(source: Stream, onClicked: unit -> unit, aspect: Aspect) =
            WidgetBuilder<unit, IFabImageButton>(
                ImageButton.WidgetKey,
                ImageButtonComponent.Clicked.WithValue(onClicked),
                ImageButton.Source.WithValue(ImageSourceValue.Stream source),
                ImageButton.Aspect.WithValue(aspect)
            )

[<Extension>]
type ImageButtonComponentModifiers =
    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<unit, #IFabImageButton>, fn: unit -> unit) =
        this.AddScalar(ImageButtonComponent.Pressed.WithValue(fn))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<unit, #IFabImageButton>, fn: unit -> unit) =
        this.AddScalar(ImageButtonComponent.Released.WithValue(fn))
