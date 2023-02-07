namespace Fabulous.Maui

open System.Collections.Generic
open System.Runtime.CompilerServices
open Fabulous.StackAllocatedCollections
open Microsoft.Maui.Controls.Shapes
open Fabulous

type IFabGeometryGroup =
    inherit IFabGeometry

module GeometryGroup =

    let WidgetKey = Widgets.register<GeometryGroup>()

    let Children =
        Attributes.defineListWidgetCollection "GeometryGroup_Children" (fun target -> (target :?> GeometryGroup).Children :> IList<_>)

    let FillRule =
        Attributes.defineBindableEnum<FillRule> GeometryGroup.FillRuleProperty

[<AutoOpen>]
module GeometryGroupBuilders =
    type Fabulous.Maui.View with

        static member inline GeometryGroup<'msg>(?fillRule: FillRule) =
            match fillRule with
            | None -> CollectionBuilder<'msg, IFabGeometryGroup, IFabGeometry>(GeometryGroup.WidgetKey, GeometryGroup.Children)
            | Some fillRule ->
                CollectionBuilder<'msg, IFabGeometryGroup, IFabGeometry>(
                    GeometryGroup.WidgetKey,
                    GeometryGroup.Children,
                    GeometryGroup.FillRule.WithValue(fillRule)
                )

[<Extension>]
type GeometryGroupYieldExtensions =
    [<Extension>]
    static member inline Yield(_: CollectionBuilder<'msg, #IFabGeometryGroup, IFabGeometry>, x: WidgetBuilder<'msg, #IFabGeometry>) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: CollectionBuilder<'msg, #IFabGeometryGroup, IFabGeometry>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabGeometry>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
