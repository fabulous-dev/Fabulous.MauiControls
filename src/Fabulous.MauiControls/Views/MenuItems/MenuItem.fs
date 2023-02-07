namespace Fabulous.Maui

open System
open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabMenuItem =
    inherit IFabElement

module MenuItem =
    let WidgetKey = Widgets.register<MenuItem>()

    let Accelerator =
        Attributes.defineBindableWithEquality MenuItem.AcceleratorProperty

    let Clicked =
        Attributes.defineEventNoArg "MenuItem_Clicked" (fun target -> (target :?> MenuItem).Clicked)

    let IconImageSource =
        Attributes.defineBindableWithEquality MenuItem.IconImageSourceProperty

    let IsDestructive = Attributes.defineBindableBool MenuItem.IsDestructiveProperty

    let Text = Attributes.defineBindableWithEquality MenuItem.TextProperty

[<AutoOpen>]
module MenuItemBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a MenuItem widget with a text and a Click callback</summary>
        /// <param name="text">The text</param>
        /// <param name="onClicked">The click callback</param>
        static member inline MenuItem<'msg>(text: string, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabMenuItem>(MenuItem.WidgetKey, MenuItem.Text.WithValue(text), MenuItem.Clicked.WithValue(onClicked))

[<Extension>]
type MenuItemModifiers =
    /// <summary>Set the accelerator of this widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The accelerator value</param>
    [<Extension>]
    static member inline accelerator(this: WidgetBuilder<'msg, #IFabMenuItem>, value: Accelerator) =
        this.AddScalar(MenuItem.Accelerator.WithValue(value))

    /// <summary>Set the source of the icon image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The source of the icon image</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: ImageSource) =
        this.AddScalar(MenuItem.IconImageSource.WithValue(value))

    /// <summary>Set a value that indicates whether or not the menu item removes its associated widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The state value</param>
    [<Extension>]
    static member inline isDestructive(this: WidgetBuilder<'msg, #IFabMenuItem>, value: bool) =
        this.AddScalar(MenuItem.IsDestructive.WithValue(value))

    /// <summary>Link a ViewRef to access the direct MenuItem control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMenuItem>, value: ViewRef<MenuItem>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type MenuItemExtraModifiers =
    /// <summary>Set the source of the icon image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The source of the icon image</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: string) =
        this.icon(ImageSource.FromFile(value))

    /// <summary>Set the source of the icon image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The source of the icon image</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: Uri) =
        this.icon(ImageSource.FromUri(value))

    /// <summary>Set the source of the icon image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The source of the icon image</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: Stream) =
        this.icon(ImageSource.FromStream(fun () -> value))