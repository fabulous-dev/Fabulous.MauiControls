namespace Fabulous.Maui

open System
open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.PlatformConfiguration

type IFabPage =
    inherit IFabVisualElement

module Page =
    let AppearingMsg =
        Attributes.defineEventNoArg "Page_AppearingMsg" (fun target -> (target :?> Page).Appearing)

    let AppearingFn =
        Attributes.defineEventNoArgNoDispatch "Page_AppearingFn" (fun target -> (target :?> Page).Appearing)

    let BackgroundImageSource =
        Attributes.defineBindableImageSource Page.BackgroundImageSourceProperty

    let DisappearingMsg =
        Attributes.defineEventNoArg "Page_DisappearingMsg" (fun target -> (target :?> Page).Disappearing)

    let DisappearingFn =
        Attributes.defineEventNoArgNoDispatch "Page_DisappearingFn" (fun target -> (target :?> Page).Disappearing)

    let IconImageSource =
        Attributes.defineBindableImageSource Page.IconImageSourceProperty

    let IsBusy = Attributes.defineBindableBool Page.IsBusyProperty

    let NavigatedToMsg =
        Attributes.defineEvent "NavigatedToMsg" (fun target -> (target :?> Page).NavigatedTo)

    let NavigatedToFn =
        Attributes.defineEventNoDispatch "NavigatedToFn" (fun target -> (target :?> Page).NavigatedTo)

    let NavigatedFromMsg =
        Attributes.defineEvent "NavigatedFromMsg" (fun target -> (target :?> Page).NavigatedFrom)

    let NavigatedFromFn =
        Attributes.defineEventNoDispatch "NavigatedFromFn" (fun target -> (target :?> Page).NavigatedFrom)

    let Padding = Attributes.defineBindableWithEquality Page.PaddingProperty

    let Title = Attributes.defineBindableWithEquality Page.TitleProperty

    let ToolbarItems =
        Attributes.defineListWidgetCollection "Page_ToolbarItems" (fun target -> (target :?> Page).ToolbarItems)

    let UseSafeArea =
        Attributes.defineSimpleScalarWithEquality "Page_UseSafeArea" (fun _ newValueOpt node ->
            let page = node.Target :?> Page

            let value =
                match newValueOpt with
                | ValueNone -> false
                | ValueSome v -> v

            iOSSpecific.Page.SetUseSafeArea(page, value))

