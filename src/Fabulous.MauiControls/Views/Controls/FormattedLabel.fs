namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabFormattedLabel =
    inherit IFabLabel

module FormattedLabel =
    let WidgetKey = Widgets.register<Label>()

    let Spans =
        Attributes.defineListWidgetCollection "FormattedString_Spans" (fun target ->
            let label = target :?> Label

            if label.FormattedText = null then
                label.FormattedText <- FormattedString()

            label.FormattedText.Spans)

[<AutoOpen>]
module FormattedLabelBuilders =
    type Fabulous.Maui.View with

        static member inline FormattedLabel<'msg>() =
            CollectionBuilder<'msg, IFabFormattedLabel, IFabSpan>(FormattedLabel.WidgetKey, FormattedLabel.Spans)

[<Extension>]
type FormattedLabelModifiers =
    /// <summary>Link a ViewRef to access the direct Label control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabFormattedLabel>, value: ViewRef<Label>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
