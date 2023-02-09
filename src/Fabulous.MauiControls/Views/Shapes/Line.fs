namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.Shapes
open Microsoft.Maui.Graphics

type IFabLine =
    inherit IFabShape

module Line =
    let WidgetKey = Widgets.register<Line>()

    let Points =
        Attributes.defineSimpleScalarWithEquality<struct (Point * Point)> "Line_Point1" (fun _ newValueOpt node ->
            let line = node.Target :?> Line

            match newValueOpt with
            | ValueNone ->
                line.X1 <- 0.
                line.Y1 <- 0.
                line.X2 <- 0.
                line.Y2 <- 0.
            | ValueSome(startPoint, endPoint) ->
                line.X1 <- startPoint.X
                line.Y1 <- startPoint.Y
                line.X2 <- endPoint.X
                line.Y2 <- endPoint.Y)

[<AutoOpen>]
module LineBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Line widget with a start point, an end point, a stroke thickness, and a stroke brush</summary>
        /// <param name="startPoint">The start point</param>
        /// <param name="endPoint">The end point</param>
        /// <param name="strokeThickness">The stroke thickness</param>
        /// <param name="stroke">The stroke brush</param>
        static member inline Line<'msg>(startPoint: Point, endPoint: Point, strokeThickness: float, stroke: Brush) =
            WidgetBuilder<'msg, IFabLine>(
                Line.WidgetKey,
                Line.Points.WithValue(struct (startPoint, endPoint)),
                Shape.StrokeThickness.WithValue(strokeThickness),
                Shape.Stroke.WithValue(stroke)
            )

        /// <summary>Create a Line widget with a start point, an end point, a stroke thickness, and a stroke brush</summary>
        /// <param name="startPoint">The start point</param>
        /// <param name="endPoint">The end point</param>
        /// <param name="strokeThickness">The stroke thickness</param>
        /// <param name="stroke">The stroke brush</param>
        static member inline Line(startPoint: Point, endPoint: Point, strokeThickness: float, stroke: WidgetBuilder<'msg, #IFabBrush>) =
            WidgetBuilder<'msg, IFabLine>(
                Line.WidgetKey,
                AttributesBundle(
                    StackList.two(Line.Points.WithValue(struct (startPoint, endPoint)), Shape.StrokeThickness.WithValue(strokeThickness)),
                    ValueSome [| Shape.StrokeWidget.WithValue(stroke.Compile()) |],
                    ValueNone
                )
            )

[<Extension>]
type LineModifiers =
    /// <summary>Link a ViewRef to access the direct Line control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabLine>, value: ViewRef<Line>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
