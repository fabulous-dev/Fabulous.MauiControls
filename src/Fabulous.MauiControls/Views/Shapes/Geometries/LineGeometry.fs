namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls.Shapes
open Microsoft.Maui.Graphics

type IFabLineGeometry =
    inherit IFabGeometry

module LineGeometry =
    let WidgetKey = Widgets.register<LineGeometry>()

    let StartPoint =
        Attributes.defineBindableWithEquality<Point> LineGeometry.StartPointProperty

    let EndPoint =
        Attributes.defineBindableWithEquality<Point> LineGeometry.EndPointProperty

[<AutoOpen>]
module LineGeometryBuilders =
    type Fabulous.Maui.View with

        static member inline LineGeometry<'msg>(start: Point, end': Point) =
            WidgetBuilder<'msg, IFabLineGeometry>(LineGeometry.WidgetKey, LineGeometry.StartPoint.WithValue(start), LineGeometry.EndPoint.WithValue(end'))
