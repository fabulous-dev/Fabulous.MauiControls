namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.Shapes

type IFabBorder =
    inherit IFabView

module Border =
    let WidgetKey = Widgets.register<Border>()

    let Stroke = Attributes.defineBindableAppTheme<Brush> Border.StrokeProperty

    let StrokeWidget = Attributes.defineBindableWidget Border.StrokeProperty

    let Content = Attributes.defineBindableWidget Border.ContentProperty

    let StrokeShape =
        Attributes.defineSimpleScalarWithEquality<Shape> "Border_StrokeShape" (fun _ newValueOpt node ->
            let target = node.Target :?> BindableObject

            match newValueOpt with
            | ValueNone -> target.ClearValue(Border.StrokeShapeProperty)
            | ValueSome value -> target.SetValue(Border.StrokeShapeProperty, value))

    let StrokeShapeString =
        Attributes.defineSimpleScalarWithEquality<string> "Border_StrokeShapeString" (fun _ newValueOpt node ->
            let target = node.Target :?> BindableObject

            match newValueOpt with
            | ValueNone -> target.ClearValue(Border.StrokeShapeProperty)
            | ValueSome value -> target.SetValue(Border.StrokeShapeProperty, StrokeShapeTypeConverter().ConvertFromInvariantString(value)))

    let StrokeShapeWidget = Attributes.defineBindableWidget Border.StrokeShapeProperty

    let StrokeThickness = Attributes.defineBindableFloat Border.StrokeThicknessProperty

    let StrokeDashArrayString =
        Attributes.defineSimpleScalarWithEquality<string> "Border_StrokeDashArrayString" (fun _ newValueOpt node ->
            let target = node.Target :?> BindableObject

            match newValueOpt with
            | ValueNone -> target.ClearValue(Border.StrokeDashArrayProperty)
            | ValueSome string -> target.SetValue(Shape.StrokeDashArrayProperty, DoubleCollectionConverter().ConvertFromInvariantString(string)))

    let StrokeDashArrayList =
        Attributes.defineSimpleScalarWithEquality<float list> "Border_StrokeDashArrayList" (fun _ newValueOpt node ->
            let target = node.Target :?> BindableObject

            match newValueOpt with
            | ValueNone -> target.ClearValue(Border.StrokeDashArrayProperty)
            | ValueSome points ->
                let coll = DoubleCollection()
                points |> List.iter coll.Add
                target.SetValue(Border.StrokeDashArrayProperty, coll))

    let StrokeDashOffset =
        Attributes.defineBindableFloat Border.StrokeDashOffsetProperty

    let StrokeLineCap =
        Attributes.defineBindableWithEquality<PenLineCap> Border.StrokeLineCapProperty

    let StrokeLineJoin =
        Attributes.defineBindableWithEquality<PenLineJoin> Border.StrokeLineJoinProperty

    let StrokeMiterLimit =
        Attributes.defineBindableFloat Border.StrokeMiterLimitProperty

    let Padding =
        Attributes.defineBindableWithEquality<Thickness> Border.PaddingProperty

[<AutoOpen>]
module BorderBuilders =
    type Fabulous.Maui.View with

        /// <summary>Border is a container control that draws a border, background, or both, around another control. A Border can only contain one child object. If you want to put a border around multiple objects, wrap them in a container object such as a layout</summary>
        /// <param name="light">The color of the stroke in the light theme.</param>
        /// <param name="dark">The color of the stroke in the dark theme.</param>
        static member inline Border<'msg, 'marker when 'marker :> IFabView>(content: WidgetBuilder<'msg, 'marker>, light: Brush, ?dark: Brush) =
            WidgetBuilder<'msg, IFabBorder>(
                Border.WidgetKey,
                AttributesBundle(
                    StackList.two(
                        Border.Stroke.WithValue(AppTheme.create light dark),
                        // By spec we need to set StrokeShape to Rectangle
                        Border.StrokeShape.WithValue(Rectangle())
                    ),
                    ValueSome [| Border.Content.WithValue(content.Compile()) |],
                    ValueNone
                )
            )

        /// <summary>Border is a container control that draws a border, background, or both, around another control. A Border can only contain one child object. If you want to put a border around multiple objects, wrap them in a container object such as a layout</summary>
        /// <param name="stroke">The stroke brush widget</param>
        static member inline Border<'msg, 'marker, 'stroke when 'marker :> IFabView and 'stroke :> IFabBrush>
            (
                content: WidgetBuilder<'msg, 'marker>,
                stroke: WidgetBuilder<'msg, 'stroke>
            ) =
            WidgetBuilder<'msg, IFabBorder>(
                Border.WidgetKey,
                AttributesBundle(
                    // By spec we need to set StrokeShape to Rectangle
                    StackList.one(Border.StrokeShape.WithValue(Rectangle())),
                    ValueSome
                        [| Border.Content.WithValue(content.Compile())
                           Border.StrokeWidget.WithValue(stroke.Compile()) |],
                    ValueNone
                )
            )

[<Extension>]
type BorderModifiers =

    [<Extension>]
    static member inline strokeShape(this: WidgetBuilder<'msg, #IFabBorder>, content: string) =
        this.AddScalar(Border.StrokeShapeString.WithValue(content))

    [<Extension>]
    static member inline strokeShape<'msg, 'marker, 'contentMarker when 'marker :> IFabBorder and 'contentMarker :> IFabShape>
        (
            this: WidgetBuilder<'msg, 'marker>,
            content: WidgetBuilder<'msg, 'contentMarker>
        ) =
        this.AddWidget(Border.StrokeShapeWidget.WithValue(content.Compile()))

    [<Extension>]
    static member inline strokeThickness(this: WidgetBuilder<'msg, #IFabBorder>, value: float) =
        this.AddScalar(Border.StrokeThickness.WithValue(value))

    [<Extension>]
    static member inline strokeDashArray(this: WidgetBuilder<'msg, #IFabBorder>, value: string) =
        this.AddScalar(Border.StrokeDashArrayString.WithValue(value))

    [<Extension>]
    static member inline strokeDashArray(this: WidgetBuilder<'msg, #IFabShape>, value: float list) =
        this.AddScalar(Border.StrokeDashArrayList.WithValue(value))

    [<Extension>]
    static member inline strokeDashOffset(this: WidgetBuilder<'msg, #IFabBorder>, value: float) =
        this.AddScalar(Border.StrokeDashOffset.WithValue(value))

    [<Extension>]
    static member inline strokeLineCap(this: WidgetBuilder<'msg, #IFabBorder>, value: PenLineCap) =
        this.AddScalar(Border.StrokeLineCap.WithValue(value))

    [<Extension>]
    static member inline strokeLineJoin(this: WidgetBuilder<'msg, #IFabBorder>, value: PenLineJoin) =
        this.AddScalar(Border.StrokeLineJoin.WithValue(value))

    [<Extension>]
    static member inline strokeMiterLimit(this: WidgetBuilder<'msg, #IFabBorder>, value: float) =
        this.AddScalar(Border.StrokeMiterLimit.WithValue(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabBorder>, value: Thickness) =
        this.AddScalar(Border.Padding.WithValue(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabBorder>, value: float) =
        BorderModifiers.padding(this, Thickness(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabBorder>, left: float, top: float, right: float, bottom: float) =
        BorderModifiers.padding(this, Thickness(left, top, right, bottom))

    /// <summary>Link a ViewRef to access the direct Border control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabBorder>, value: ViewRef<Border>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
