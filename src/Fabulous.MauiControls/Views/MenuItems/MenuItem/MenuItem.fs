namespace Fabulous.Maui

open System
open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Microsoft.Maui.Controls

type IFabMenuItem =
    inherit IFabElement

module MenuItem =
    let WidgetKey = Widgets.register<MenuItem>()

    let IconImageSource =
        Attributes.defineBindableImageSource MenuItem.IconImageSourceProperty

    let IsDestructive = Attributes.defineBindableBool MenuItem.IsDestructiveProperty

    let Text: SimpleScalarAttributeDefinition<string> =
        Attributes.defineBindableWithEquality MenuItem.TextProperty

[<Extension>]
type MenuItemModifiers =
    /// <summary>Set the source of the icon image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The source of the icon image</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: ImageSource) =
        this.AddScalar(MenuItem.IconImageSource.WithValue(ImageSourceValue.Source value))

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
        this.AddScalar(MenuItem.IconImageSource.WithValue(ImageSourceValue.File value))

    /// <summary>Set the source of the icon image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The source of the icon image</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: Uri) =
        this.AddScalar(MenuItem.IconImageSource.WithValue(ImageSourceValue.Uri value))

    /// <summary>Set the source of the icon image</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The source of the icon image</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: Stream) =
        this.AddScalar(MenuItem.IconImageSource.WithValue(ImageSourceValue.Stream value))
