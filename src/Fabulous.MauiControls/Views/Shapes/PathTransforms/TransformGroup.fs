namespace Fabulous.Maui

open System.Collections.Generic
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections
open Microsoft.Maui.Controls.Shapes

type IFabTransformGroup =
    inherit IFabTransform

module TransformGroup =

    let WidgetKey = Widgets.register<TransformGroup>()

    let Children =
        Attributes.defineListWidgetCollection "TransformGroup_Children" (fun target -> (target :?> TransformGroup).Children :> IList<_>)

[<AutoOpen>]
module TransformGroupBuilders =
    type Fabulous.Maui.View with

        static member inline TransformGroup<'msg>() =
            CollectionBuilder<'msg, IFabTransformGroup, IFabTransform>(TransformGroup.WidgetKey, TransformGroup.Children)

[<Extension>]
type TransformGroupYieldExtensions =
    [<Extension>]
    static member inline Yield(_: CollectionBuilder<'msg, #IFabTransformGroup, IFabTransform>, x: WidgetBuilder<'msg, #IFabTransform>) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: CollectionBuilder<'msg, #IFabTransformGroup, IFabTransform>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabTransform>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
