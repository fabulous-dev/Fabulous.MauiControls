namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module CheckBoxMvu =
    let IsCheckedWithEvent =
        Attributes.Mvu.defineBindableWithEvent "CheckBoxMvu_CheckedChanged" CheckBox.IsCheckedProperty (fun target -> (target :?> CheckBox).CheckedChanged)

[<AutoOpen>]
module CheckBoxMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a CheckBox widget with a state and listen for state changes</summary>
        /// <param name="isChecked">The state of the checkbox</param>
        /// <param name="onCheckedChanged">Message to dispatch</param>
        static member inline CheckBox(isChecked: bool, onCheckedChanged: bool -> 'msg) =
            WidgetBuilder<'msg, IFabCheckBox>(
                CheckBox.WidgetKey,
                CheckBoxMvu.IsCheckedWithEvent.WithValue(
                    MsgValueEventData.create isChecked (fun (args: CheckedChangedEventArgs) -> onCheckedChanged args.Value)
                )
            )
