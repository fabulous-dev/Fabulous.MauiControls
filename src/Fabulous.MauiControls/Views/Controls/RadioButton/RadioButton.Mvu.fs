namespace Fabulous.Maui

open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

module RadioButtonMvu =
    let IsCheckedWithEvent =
        Attributes.Mvu.defineBindableWithEvent "RadioButtonMvu_CheckedChanged" RadioButton.IsCheckedProperty (fun target ->
            (target :?> RadioButton).CheckedChanged)

[<AutoOpen>]
module RadioButtonMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a RadioButton widget with a content, a checked state and listen for the checked state changes</summary>
        /// <param name="content">The content</param>
        /// <param name="isChecked">The checked state</param>
        /// <param name="onChecked">Message to dispatch</param>
        static member inline RadioButton(content: string, isChecked: bool, onChecked: bool -> 'msg) =
            WidgetBuilder<'msg, IFabRadioButton>(
                RadioButton.WidgetKey,
                RadioButtonMvu.IsCheckedWithEvent.WithValue(MsgValueEventData.create isChecked (fun (args: CheckedChangedEventArgs) -> onChecked args.Value)),
                RadioButton.ContentString.WithValue(content)
            )

        /// <summary>Create a RadioButton widget with a content, a checked state and listen for the checked state changes</summary>
        /// <param name="content">The content widget</param>
        /// <param name="isChecked">The checked state</param>
        /// <param name="onChecked">Message to dispatch</param>
        static member inline RadioButton(content: WidgetBuilder<'msg, #IFabView>, isChecked: bool, onChecked: bool -> 'msg) =
            WidgetBuilder<'msg, IFabRadioButton>(
                RadioButton.WidgetKey,
                AttributesBundle(
                    StackList.one(
                        RadioButtonMvu.IsCheckedWithEvent.WithValue(
                            MsgValueEventData.create isChecked (fun (args: CheckedChangedEventArgs) -> onChecked args.Value)
                        )
                    ),
                    ValueSome [| RadioButton.ContentWidget.WithValue(content.Compile()) |],
                    ValueNone
                )
            )
