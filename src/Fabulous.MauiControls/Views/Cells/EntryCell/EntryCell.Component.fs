namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module EntryCellComponent =
    let OnCompleted =
        Attributes.Component.defineEventNoArg "EntryCellComponent_Completed" (fun target -> (target :?> EntryCell).Completed)

    let TextWithEvent =
        Attributes.Component.defineBindableWithEvent "EntryCellComponent_TextChanged" EntryCell.TextProperty (fun target -> (target :?> FabEntryCell).TextChanged)

[<AutoOpen>]
module EntryCellComponentBuilders =
    type Fabulous.Maui.View with
    
        /// <summary>Create an EntryCell with a label, a text, and listen to text changes</summary>
        /// <param name="label">The label value</param>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline EntryCell(label: string, text: string, onTextChanged: string -> unit) =
            WidgetBuilder<unit, IFabEntryCell>(
                EntryCell.WidgetKey,
                EntryCell.Label.WithValue(label),
                EntryCellComponent.TextWithEvent.WithValue(ValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EntryCellComponentModifiers =
    /// <summary>Listen to the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<unit, #IFabEntryCell>, fn: unit -> unit) =
        this.AddScalar(EntryCellComponent.OnCompleted.WithValue(fn))