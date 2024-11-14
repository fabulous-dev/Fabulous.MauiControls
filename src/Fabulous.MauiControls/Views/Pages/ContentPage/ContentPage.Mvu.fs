namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous

module ContentPageMvu =
    let SizeAllocated =
        Attributes.Mvu.defineEvent<SizeAllocatedEventArgs> "ContentPageMvu_SizeAllocated" (fun target -> (target :?> FabContentPage).SizeAllocated)

[<Extension>]
type ContentPageMvuModifiers =
    /// <summary>Listen for SizeAllocated event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSizeAllocated(this: WidgetBuilder<'msg, #IFabContentPage>, fn: SizeAllocatedEventArgs -> 'msg) =
        this.AddScalar(ContentPageMvu.SizeAllocated.WithValue(fn))
