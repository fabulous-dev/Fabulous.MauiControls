namespace Fabulous.Maui.Mvu

open System
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Controls
open System.IO

type IFabMvuImage =
    inherit IFabMvuView
    inherit IFabImage

[<AutoOpen>]
module ImageBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: ImageSource) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.Source source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: ImageSource, aspect: Aspect) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.Source source), Image.Aspect.WithValue(aspect))

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: string) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.File source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: string, aspect: Aspect) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.File source), Image.Aspect.WithValue(aspect))

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: Uri) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.Uri source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: Uri, aspect: Aspect) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.Uri source), Image.Aspect.WithValue(aspect))

        /// <summary>Create an Image widget with a source</summary>
        /// <param name="source">The image source</param>
        static member inline Image<'msg>(source: Stream) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.Stream source))

        /// <summary>Create an Image widget with a source and an aspect</summary>
        /// <param name="source">The image source</param>
        /// <param name="aspect">The image aspect</param>
        static member inline Image<'msg>(source: Stream, aspect: Aspect) =
            WidgetBuilder<'msg, IFabMvuImage>(Image.WidgetKey, Image.Source.WithValue(ImageSourceValue.Stream source), Image.Aspect.WithValue(aspect))

[<Extension>]
type ImageModifiers =
    /// <summary>Link a ViewRef to access the direct Image control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuImage>, value: ViewRef<Image>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
