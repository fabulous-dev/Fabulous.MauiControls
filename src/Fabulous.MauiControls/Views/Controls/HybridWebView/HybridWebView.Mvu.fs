namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module HybridWebViewMvu =
    let RawMessageReceived = Attributes.Mvu.defineEvent "HybridWebViewMvu_RawMessageReceived" (fun target -> (target :?> HybridWebView).RawMessageReceived)
    
[<Extension>]
type HybridWebViewMvuModifiers =
    /// <summary>Listen for the RawMessageReceived event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onRawMessageReceived(this: WidgetBuilder<'msg, #IFabHybridWebView>, fn: HybridWebViewRawMessageReceivedEventArgs -> 'msg) =
        this.AddScalar(HybridWebViewMvu.RawMessageReceived.WithValue(fn))