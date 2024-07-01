namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentSpan =
    inherit IFabComponentElement
    inherit IFabSpan

[<AutoOpen>]
module SpanBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a Span widget with a text</summary>
        /// <param name="text">The text value</param>
        static member inline Span<'msg>(text: string) =
            WidgetBuilder<'msg, IFabComponentSpan>(Span.WidgetKey, Span.Text.WithValue(text))

[<Extension>]
type SpanModifiers =
    /// <summary>Link a ViewRef to access the direct Span control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentSpan>, value: ViewRef<Span>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
