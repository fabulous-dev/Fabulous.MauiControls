namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabBoxView =
    inherit IFabView

module BoxView =
    let WidgetKey = Widgets.register<BoxView>()

    let Color = Attributes.defineBindableColor BoxView.ColorProperty

    let CornerRadius = Attributes.defineBindableFloat BoxView.CornerRadiusProperty

[<Extension>]
type BoxViewModifiers =
    /// <summary>Set the corner radius of the BoxView</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">corner radius value for the box view.</param>
    [<Extension>]
    static member inline cornerRadius(this: WidgetBuilder<'msg, #IFabBoxView>, value: float) =
        this.AddScalar(BoxView.CornerRadius.WithValue(value))
