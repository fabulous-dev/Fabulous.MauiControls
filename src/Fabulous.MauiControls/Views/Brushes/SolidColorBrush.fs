namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabSolidColorBrush =
    inherit IFabBrush

module SolidColorBrush =
    let WidgetKey = Widgets.register<SolidColorBrush>()

    let Color = Attributes.defineBindableWithEquality SolidColorBrush.ColorProperty
    
    let FabColor = Attributes.defineBindableWithEquality SolidColorBrush.ColorProperty

[<AutoOpen>]
module SolidColorBrushBuilders =
    type Fabulous.Maui.View with
        /// <summary>Creat a SolidColorBrush widget, which paints an area with a solid color</summary>
        /// <param name="color">The color value</param>
        static member inline SolidColorBrush(color: Color) =
            WidgetBuilder<'msg, IFabSolidColorBrush>(SolidColorBrush.WidgetKey, SolidColorBrush.Color.WithValue(color))
            
        /// <summary>Creat a SolidColorBrush widget, which paints an area with a solid color</summary>
        /// <param name="color">The color value</param>
        static member inline SolidColorBrush(color: FabColor) =
            WidgetBuilder<'msg, IFabSolidColorBrush>(SolidColorBrush.WidgetKey, SolidColorBrush.FabColor.WithValue(color))
