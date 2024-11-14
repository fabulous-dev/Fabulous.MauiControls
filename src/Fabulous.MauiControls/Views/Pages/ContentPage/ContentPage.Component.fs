namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous

module ContentPageComponent =
    let SizeAllocated =
        Attributes.Component.defineEvent<SizeAllocatedEventArgs> "ContentPageComponent_SizeAllocated" (fun target -> (target :?> FabContentPage).SizeAllocated)

[<Extension>]
type ContentPageComponentModifiers =
    /// <summary>Listen for SizeAllocated event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSizeAllocated(this: WidgetBuilder<unit, #IFabContentPage>, fn: SizeAllocatedEventArgs -> unit) =
        this.AddScalar(ContentPageComponent.SizeAllocated.WithValue(fn))
