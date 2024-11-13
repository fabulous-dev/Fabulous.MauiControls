namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module WebViewMvu =
    let Navigated =
        Attributes.Mvu.defineEvent<WebNavigatedEventArgs> "WebViewMvu_Navigated" (fun target -> (target :?> WebView).Navigated)

    let Navigating =
        Attributes.Mvu.defineEvent<WebNavigatingEventArgs> "WebViewMvu_Navigating" (fun target -> (target :?> WebView).Navigating)

[<Extension>]
type WebViewMvuModifiers() =
    /// <summary>Listen for the Navigated event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onNavigated(this: WidgetBuilder<'msg, #IFabWebView>, fn: WebNavigatedEventArgs -> 'msg) =
        this.AddScalar(WebViewMvu.Navigated.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the Navigating event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onNavigating(this: WidgetBuilder<'msg, #IFabWebView>, fn: WebNavigatingEventArgs -> 'msg) =
        this.AddScalar(WebViewMvu.Navigating.WithValue(fun args -> fn args |> box))
