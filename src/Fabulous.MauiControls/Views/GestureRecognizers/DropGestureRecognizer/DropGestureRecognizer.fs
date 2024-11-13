namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabDropGestureRecognizer =
    inherit IFabGestureRecognizer

module DropGestureRecognizer =
    let WidgetKey = Widgets.register<DropGestureRecognizer>()

    let AllowDrop =
        Attributes.defineBindableBool DropGestureRecognizer.AllowDropProperty
        
[<Extension>]
type DropGestureRecognizerModifiers =
    /// <summary>Set whether users are allowed to drop</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true to allow users to drop; otherwise, false</param>
    [<Extension>]
    static member inline allowDrop(this: WidgetBuilder<'msg, #IFabDropGestureRecognizer>, value: bool) =
        this.AddScalar(DropGestureRecognizer.AllowDrop.WithValue(value))

    /// <summary>Link a ViewRef to access the direct DropGestureRecognizer control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabDropGestureRecognizer>, value: ViewRef<DropGestureRecognizer>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
