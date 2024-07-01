namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabShadow =
    inherit IFabElement

module Shadow =
    let WidgetKey = Widgets.register<Shadow>()

    let Radius = Attributes.defineBindableFloat Shadow.RadiusProperty

    let Opacity = Attributes.defineBindableFloat Shadow.OffsetProperty

    let Brush = Attributes.defineBindableWithEquality<Brush> Shadow.BrushProperty

    let Offset = Attributes.defineBindableWithEquality<Point> Shadow.OffsetProperty

[<Extension>]
type ShadowModifiers =
    /// <summary>Set the opacity value applied to the widget when it is rendered</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The opacity value. Values will be clamped between 0 and 1</param>
    [<Extension>]
    static member inline opacity(this: WidgetBuilder<'msg, #IFabShadow>, value: float) =
        this.AddScalar(Shadow.Opacity.WithValue(value))

    /// <summary>Set the radius of the blur used to generate the shadow</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The blur radius value</param>
    [<Extension>]
    static member inline blurRadius(this: WidgetBuilder<'msg, #IFabShadow>, value: float) =
        this.AddScalar(Shadow.Radius.WithValue(value))
