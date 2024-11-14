namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module PickerMvu =
    let SelectedIndexWithEvent =
        Attributes.Mvu.defineBindableWithEvent "PickerMvu_SelectedIndexChanged" Picker.SelectedIndexProperty (fun target ->
            (target :?> FabPicker).CustomSelectedIndexChanged)

[<AutoOpen>]
module PickerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Picker widget with a list of items, the selected index and listen to the selected index changes</summary>
        /// <param name="items">The items list</param>
        /// <param name="selectedIndex">The selected index</param>
        /// <param name="onSelectedIndexChanged">Message to dispatch</param>
        static member inline Picker(items: string list, selectedIndex: int, onSelectedIndexChanged: int -> 'msg) =
            WidgetBuilder<'msg, IFabPicker>(
                Picker.WidgetKey,
                Picker.ItemsSource.WithValue(Array.ofList items),
                PickerMvu.SelectedIndexWithEvent.WithValue(
                    MsgValueEventData.create selectedIndex (fun (args: FabPositionChangedEventArgs) -> onSelectedIndexChanged args.CurrentPosition)
                )
            )
