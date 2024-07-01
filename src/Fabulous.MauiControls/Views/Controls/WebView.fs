namespace Fabulous.Maui

open System.Net
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.PlatformConfiguration

type IFabWebView =
    inherit IFabView

module WebView =
    let WidgetKey = Widgets.register<WebView>()

    let CanGoBack = Attributes.defineBindableBool WebView.CanGoBackProperty

    let CanGoForward = Attributes.defineBindableBool WebView.CanGoForwardProperty

    let Cookies =
        Attributes.defineBindableWithEquality<CookieContainer> WebView.CookiesProperty

    let Source =
        Attributes.defineBindableWithEquality<WebViewSource> WebView.SourceProperty

module WebViewPlatform =
    let DisplayZoomControls =
        Attributes.defineBool "WebView_DisplayZoomControls" (fun _ newValueOpt node ->
            let webview = node.Target :?> WebView

            let value =
                match newValueOpt with
                | ValueNone -> false
                | ValueSome v -> v

            AndroidSpecific.WebView.SetDisplayZoomControls(webview, value))

    let EnableZoomControls =
        Attributes.defineBool "WebView_EnableZoomControls" (fun _ newValueOpt node ->
            let webview = node.Target :?> WebView

            let value =
                match newValueOpt with
                | ValueNone -> false
                | ValueSome v -> v

            AndroidSpecific.WebView.SetEnableZoomControls(webview, value))

[<Extension>]
type WebViewModifiers() =
    /// <summary>Set a value that indicates whether the user can navigate to previous pages</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if the user can navigate to previous pages; otherwise, false</param>
    [<Extension>]
    static member inline canGoBack(this: WidgetBuilder<'msg, #IFabWebView>, value: bool) =
        this.AddScalar(WebView.CanGoBack.WithValue(value))

    /// <summary>Set a value that indicates whether the user can navigate to the next pages</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">true if the user can navigate to the next pages; otherwise, false</param>
    [<Extension>]
    static member inline canGoForward(this: WidgetBuilder<'msg, #IFabWebView>, value: bool) =
        this.AddScalar(WebView.CanGoForward.WithValue(value))

    /// <summary>Set the cookie container</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The cookie container</param>
    [<Extension>]
    static member inline cookies(this: WidgetBuilder<'msg, #IFabWebView>, value: CookieContainer) =
        this.AddScalar(WebView.Cookies.WithValue(value))

[<Extension>]
type WebViewPlatformModifiers =
    /// <summary>Android platform-specific. Set a value indicating whether the zoom controls are enabled</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the zoom controls are enabled</param>
    [<Extension>]
    static member inline enableZoomControls(this: WidgetBuilder<'msg, #IFabWebView>, value: bool) =
        this.AddScalar(WebViewPlatform.EnableZoomControls.WithValue(value))

    /// <summary>Android platform-specific. Set a value indicating whether the zoom controls are displayed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the zoom controls are displayed</param>
    [<Extension>]
    static member displayZoomControls(this: WidgetBuilder<'msg, #IFabWebView>, value: bool) =
        this.AddScalar(WebViewPlatform.DisplayZoomControls.WithValue(value))
