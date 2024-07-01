namespace Fabulous.Maui.Mvu

open System
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuWebView =
    inherit IFabMvuView
    inherit IFabWebView

module WebView =
    let Navigated =
        MvuAttributes.defineEvent<WebNavigatedEventArgs> "WebView_Navigated" (fun target -> (target :?> WebView).Navigated)

    let Navigating =
        MvuAttributes.defineEvent<WebNavigatingEventArgs> "WebView_Navigating" (fun target -> (target :?> WebView).Navigating)

[<AutoOpen>]
module WebViewBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a WebView with a source</summary>
        /// <param name="source">The web source</param>
        static member inline WebView<'msg>(source: WebViewSource) =
            WidgetBuilder<'msg, IFabMvuWebView>(WebView.WidgetKey, WebView.Source.WithValue(source))

        /// <summary>Create a WebView with an HTML content</summary>
        /// <param name="html">The HTML content</param>
        /// <param name="baseUrl">The base URL</param>
        static member inline WebView<'msg>(html: string, ?baseUrl: string) =
            let source =
                match baseUrl with
                | Some url -> HtmlWebViewSource(Html = html, BaseUrl = url)
                | None -> HtmlWebViewSource(Html = html)

            View.WebView<'msg>(source)

        /// <summary>Create a WebView with a Uri source</summary>
        /// <param name="uri">The Uri source</param>
        static member inline WebView<'msg>(uri: Uri) =
            View.WebView<'msg>(WebViewSource.op_Implicit uri)

        /// <summary>Create a WebView with a Url source</summary>
        /// <param name="url">The Url source</param>
        static member inline WebView<'msg>(url: string) =
            View.WebView<'msg>(WebViewSource.op_Implicit url)

[<Extension>]
type WebViewModifiers() =
    /// <summary>Listen for the Navigated event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onNavigated(this: WidgetBuilder<'msg, #IFabMvuWebView>, fn: WebNavigatedEventArgs -> 'msg) =
        this.AddScalar(WebView.Navigated.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the Navigating event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onNavigating(this: WidgetBuilder<'msg, #IFabMvuWebView>, fn: WebNavigatingEventArgs -> 'msg) =
        this.AddScalar(WebView.Navigating.WithValue(fun args -> fn args |> box))

    /// <summary>Link a ViewRef to access the direct WebView control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuWebView>, value: ViewRef<WebView>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
