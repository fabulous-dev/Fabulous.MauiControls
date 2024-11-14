namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module WebViewComponent =
    let ProcessTerminated = Attributes.Component.defineEvent "WebViewComponent_ProcessTerminated" (fun target -> (target :?> WebView).ProcessTerminated)
    
    let Navigated =
        Attributes.Component.defineEvent<WebNavigatedEventArgs> "WebViewComponent_Navigated" (fun target -> (target :?> WebView).Navigated)

    let Navigating =
        Attributes.Component.defineEvent<WebNavigatingEventArgs> "WebViewComponent_Navigating" (fun target -> (target :?> WebView).Navigating)

[<Extension>]
type WebViewComponentModifiers() =
    /// <summary>Listen for the ProcessTerminated event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onProcessTerminated(this: WidgetBuilder<unit, #IFabWebView>, fn: WebViewProcessTerminatedEventArgs -> unit) =
        this.AddScalar(WebViewComponent.ProcessTerminated.WithValue(fn))
    
    /// <summary>Listen for the Navigated event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onNavigated(this: WidgetBuilder<unit, #IFabWebView>, fn: WebNavigatedEventArgs -> unit) =
        this.AddScalar(WebViewComponent.Navigated.WithValue(fn))

    /// <summary>Listen for the Navigating event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onNavigating(this: WidgetBuilder<unit, #IFabWebView>, fn: WebNavigatingEventArgs -> unit) =
        this.AddScalar(WebViewComponent.Navigating.WithValue(fn))
