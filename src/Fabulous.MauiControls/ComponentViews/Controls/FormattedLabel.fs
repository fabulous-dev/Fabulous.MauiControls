namespace Fabulous.Maui.Components

open Fabulous
open Fabulous.Maui

type IFabComponentFormattedLabel =
    inherit IFabComponentLabel
    inherit IFabFormattedLabel

[<AutoOpen>]
module FormattedLabelBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a FormattedLabel widget</summary>
        static member inline FormattedLabel<'msg>() =
            CollectionBuilder<'msg, IFabComponentFormattedLabel, IFabSpan>(FormattedLabel.WidgetKey, FormattedLabel.Spans)
