namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module EntryMvu =
    let Completed =
        Attributes.Mvu.defineEventNoArg "EntryMvu_Completed" (fun target -> (target :?> Entry).Completed)

[<AutoOpen>]
module EntryMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an Entry widget with a text and listen for text changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline Entry(text: string, onTextChanged: string -> 'msg) =
            WidgetBuilder<'msg, IFabEntry>(
                Entry.WidgetKey,
                InputViewMvu.TextWithEvent.WithValue(MsgValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EntryMvuModifiers =
    /// <summary>Listen for the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<'msg, #IFabEntry>, msg: 'msg) =
        this.AddScalar(EntryMvu.Completed.WithValue(MsgValue(msg)))
        