namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuSearchBar =
    inherit IFabMvuInputView
    inherit IFabSearchBar

module SearchBar =
    let SearchButtonPressed =
        MvuAttributes.defineEventNoArg "SearchBar_SearchButtonPressed" (fun target -> (target :?> SearchBar).SearchButtonPressed)

[<AutoOpen>]
module SearchBarBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a SearchBar widget with a text and listen for both text changes and search button presses</summary>
        /// <param name="text">The text value</param>
        /// <param name="onTextChanged">Message to dispatch</param>
        /// <param name="onSearchButtonPressed">Message to dispatch</param>
        static member inline SearchBar<'msg>(text: string, onTextChanged: string -> 'msg, onSearchButtonPressed: 'msg) =
            WidgetBuilder<'msg, IFabMvuSearchBar>(
                SearchBar.WidgetKey,
                InputView.TextWithEvent.WithValue(MvuValueEventData.create text (fun (args: TextChangedEventArgs) -> onTextChanged args.NewTextValue)),
                SearchBar.SearchButtonPressed.WithValue(MsgValue(onSearchButtonPressed))
            )

[<Extension>]
type SearchBarModifiers =
    /// <summary>Link a ViewRef to access the direct SearchBar control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuSearchBar>, value: ViewRef<SearchBar>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
