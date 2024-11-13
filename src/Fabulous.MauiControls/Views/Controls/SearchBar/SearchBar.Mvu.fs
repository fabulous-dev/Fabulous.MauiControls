namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module SearchBarMvu =
    let SearchButtonPressed =
        Attributes.Mvu.defineEventNoArg "SearchBar_SearchButtonPressedMsg" (fun target -> (target :?> SearchBar).SearchButtonPressed)

[<AutoOpen>]
module SearchBarMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a SearchBar widget with a text and listen for both text changes and search button presses</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        /// <param name="onSearchButtonPressed">Message to dispatch</param>
        static member inline SearchBar(text: string, onTextChanged: string -> 'msg, onSearchButtonPressed: 'msg) =
            WidgetBuilder<'msg, IFabSearchBar>(
                SearchBar.WidgetKey,
                InputViewMvu.TextWithEvent.WithValue(MsgValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue)),
                SearchBarMvu.SearchButtonPressed.WithValue(MsgValue(onSearchButtonPressed))
            )