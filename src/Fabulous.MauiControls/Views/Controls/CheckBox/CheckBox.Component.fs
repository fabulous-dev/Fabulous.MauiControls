namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module CheckBoxComponent =
    let IsCheckedWithEvent =
        Attributes.Component.defineBindableWithEvent "CheckBoxComponent_CheckedChanged" CheckBox.IsCheckedProperty (fun target -> (target :?> CheckBox).CheckedChanged)

[<AutoOpen>]
module CheckBoxComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a CheckBox widget with a state and listen for state changes</summary>
        /// <param name="isChecked">The state of the checkbox</param>
        /// <param name="onCheckedChanged">Message to dispatch</param>
        static member inline CheckBox(isChecked: bool, onCheckedChanged: bool -> unit) =
            WidgetBuilder<unit, IFabCheckBox>(
                CheckBox.WidgetKey,
                CheckBoxComponent.IsCheckedWithEvent.WithValue(ValueEventData.create isChecked (fun (args: CheckedChangedEventArgs) -> onCheckedChanged args.Value))
            )
