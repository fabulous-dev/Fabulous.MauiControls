namespace Fabulous.Maui

open System.Collections.Generic
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections
open Microsoft.Maui.Controls.Shapes
open Microsoft.Maui.Graphics

type IFabPathFigure =
    inherit IFabElement

module PathFigure =
    let WidgetKey = Widgets.register<PathFigure>()

    let Segments =
        Attributes.defineListWidgetCollection "PathGeometry_Segments" (fun target -> (target :?> PathFigure).Segments :> IList<_>)

    let StartPoint =
        Attributes.defineBindableWithEquality<Point> PathFigure.StartPointProperty

    let IsClosed = Attributes.defineBindableBool PathFigure.IsClosedProperty

    let IsFilled = Attributes.defineBindableBool PathFigure.IsFilledProperty

[<AutoOpen>]
module PathFigureBuilders =

    type Fabulous.Maui.View with

        static member inline PathFigure<'msg>(?start: Point) =
            match start with
            | None -> CollectionBuilder<'msg, IFabPathFigure, IFabPathSegment>(PathFigure.WidgetKey, PathFigure.Segments)
            | Some start ->
                CollectionBuilder<'msg, IFabPathFigure, IFabPathSegment>(PathFigure.WidgetKey, PathFigure.Segments, PathFigure.StartPoint.WithValue(start))


[<Extension>]
type PathFigureModifiers =

    [<Extension>]
    static member inline isClosed(this: WidgetBuilder<'msg, #IFabPathFigure>, value: bool) =
        this.AddScalar(PathFigure.IsClosed.WithValue(value))

    [<Extension>]
    static member inline isFilled(this: WidgetBuilder<'msg, #IFabPathFigure>, value: bool) =
        this.AddScalar(PathFigure.IsFilled.WithValue(value))

[<Extension>]
type PathFigureYieldExtensions =
    [<Extension>]
    static member inline Yield(_: CollectionBuilder<'msg, #IFabPathFigure, IFabPathSegment>, x: WidgetBuilder<'msg, #IFabPathSegment>) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: CollectionBuilder<'msg, #IFabPathFigure, IFabPathSegment>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabPathSegment>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
