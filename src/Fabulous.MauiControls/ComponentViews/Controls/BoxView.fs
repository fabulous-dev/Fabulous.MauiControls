namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabComponentBoxView =
    inherit IFabComponentView
    inherit IFabBoxView

[<AutoOpen>]
module BoxViewBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a BoxView widget with a color</summary>
        /// <param name="color">The color value</param>
        static member inline BoxView<'msg>(color: Color) =
            WidgetBuilder<'msg, IFabComponentBoxView>(BoxView.WidgetKey, BoxView.Color.WithValue(color))

[<Extension>]
type BoxViewModifiers =
    /// <summary>Link a ViewRef to access the direct BoxView control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentBoxView>, value: ViewRef<BoxView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
