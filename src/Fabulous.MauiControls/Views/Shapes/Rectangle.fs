namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.Shapes

type IFabRectangle =
    inherit IFabShape

module Rectangle =
    let WidgetKey = Widgets.register<Rectangle>()

    let RadiusX = Attributes.defineBindableFloat Rectangle.RadiusXProperty

    let RadiusY = Attributes.defineBindableFloat Rectangle.RadiusYProperty

[<AutoOpen>]
module RectangleBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Rectangle widget with a stroke thickness and a stroke brush</summary>
        /// <param name="strokeThickness">The stroke thickness value</param>
        /// <param name="stroke">The stroke brush value</param>
        static member inline Rectangle<'msg>(strokeThickness: float, stroke: Brush) =
            WidgetBuilder<'msg, IFabRectangle>(Rectangle.WidgetKey, Shape.StrokeThickness.WithValue(strokeThickness), Shape.Stroke.WithValue(stroke))

        /// <summary>Create a Rectangle widget with a stroke thickness and a stroke brush</summary>
        /// <param name="strokeThickness">The stroke thickness value</param>
        /// <param name="stroke">The stroke brush value</param>
        static member inline Rectangle(strokeThickness: float, stroke: WidgetBuilder<'msg, #IFabRectangle>) =
            WidgetBuilder<'msg, IFabRectangle>(
                Rectangle.WidgetKey,
                AttributesBundle(
                    StackList.one(Shape.StrokeThickness.WithValue(strokeThickness)),
                    ValueSome [| Shape.StrokeWidget.WithValue(stroke.Compile()) |],
                    ValueNone
                )
            )

[<Extension>]
type RectangleModifiers =
    /// <summary>Set the X component of the radius</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The X component of the radius</param>
    [<Extension>]
    static member inline radiusX(this: WidgetBuilder<'msg, #IFabRectangle>, value: float) =
        this.AddScalar(Rectangle.RadiusX.WithValue(value))

    /// <summary>Set the Y component of the radius</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The Y component of the radius</param>
    [<Extension>]
    static member inline radiusY(this: WidgetBuilder<'msg, #IFabRectangle>, value: float) =
        this.AddScalar(Rectangle.RadiusY.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Rectangle control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabRectangle>, value: ViewRef<Rectangle>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type RectangleExtraModifiers =
    /// <summary>Set the radius</summary>
    /// <param name="this">Current widget</param>
    /// <param name="x">The X component of the radius</param>
    /// <param name="y">The Y component of the radius</param>
    [<Extension>]
    static member inline radiusX(this: WidgetBuilder<'msg, #IFabRectangle>, x: float, y: float) = this.radiusX(x).radiusY(y)
