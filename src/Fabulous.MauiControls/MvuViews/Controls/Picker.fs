namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuPicker =
    inherit IFabMvuView
    inherit IFabPicker

module Picker =
    let SelectedIndexWithEvent =
        MvuAttributes.defineBindableWithEvent "Picker_SelectedIndexChanged" Picker.SelectedIndexProperty (fun target ->
            (target :?> FabPicker).CustomSelectedIndexChanged)

[<AutoOpen>]
module PickerBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a Picker widget with a list of items, the selected index and listen to the selected index changes</summary>
        /// <param name="items">The items list</param>
        /// <param name="selectedIndex">The selected index</param>
        /// <param name="onSelectedIndexChanged">Message to dispatch</param>
        static member inline Picker<'msg>(items: string list, selectedIndex: int, onSelectedIndexChanged: int -> 'msg) =
            WidgetBuilder<'msg, IFabMvuPicker>(
                Picker.WidgetKey,
                Picker.ItemsSource.WithValue(Array.ofList items),
                Picker.SelectedIndexWithEvent.WithValue(
                    MvuValueEventData.create selectedIndex (fun (args: Fabulous.Maui.PositionChangedEventArgs) -> onSelectedIndexChanged args.CurrentPosition)
                )
            )

[<Extension>]
type PickerModifiers =
    /// <summary>Link a ViewRef to access the direct DatePicker control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuPicker>, value: ViewRef<Picker>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
