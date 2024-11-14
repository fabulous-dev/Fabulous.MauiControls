namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ScrollViewMvu =
    let Scrolled =
        Attributes.Mvu.defineEvent<ScrolledEventArgs> "ScrollViewMvu_Scrolled" (fun target -> (target :?> ScrollView).Scrolled)

[<Extension>]
type ScrollViewMvuModifiers =
    /// <summary>Listen for the Scrolled event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onScrolled(this: WidgetBuilder<'msg, #IFabScrollView>, fn: ScrolledEventArgs -> 'msg) =
        this.AddScalar(ScrollViewMvu.Scrolled.WithValue(fun args -> fn args |> box))
