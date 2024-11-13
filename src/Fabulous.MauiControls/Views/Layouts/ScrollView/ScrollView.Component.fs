namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ScrollViewComponent =
    let Scrolled =
        Attributes.Component.defineEvent<ScrolledEventArgs> "ScrollViewComponent_Scrolled" (fun target -> (target :?> ScrollView).Scrolled)

[<Extension>]
type ScrollViewComponentModifiers =
    /// <summary>Listen for the Scrolled event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrolled(this: WidgetBuilder<'msg, #IFabScrollView>, fn: ScrolledEventArgs -> unit) =
        this.AddScalar(ScrollViewComponent.Scrolled.WithValue(fn))
