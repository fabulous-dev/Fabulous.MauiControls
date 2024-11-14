namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module SearchBarComponent =
    let SearchButtonPressed =
        Attributes.Component.defineEventNoArg "SearchBarComponent_SearchButtonPressed" (fun target -> (target :?> SearchBar).SearchButtonPressed)

[<AutoOpen>]
module SearchBarComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a SearchBar widget with a text and listen for both text changes and search button presses</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        /// <param name="onSearchButtonPressed">Message to dispatch</param>
        static member inline SearchBar(text: string, onTextChanged: string -> unit, onSearchButtonPressed: unit -> unit) =
            WidgetBuilder<unit, IFabSearchBar>(
                SearchBar.WidgetKey,
                InputViewComponent.TextWithEvent.WithValue(ValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue)),
                SearchBarComponent.SearchButtonPressed.WithValue(onSearchButtonPressed)
            )
