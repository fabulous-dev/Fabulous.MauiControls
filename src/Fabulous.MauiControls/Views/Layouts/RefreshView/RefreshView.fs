namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabRefreshView =
    inherit IFabContentView

module RefreshView =
    let WidgetKey = Widgets.register<RefreshView>()

    let IsRefreshing = Attributes.defineBindableBool RefreshView.IsRefreshingProperty

    let RefreshColor = Attributes.defineBindableColor RefreshView.RefreshColorProperty

[<Extension>]
type RefreshViewModifiers =
    /// <summary>Set the color of the refresh indicator</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the refresh indicator</param>
    [<Extension>]
    static member inline refreshColor(this: WidgetBuilder<'msg, #IFabRefreshView>, value: Color) =
        this.AddScalar(RefreshView.RefreshColor.WithValue(value))

    /// <summary>Link a ViewRef to access the direct RefreshView control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabRefreshView>, value: ViewRef<RefreshView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
