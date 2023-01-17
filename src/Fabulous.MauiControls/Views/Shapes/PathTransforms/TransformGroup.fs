namespace Fabulous.Maui

open System.Collections.Generic
open Fabulous
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
