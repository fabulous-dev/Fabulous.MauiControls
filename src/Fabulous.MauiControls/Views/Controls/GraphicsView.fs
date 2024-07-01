namespace Fabulous.Maui

open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabGraphicsView =
    inherit IFabView

module GraphicsView =
    let WidgetKey = Widgets.register<GraphicsView>()

    let Drawable =
        Attributes.defineBindableWithEquality<IDrawable> GraphicsView.DrawableProperty
