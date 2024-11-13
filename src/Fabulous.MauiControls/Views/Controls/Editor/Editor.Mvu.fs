namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module EditorMvu =
    let Completed =
        Attributes.Mvu.defineEventNoArg "EditorMvu_Completed" (fun target -> (target :?> Editor).Completed)

[<AutoOpen>]
module EditorMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an Editor widget with a text and listen for text changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline Editor(text: string, onTextChanged: string -> 'msg) =
            WidgetBuilder<'msg, IFabEditor>(
                Editor.WidgetKey,
                InputViewMvu.TextWithEvent.WithValue(MsgValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EditorMvuModifiers =
    /// <summary>Listen for the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<'msg, #IFabEditor>, msg: 'msg) =
        this.AddScalar(EditorMvu.Completed.WithValue(MsgValue(msg)))
