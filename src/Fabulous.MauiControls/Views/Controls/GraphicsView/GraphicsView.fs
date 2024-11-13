namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IGraphicsView =
    inherit IFabView

module GraphicsView =
    let WidgetKey = Widgets.register<GraphicsView>()

    let Drawable =
        Attributes.defineBindableWithEquality<IDrawable> GraphicsView.DrawableProperty

[<AutoOpen>]
module GraphicsViewBuilders =
    /// <summary>GraphicsView defines the Drawable property, of type IDrawable, which specifies the content that will be drawn.</summary>
    type Fabulous.Maui.View with

        /// <summary>Create a GraphicsView widget with a drawable content</summary>
        /// <param name="drawable">The drawable content</param>
        static member inline GraphicsView(drawable: IDrawable) =
            WidgetBuilder<'msg, IGraphicsView>(GraphicsView.WidgetKey, GraphicsView.Drawable.WithValue(drawable))
