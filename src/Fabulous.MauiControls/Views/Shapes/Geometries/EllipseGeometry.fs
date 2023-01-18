namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls.Shapes
open Microsoft.Maui.Graphics

type IFabEllipseGeometry =
    inherit IFabGeometry

module EllipseGeometry =
    let WidgetKey = Widgets.register<EllipseGeometry>()

    let Center =
        Attributes.defineBindableWithEquality<Point> EllipseGeometry.CenterProperty

    let RadiusX = Attributes.defineBindableFloat EllipseGeometry.RadiusXProperty

    let RadiusY = Attributes.defineBindableFloat EllipseGeometry.RadiusYProperty

[<AutoOpen>]
module EllipseGeometryBuilders =

    type Fabulous.Maui.View with

        static member inline EllipseGeometry<'msg>(center: Point, radiusX: float, radiusY: float) =
            WidgetBuilder<'msg, IFabEllipseGeometry>(
                EllipseGeometry.WidgetKey,
                EllipseGeometry.Center.WithValue(center),
                EllipseGeometry.RadiusX.WithValue(radiusX),
                EllipseGeometry.RadiusY.WithValue(radiusY)
            )
