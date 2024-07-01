namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuEditor =
    inherit IFabMvuInputView
    inherit IFabEditor

module Editor =
    let Completed =
        MvuAttributes.defineEventNoArg "Editor_Completed" (fun target -> (target :?> Editor).Completed)

[<AutoOpen>]
module EditorBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create an Editor widget with a text and listen for text changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline Editor<'msg>(text: string, onTextChanged: string -> 'msg) =
            WidgetBuilder<'msg, IFabMvuEditor>(
                Editor.WidgetKey,
                InputView.TextWithEvent.WithValue(MvuValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EditorModifiers =
    /// <summary>Listen for the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<'msg, #IFabMvuEditor>, msg: 'msg) =
        this.AddScalar(Editor.Completed.WithValue(MsgValue(msg)))

    /// <summary>Link a ViewRef to access the direct Editor control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuEditor>, value: ViewRef<Editor>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
