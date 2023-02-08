namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open System.IO

type IFabImage =
    inherit IFabView

module Image =
    let WidgetKey = Widgets.register<Image>()

    let Aspect = Attributes.defineBindableEnum<Aspect> Image.AspectProperty

    let IsAnimationPlaying =
        Attributes.defineBindableBool Image.IsAnimationPlayingProperty

    let IsLoading = Attributes.defineBindableBool Image.IsLoadingProperty

    let IsOpaque = Attributes.defineBindableBool Image.IsOpaqueProperty

    let Source = Attributes.defineBindableWithEquality<ImageSource> Image.SourceProperty

[<AutoOpen>]
module ImageBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: ImageSource) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.Source.WithValue(source), Image.Aspect.WithValue(aspect))
            
        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: ImageSource, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.Source.WithValue(source), Image.Aspect.WithValue(aspect))

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: string) =
            View.Image<'msg>(ImageSource.FromFile(source))
            
        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: string, aspect: Aspect) =
            View.Image<'msg>(ImageSource.FromFile(source), aspect)
            
        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: Uri) =
            View.Image<'msg>(ImageSource.FromUri(source))
            
        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: Uri, aspect: Aspect) =
            View.Image<'msg>(ImageSource.FromUri(source), aspect)
            
        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: Stream) =
            View.Image<'msg>(ImageSource.FromStream(fun () -> source))
            
        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: Stream, aspect: Aspect) =
            View.Image<'msg>(ImageSource.FromStream(fun () -> source), aspect)

[<Extension>]
type ImageModifiers =
    /// <summary>Set whether an animated GIF is playing or stopped</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indication whether the animated GIF is playing</param>
    [<Extension>]
    static member inline isAnimationPlaying(this: WidgetBuilder<'msg, #IFabImage>, value: bool) =
        this.AddScalar(Image.IsAnimationPlaying.WithValue(value))

    /// <summary>Set whether the rendering engine may treat the image as opaque while rendering it</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the rendering engine may treat the image as opaque while rendering it</param>
    [<Extension>]
    static member inline isOpaque(this: WidgetBuilder<'msg, #IFabImage>, value: bool) =
        this.AddScalar(Image.IsOpaque.WithValue(value))

    /// <summary>Set the loading status of the image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The loading status of the image</param>
    [<Extension>]
    static member inline isLoading(this: WidgetBuilder<'msg, #IFabImage>, value: bool) =
        this.AddScalar(Image.IsLoading.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Image control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabImage>, value: ViewRef<Image>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
