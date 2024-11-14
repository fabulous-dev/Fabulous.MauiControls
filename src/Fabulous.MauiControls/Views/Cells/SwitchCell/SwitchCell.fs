namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabSwitchCell =
    inherit IFabCell

module SwitchCell =
    let WidgetKey = Widgets.register<SwitchCell>()

    let OnColor = Attributes.defineBindableColor SwitchCell.OnColorProperty

    let Text: SimpleScalarAttributeDefinition<string> =
        Attributes.defineBindableWithEquality SwitchCell.TextProperty

[<Extension>]
type SwitchCellModifiers =
    /// <summary>Set the color of the on state</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the on state in the light theme.</param>
    [<Extension>]
    static member inline colorOn(this: WidgetBuilder<'msg, #IFabSwitchCell>, value: Color) =
        this.AddScalar(SwitchCell.OnColor.WithValue(value))

    /// <summary>Link a ViewRef to access the direct SwitchCell control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSwitchCell>, value: ViewRef<SwitchCell>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
