namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentEditor =
    inherit IFabComponentInputView
    inherit IFabEditor

module Editor =
    let Completed =
        ComponentAttributes.defineEventNoArg "Editor_Completed" (fun target -> (target :?> Editor).Completed)

[<AutoOpen>]
module EditorBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create an Editor widget with a text and listen for text changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline Editor<'msg>(text: string, onTextChanged: string -> unit) =
            WidgetBuilder<'msg, IFabComponentEditor>(
                Editor.WidgetKey,
                InputView.TextWithEvent.WithValue(ComponentValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EditorModifiers =
    /// <summary>Listen for the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<'msg, #IFabComponentEditor>, fn: unit -> unit) =
        this.AddScalar(Editor.Completed.WithValue(fn))

    /// <summary>Link a ViewRef to access the direct Editor control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentEditor>, value: ViewRef<Editor>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
