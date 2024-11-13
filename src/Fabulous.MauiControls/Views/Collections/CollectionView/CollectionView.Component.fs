namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module CollectionViewComponent =
    let SelectionChanged =
        Attributes.Component.defineEvent<SelectionChangedEventArgs> "CollectionViewComponent_SelectionChanged" (fun target ->
            (target :?> CollectionView).SelectionChanged)

[<Extension>]
type CollectionViewComponentModifiers =
    /// <summary>Listen for the SelectionChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSelectionChanged(this: WidgetBuilder<unit, #IFabCollectionView>, fn: SelectionChangedEventArgs -> unit) =
        this.AddScalar(CollectionViewComponent.SelectionChanged.WithValue(fn))
