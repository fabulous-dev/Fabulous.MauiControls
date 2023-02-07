namespace Fabulous.Maui

open System.Collections.Generic
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.Shapes

type IFabPathGeometry =
    inherit IFabGeometry

module PathGeometry =
    let WidgetKey = Widgets.register<PathGeometry>()

    let FiguresWidgets =
        Attributes.defineListWidgetCollection "PathGeometry_FiguresWidgets" (fun target -> (target :?> PathGeometry).Figures :> IList<_>)

    let FiguresString =
        Attributes.defineSimpleScalarWithEquality<string> "PathGeometry_FiguresString" (fun _ newValueOpt node ->
            let target = node.Target :?> BindableObject

            match newValueOpt with
            | ValueNone -> target.ClearValue(PathGeometry.FiguresProperty)
            | ValueSome value -> target.SetValue(PathGeometry.FiguresProperty, PathFigureCollectionConverter().ConvertFromInvariantString(value)))

    let FillRule = Attributes.defineBindableEnum<FillRule> PathGeometry.FillRuleProperty

[<AutoOpen>]
module PathGeometryBuilders =

    type Fabulous.Maui.View with

        static member inline PathGeometry<'msg>(?fillRule: FillRule) =
            match fillRule with
            | None -> CollectionBuilder<'msg, IFabPathGeometry, IFabPathFigure>(PathGeometry.WidgetKey, PathGeometry.FiguresWidgets)
            | Some fillRule ->
                CollectionBuilder<'msg, IFabPathGeometry, IFabPathFigure>(
                    PathGeometry.WidgetKey,
                    PathGeometry.FiguresWidgets,
                    PathGeometry.FillRule.WithValue(fillRule)
                )

        static member inline PathGeometry<'msg>(content: string, ?fillRule: FillRule) =
            match fillRule with
            | None -> WidgetBuilder<'msg, IFabPathGeometry>(PathGeometry.WidgetKey, PathGeometry.FiguresString.WithValue(content))
            | Some fillRule ->
                WidgetBuilder<'msg, IFabPathGeometry>(
                    PathGeometry.WidgetKey,
                    PathGeometry.FiguresString.WithValue(content),
                    PathGeometry.FillRule.WithValue(fillRule)
                )

[<Extension>]
type CollectionBuilderExtensions =
    [<Extension>]
    static member inline Yield(_: CollectionBuilder<'msg, #IFabPathGeometry, IFabPathFigure>, x: WidgetBuilder<'msg, #IFabPathFigure>) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: CollectionBuilder<'msg, #IFabPathGeometry, IFabPathFigure>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabPathFigure>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
