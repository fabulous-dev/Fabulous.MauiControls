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

    /// Performance optimization: avoid allocating a new ImageSource instance on each update
    /// we store the user value (eg. string, Uri, Stream) and convert it to an ImageSource only when needed
    let inline private defineSourceAttribute<'model when 'model: equality> ([<InlineIfLambda>] convertModelToValue: 'model -> ImageSource) =
        Attributes.defineScalar<'model, 'model> Image.SourceProperty.PropertyName id ScalarAttributeComparers.equalityCompare (fun _ newValueOpt node ->
            let target = node.Target :?> Image

            match newValueOpt with
            | ValueNone -> target.ClearValue(Image.SourceProperty)
            | ValueSome v -> target.SetValue(Image.SourceProperty, convertModelToValue v))

    let SourceFile = defineSourceAttribute<string> ImageSource.FromFile

    let SourceUri = defineSourceAttribute<Uri> ImageSource.FromUri

    let SourceStream =
        defineSourceAttribute<Stream>(fun stream -> ImageSource.FromStream(fun () -> stream))

[<AutoOpen>]
module ImageBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: ImageSource) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.Source.WithValue(source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: ImageSource, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.Source.WithValue(source), Image.Aspect.WithValue(aspect))

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: string) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.SourceFile.WithValue(source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: string, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.SourceFile.WithValue(source), Image.Aspect.WithValue(aspect))

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: Uri) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.SourceUri.WithValue(source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: Uri, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.SourceUri.WithValue(source), Image.Aspect.WithValue(aspect))

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: Stream) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.SourceStream.WithValue(source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: Stream, aspect: Aspect) =
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, Image.SourceStream.WithValue(source), Image.Aspect.WithValue(aspect))

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
