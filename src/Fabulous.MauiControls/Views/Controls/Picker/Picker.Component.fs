namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module PickerComponent =
    let SelectedIndexWithEvent =
        Attributes.Component.defineBindableWithEvent "PickerComponent_SelectedIndexChanged" Picker.SelectedIndexProperty (fun target ->
            (target :?> FabPicker).CustomSelectedIndexChanged)

[<AutoOpen>]
module PickerComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Picker widget with a list of items, the selected index and listen to the selected index changes</summary>
        /// <param name="items">The items list</param>
        /// <param name="selectedIndex">The selected index</param>
        /// <param name="onSelectedIndexChanged">Message to dispatch</param>
        static member inline Picker(items: string list, selectedIndex: int, onSelectedIndexChanged: int -> unit) =
            WidgetBuilder<unit, IFabPicker>(
                Picker.WidgetKey,
                Picker.ItemsSource.WithValue(Array.ofList items),
                PickerComponent.SelectedIndexWithEvent.WithValue(
                    ValueEventData.create selectedIndex (fun (args: FabPositionChangedEventArgs) -> onSelectedIndexChanged args.CurrentPosition)
                )
            )
