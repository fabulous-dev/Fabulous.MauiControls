namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module EntryComponent =
    let Completed =
        Attributes.Component.defineEventNoArg "EntryComponent_Completed" (fun target -> (target :?> Entry).Completed)

[<AutoOpen>]
module EntryComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an Entry widget with a text and listen for text changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline Entry(text: string, onTextChanged: string -> unit) =
            WidgetBuilder<unit, IFabEntry>(
                Entry.WidgetKey,
                InputViewComponent.TextWithEvent.WithValue(ValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EntryComponentModifiers =
    /// <summary>Listen for the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<'msg, #IFabEntry>, fn: unit -> unit) =
        this.AddScalar(EntryComponent.Completed.WithValue(fn))