[<Extension>]
type PageModifiers =
    /// <summary>Set the image source of the background</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline backgroundImageSource(this: WidgetBuilder<'msg, #IFabPage>, value: ImageSource) =
        this.AddScalar(Page.BackgroundImageSource.WithValue(ImageSourceValue.Source value))

    /// <summary>Set the image source of the icon</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabPage>, value: ImageSource) =
        this.AddScalar(Page.IconImageSource.WithValue(ImageSourceValue.Source value))

    /// <summary>Set the page as busy. This will cause the global activity indicator to show a busy state</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The busy value</param>
    [<Extension>]
    static member inline isBusy(this: WidgetBuilder<'msg, #IFabPage>, value: bool) =
        this.AddScalar(Page.IsBusy.WithValue(value))

    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(Page.AppearingMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen to the Appearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onAppearing(this: WidgetBuilder<'msg, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(Page.AppearingFn.WithValue(fn))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(Page.DisappearingMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen to the Disappearing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onDisappearing(this: WidgetBuilder<'msg, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(Page.DisappearingFn.WithValue(fn))

    [<Extension>]
    static member inline onNavigatedTo(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(Page.NavigatedToMsg.WithValue(fun _ -> msg))

    [<Extension>]
    static member inline onNavigatedTo(this: WidgetBuilder<'msg, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(Page.NavigatedToFn.WithValue(fun _ -> fn()))

    [<Extension>]
    static member inline onNavigatedFrom(this: WidgetBuilder<'msg, #IFabPage>, msg: 'msg) =
        this.AddScalar(Page.NavigatedFromMsg.WithValue(fun _ -> msg))

    [<Extension>]
    static member inline onNavigatedFrom(this: WidgetBuilder<'msg, #IFabPage>, fn: unit -> unit) =
        this.AddScalar(Page.NavigatedFromFn.WithValue(fun _ -> fn()))

    /// <summary>Set the padding inside the widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The padding value</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabPage>, value: Thickness) =
        this.AddScalar(Page.Padding.WithValue(value))

    /// <summary>Set the title of the page</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The title value</param>
    [<Extension>]
    static member inline title(this: WidgetBuilder<'msg, #IFabPage>, value: string) =
        this.AddScalar(Page.Title.WithValue(value))

    /// <summary>Set the toolbar items of this page menu</summary>
    /// <param name="this">Current widget</param>
    [<Extension>]
    static member inline toolbarItems<'msg, 'marker when 'msg: equality and 'marker :> IFabPage>(this: WidgetBuilder<'msg, 'marker>) =
        WidgetHelpers.buildAttributeCollection<'msg, 'marker, IFabToolbarItem> Page.ToolbarItems this

[<Extension>]
type PageExtraModifiers =
    /// <summary>Set the image source of the background</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline backgroundImageSource(this: WidgetBuilder<'msg, #IFabPage>, value: string) =
        this.AddScalar(Page.BackgroundImageSource.WithValue(ImageSourceValue.File value))

    /// <summary>Set the image source of the background</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline backgroundImageSource(this: WidgetBuilder<'msg, #IFabPage>, value: Uri) =
        this.AddScalar(Page.BackgroundImageSource.WithValue(ImageSourceValue.Uri value))

    /// <summary>Set the image source of the background</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline backgroundImageSource(this: WidgetBuilder<'msg, #IFabPage>, value: Stream) =
        this.AddScalar(Page.BackgroundImageSource.WithValue(ImageSourceValue.Stream value))

    /// <summary>Set the image source of the icon</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabPage>, value: string) =
        this.AddScalar(Page.IconImageSource.WithValue(ImageSourceValue.File value))

    /// <summary>Set the image source of the icon</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabPage>, value: Uri) =
        this.AddScalar(Page.IconImageSource.WithValue(ImageSourceValue.Uri value))

    /// <summary>Set the image source of the icon</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The image source</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabPage>, value: Stream) =
        this.AddScalar(Page.IconImageSource.WithValue(ImageSourceValue.Stream value))

    /// <summary>Set the padding inside the widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="uniformSize">The uniform padding value that will be applied to all sides</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabPage>, uniformSize: float) = this.padding(Thickness(uniformSize))

    /// <summary>Set the padding inside the widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="horizontalSize">The padding value that will be applied to both left and right sides</param>
    /// <param name="verticalSize">The padding value that will be applied to both top and bottom sides</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabPage>, horizontalSize: float, verticalSize: float) =
        this.padding(Thickness(horizontalSize, verticalSize))

    /// <summary>Set the padding inside the widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="left">The left padding value</param>
    /// <param name="top">The top padding value</param>
    /// <param name="right">The right padding value</param>
    /// <param name="bottom">The bottom padding value</param>
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabPage>, left: float, top: float, right: float, bottom: float) =
        this.padding(Thickness(left, top, right, bottom))

[<Extension>]
type PagePlatformModifiers =
    /// <summary>iOS platform specific. Ignore the safe area view</summary>
    [<Extension>]
    static member inline ignoreSafeArea(this: WidgetBuilder<'msg, #IFabPage>) =
        this.AddScalar(Page.UseSafeArea.WithValue(false))

[<Extension>]
type PageYieldExtensions =
    [<Extension>]
    static member inline Yield(_: AttributeCollectionBuilder<'msg, #IFabPage, IFabToolbarItem>, x: WidgetBuilder<'msg, #IFabToolbarItem>) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: AttributeCollectionBuilder<'msg, #IFabPage, IFabToolbarItem>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabToolbarItem>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
