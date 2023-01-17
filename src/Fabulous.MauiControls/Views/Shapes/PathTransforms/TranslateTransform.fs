namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls.Shapes

type IFabTranslateTransform =
    inherit IFabTransform

module TranslateTransform =
    let WidgetKey = Widgets.register<TranslateTransform>()

    let X = Attributes.defineBindableFloat TranslateTransform.XProperty

    let Y = Attributes.defineBindableFloat TranslateTransform.YProperty

[<AutoOpen>]
module TranslateTransformBuilders =

    type Fabulous.Maui.View with

        static member inline TranslateTransform<'msg>(x: float, y: float) =
            WidgetBuilder<'msg, IFabTranslateTransform>(TranslateTransform.WidgetKey, TranslateTransform.X.WithValue(x), TranslateTransform.Y.WithValue(y))
