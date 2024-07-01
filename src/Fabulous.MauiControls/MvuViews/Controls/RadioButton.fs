namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

type IFabMvuRadioButton =
    inherit IFabMvuTemplatedView
    inherit IFabRadioButton

module RadioButton =
    let IsCheckedWithEvent =
        MvuAttributes.defineBindableWithEvent "RadioButton_CheckedChanged" RadioButton.IsCheckedProperty (fun target -> (target :?> RadioButton).CheckedChanged)

[<AutoOpen>]
module RadioButtonBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a RadioButton widget with a content, a checked state and listen for the checked state changes</summary>
        /// <param name="content">The content</param>
        /// <param name="isChecked">The checked state</param>
        /// <param name="onChecked">Message to dispatch</param>
        static member inline RadioButton<'msg>(content: string, isChecked: bool, onChecked: bool -> 'msg) =
            WidgetBuilder<'msg, IFabMvuRadioButton>(
                RadioButton.WidgetKey,
                RadioButton.IsCheckedWithEvent.WithValue(MvuValueEventData.create isChecked (fun (args: CheckedChangedEventArgs) -> onChecked args.Value)),
                RadioButton.ContentString.WithValue(content)
            )

        /// <summary>Create a RadioButton widget with a content, a checked state and listen for the checked state changes</summary>
        /// <param name="content">The content widget</param>
        /// <param name="isChecked">The checked state</param>
        /// <param name="onChecked">Message to dispatch</param>
        static member inline RadioButton(content: WidgetBuilder<'msg, #IFabView>, isChecked: bool, onChecked: bool -> 'msg) =
            WidgetBuilder<'msg, IFabMvuRadioButton>(
                RadioButton.WidgetKey,
                AttributesBundle(
                    StackList.one(
                        RadioButton.IsCheckedWithEvent.WithValue(
                            MvuValueEventData.create isChecked (fun (args: CheckedChangedEventArgs) -> onChecked args.Value)
                        )
                    ),
                    ValueSome [| RadioButton.ContentWidget.WithValue(content.Compile()) |],
                    ValueNone
                )
            )

[<Extension>]
type RadioButtonModifiers =
    /// <summary>Link a ViewRef to access the direct RadioButton control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuRadioButton>, value: ViewRef<RadioButton>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
