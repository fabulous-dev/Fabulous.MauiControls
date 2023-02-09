namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.PlatformConfiguration
open Microsoft.Maui.Graphics

type IFabTabbedPage =
    inherit IFabMultiPageOfPage

module TabbedPage =
    let WidgetKey = Widgets.register<TabbedPage>()

    let BarBackgroundColor =
        Attributes.defineBindableWithEquality TabbedPage.BarBackgroundColorProperty

    let BarBackgroundFabColor =
        Attributes.defineBindableColor TabbedPage.BarBackgroundColorProperty

    let BarTextColor =
        Attributes.defineBindableWithEquality TabbedPage.BarTextColorProperty

    let BarTextFabColor = Attributes.defineBindableColor TabbedPage.BarTextColorProperty

    let SelectedTabColor =
        Attributes.defineBindableWithEquality TabbedPage.SelectedTabColorProperty

    let SelectedTabFabColor =
        Attributes.defineBindableColor TabbedPage.SelectedTabColorProperty

    let ToolbarPlacement =
        Attributes.defineSimpleScalarWithEquality<AndroidSpecific.ToolbarPlacement> "TabbedPage_ToolbarPlacement" (fun _ newValueOpt node ->
            let tabbedPage = node.Target :?> TabbedPage

            let value =
                match newValueOpt with
                | ValueNone -> AndroidSpecific.ToolbarPlacement.Default
                | ValueSome v -> v

            AndroidSpecific.TabbedPage.SetToolbarPlacement(tabbedPage, value))

    let UnselectedTabColor =
        Attributes.defineBindableWithEquality TabbedPage.UnselectedTabColorProperty

    let UnselectedTabFabColor =
        Attributes.defineBindableColor TabbedPage.UnselectedTabColorProperty

[<AutoOpen>]
module TabbedPageBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a TabbedPage widget</summary>
        static member inline TabbedPage<'msg>() =
            CollectionBuilder<'msg, IFabTabbedPage, IFabPage>(TabbedPage.WidgetKey, MultiPageOfPage.Children)

[<Extension>]
type TabbedPageModifiers =
    /// <summary>Set the background color of the bar</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The background color of the bar</param>
    [<Extension>]
    static member inline barBackgroundColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: Color) =
        this.AddScalar(TabbedPage.BarBackgroundColor.WithValue(value))

    /// <summary>Set the background color of the bar</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The background color of the bar</param>
    [<Extension>]
    static member inline barBackgroundColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: FabColor) =
        this.AddScalar(TabbedPage.BarBackgroundFabColor.WithValue(value))

    /// <summary>Set the text color of the bar</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The text color of the bar</param>
    [<Extension>]
    static member inline barTextColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: Color) =
        this.AddScalar(TabbedPage.BarTextColor.WithValue(value))

    /// <summary>Set the text color of the bar</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The text color of the bar</param>
    [<Extension>]
    static member inline barTextColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: FabColor) =
        this.AddScalar(TabbedPage.BarTextFabColor.WithValue(value))

    /// <summary>Set the color of the selected tab</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the selected tab</param>
    [<Extension>]
    static member inline selectedTabColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: Color) =
        this.AddScalar(TabbedPage.SelectedTabColor.WithValue(value))

    /// <summary>Set the color of the selected tab</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the selected tab</param>
    [<Extension>]
    static member inline selectedTabColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: FabColor) =
        this.AddScalar(TabbedPage.SelectedTabFabColor.WithValue(value))

    /// <summary>Set the color of the unselected tab</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the unselected tab</param>
    [<Extension>]
    static member inline unselectedTabColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: Color) =
        this.AddScalar(TabbedPage.UnselectedTabColor.WithValue(value))

    /// <summary>Set the color of the unselected tab</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the unselected tab</param>
    [<Extension>]
    static member inline unselectedTabColor(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: FabColor) =
        this.AddScalar(TabbedPage.UnselectedTabFabColor.WithValue(value))

    /// <summary>Link a ViewRef to access the direct TabbedPage control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabTabbedPage>, value: ViewRef<TabbedPage>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type TabbedPagePlatformModifiers =
    /// <summary>Android platform specific. Set the toolbar placement</summary>
    /// <param name="this">Current widget</param>
    /// <param name= "value">The toolbar placement value</param>
    [<Extension>]
    static member inline toolbarPlacement(this: WidgetBuilder<'msg, #IFabTabbedPage>, value: AndroidSpecific.ToolbarPlacement) =
        this.AddScalar(TabbedPage.ToolbarPlacement.WithValue(value))
