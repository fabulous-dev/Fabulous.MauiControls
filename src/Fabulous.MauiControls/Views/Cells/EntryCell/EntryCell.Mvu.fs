namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module EntryCellMvu =
    let OnCompleted =
        Attributes.Mvu.defineEventNoArg "EntryCellMvu_Completed" (fun target -> (target :?> EntryCell).Completed)

    let TextWithEvent =
        Attributes.Mvu.defineBindableWithEvent "EntryCellMvu_TextChanged" EntryCell.TextProperty (fun target -> (target :?> FabEntryCell).TextChanged)

[<AutoOpen>]
module EntryCellMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an EntryCell with a label, a text, and listen to text changes</summary>
        /// <param name="label">The label value</param>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline EntryCell(label: string, text: string, onTextChanged: string -> 'msg) =
            WidgetBuilder<'msg, IFabEntryCell>(
                EntryCell.WidgetKey,
                EntryCell.Label.WithValue(label),
                EntryCellMvu.TextWithEvent.WithValue(MsgValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EntryCellMvuModifiers =

    /// <summary>Listen to the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<'msg, #IFabEntryCell>, msg: 'msg) =
        this.AddScalar(EntryCellMvu.OnCompleted.WithValue(MsgValue(msg)))
