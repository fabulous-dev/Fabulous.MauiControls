namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabBoxView =
    inherit IFabView

module BoxView =
    let WidgetKey = Widgets.register<BoxView>()

    let Color = Attributes.defineBindableAppThemeColor BoxView.ColorProperty

    let CornerRadius = Attributes.defineBindableFloat BoxView.CornerRadiusProperty

[<AutoOpen>]
module BoxViewBuilders =
    type Fabulous.Maui.View with

        static member inline BoxView<'msg>(light: FabColor, ?dark: FabColor) =
            WidgetBuilder<'msg, IFabBoxView>(BoxView.WidgetKey, BoxView.Color.WithValue(AppTheme.create light dark))

[<Extension>]
type BoxViewModifiers =
    /// <summary>Set the corner radius of the BoxView</summary>
    /// <param name="value">corner radius value for the box view.</param>
    [<Extension>]
    static member inline cornerRadius(this: WidgetBuilder<'msg, #IFabBoxView>, value: float) =
        this.AddScalar(BoxView.CornerRadius.WithValue(value))

    /// <summary>Link a ViewRef to access the direct BoxView control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabBoxView>, value: ViewRef<BoxView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
