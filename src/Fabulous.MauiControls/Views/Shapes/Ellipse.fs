namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.Shapes

type IFabEllipse =
    inherit IFabShape

module Ellipse =
    let WidgetKey = Widgets.register<Ellipse>()

[<AutoOpen>]
module EllipseBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create an Ellipse widget with a stroke brush and a stroke thickness</summary>
        /// <param name="strokeThickness">The stroke thickness value</param>
        /// <param name="stroke">The stroke value</param>
        static member inline Ellipse<'msg>(strokeThickness: float, stroke: Brush) =
            WidgetBuilder<'msg, IFabEllipse>(
                Ellipse.WidgetKey,
                Shape.StrokeThickness.WithValue(strokeThickness),
                Shape.Stroke.WithValue(stroke)
            )
            
        /// <summary>Create an Ellipse widget with a stroke brush and a stroke thickness</summary>
        /// <param name="strokeThickness">The stroke thickness value</param>
        /// <param name="stroke">The stroke value</param>
        static member inline Ellipse(strokeThickness: float, stroke: WidgetBuilder<'msg, #IFabBrush>) =
            WidgetBuilder<'msg, IFabEllipse>(
                Ellipse.WidgetKey,
                AttributesBundle(
                    StackList.one(Shape.StrokeThickness.WithValue(strokeThickness)),
                    ValueSome [|
                        Shape.StrokeWidget.WithValue(stroke.Compile())
                    |],
                    ValueNone
                )
            )

[<Extension>]
type EllipseModifiers =
    /// <summary>Link a ViewRef to access the direct Ellipse control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabEllipse>, value: ViewRef<Ellipse>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
