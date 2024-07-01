namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuFormattedLabel =
    inherit IFabMvuLabel
    inherit IFabFormattedLabel

[<AutoOpen>]
module FormattedLabelBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a FormattedLabel widget</summary>
        static member inline FormattedLabel<'msg>() =
            CollectionBuilder<'msg, IFabMvuFormattedLabel, IFabSpan>(FormattedLabel.WidgetKey, FormattedLabel.Spans)

[<Extension>]
type FormattedLabelModifiers =        
    /// <summary>Link a ViewRef to access the direct FormattedLabel control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuFormattedLabel>, value: ViewRef<Label>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
