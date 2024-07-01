namespace Fabulous.Maui.Mvu

open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuIndicatorView =
    inherit IFabTemplatedView
    inherit IFabMvuLayout

[<AutoOpen>]
module IndicatorViewBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create an IndicatorView widget with a reference</summary>
        static member inline IndicatorView<'msg>(reference: ViewRef<IndicatorView>) =
            WidgetBuilder<'msg, IFabMvuIndicatorView>(IndicatorView.WidgetKey, ViewRefAttributes.ViewRef.WithValue(reference.Unbox))
