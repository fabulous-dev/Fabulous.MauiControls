namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentEntry =
    inherit IFabComponentInputView
    inherit IFabEntry

module Entry =
    let Completed =
        ComponentAttributes.defineEventNoArg "Entry_Completed" (fun target -> (target :?> Entry).Completed)

[<AutoOpen>]
module EntryBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create an Entry widget with a text and listen for text changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        static member inline Entry<'msg>(text: string, onTextChanged: string -> unit) =
            WidgetBuilder<'msg, IFabComponentEntry>(
                Entry.WidgetKey,
                InputView.TextWithEvent.WithValue(ComponentValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue))
            )

[<Extension>]
type EntryModifiers =
    /// <summary>Listen for the Completed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCompleted(this: WidgetBuilder<'msg, #IFabComponentEntry>, fn: unit -> unit) =
        this.AddScalar(Entry.Completed.WithValue(fn))

    /// <summary>Link a ViewRef to access the direct Entry control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentEntry>, value: ViewRef<Entry>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
