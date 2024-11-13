namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabSwipeItem =
    inherit IFabMenuItem

module SwipeItem =
    let WidgetKey = Widgets.register<SwipeItem>()

    let BackgroundColor =
        Attributes.defineBindableColor SwipeItem.BackgroundColorProperty

    let IsVisible = Attributes.defineBindableBool SwipeItem.IsVisibleProperty

[<Extension>]
type SwipeItemModifiers() =
    /// <summary>Set the background color of the SwipeItem</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The background color</param>
    [<Extension>]
    static member inline backgroundColor(this: WidgetBuilder<'msg, #IFabSwipeItem>, value: Color) =
        this.AddScalar(SwipeItem.BackgroundColor.WithValue(value))

    /// <summary>Set the visibility of the SwipeItem</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the SwipeItem is visible or not</param>
    [<Extension>]
    static member inline isVisible(this: WidgetBuilder<'msg, #IFabSwipeItem>, value: bool) =
        this.AddScalar(SwipeItem.IsVisible.WithValue(value))

    /// <summary>Link a ViewRef to access the direct SwipeItem control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSwipeItem>, value: ViewRef<SwipeItem>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
