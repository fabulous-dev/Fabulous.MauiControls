namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuLabel =
    inherit IFabMvuView
    inherit IFabLabel

[<AutoOpen>]
module LabelBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a Label widget with a text</summary>
        /// <param name="text">The text value</param>
        static member inline Label<'msg>(text: string) =
            WidgetBuilder<'msg, IFabMvuLabel>(Label.WidgetKey, Label.Text.WithValue(text))

[<Extension>]
type LabelModifiers =
    /// <summary>Link a ViewRef to access the direct Label control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuLabel>, value: ViewRef<Label>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
