namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuCheckBox =
    inherit IFabMvuView
    inherit IFabCheckBox

module CheckBox =
    let IsCheckedWithEvent =
        MvuAttributes.defineBindableWithEvent "CheckBox_CheckedChanged" CheckBox.IsCheckedProperty (fun target -> (target :?> CheckBox).CheckedChanged)

[<AutoOpen>]
module CheckBoxBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a CheckBox widget with a state and listen for state changes</summary>
        /// <param name="isChecked">The state of the checkbox</param>
        /// <param name="onCheckedChanged">Message to dispatch</param>
        static member inline CheckBox<'msg>(isChecked: bool, onCheckedChanged: bool -> 'msg) =
            WidgetBuilder<'msg, IFabMvuCheckBox>(
                CheckBox.WidgetKey,
                CheckBox.IsCheckedWithEvent.WithValue(MvuValueEventData.create isChecked (fun (args: CheckedChangedEventArgs) -> onCheckedChanged args.Value))
            )

[<Extension>]
type CheckBoxModifiers =
    /// <summary>Link a ViewRef to access the direct CheckBox control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuCheckBox>, value: ViewRef<CheckBox>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
