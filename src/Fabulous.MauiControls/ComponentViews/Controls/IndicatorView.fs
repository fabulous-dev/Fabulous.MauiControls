namespace Fabulous.Maui.Components

open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentIndicatorView =
    inherit IFabComponentTemplatedView
    inherit IFabIndicatorView

[<AutoOpen>]
module IndicatorViewBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create an IndicatorView widget with a reference</summary>
        static member inline IndicatorView<'msg>(reference: ViewRef<IndicatorView>) =
            WidgetBuilder<'msg, IFabComponentIndicatorView>(IndicatorView.WidgetKey, ViewRefAttributes.ViewRef.WithValue(reference.Unbox))
