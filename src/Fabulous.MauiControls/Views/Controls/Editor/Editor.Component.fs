namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module EditorComponent =
    let Completed =
        Attributes.Component.defineEventNoArg "EditorComponent_Completed" (fun target -> (target :?> Editor).Completed)

[<AutoOpen>]
module EditorComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an Editor widget with a text and listen for text changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline Editor(text: string, onTextChanged: string -> unit) =
            WidgetBuilder<unit, IFabEditor>(
                Editor.WidgetKey,
                InputViewComponent.TextWithEvent.WithValue(ValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EditorComponentModifiers =
    /// <summary>Listen for the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<unit, #IFabEditor>, fn: unit -> unit) =
        this.AddScalar(EditorComponent.Completed.WithValue(fn))
