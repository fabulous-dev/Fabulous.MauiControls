namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module HybridWebViewComponent =
    let RawMessageReceived = Attributes.Component.defineEvent "HybridWebViewComponent_RawMessageReceived" (fun target -> (target :?> HybridWebView).RawMessageReceived)
    
[<Extension>]
type HybridWebViewComponentModifiers =
    /// <summary>Listen for the RawMessageReceived event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onRawMessageReceived(this: WidgetBuilder<unit, #IFabHybridWebView>, fn: HybridWebViewRawMessageReceivedEventArgs -> unit) =
        this.AddScalar(HybridWebViewComponent.RawMessageReceived.WithValue(fn))