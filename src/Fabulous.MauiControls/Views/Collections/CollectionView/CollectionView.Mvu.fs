namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module CollectionViewMvu =
    let SelectionChanged =
        Attributes.Mvu.defineEvent<SelectionChangedEventArgs> "CollectionViewMvu_SelectionChanged" (fun target -> (target :?> CollectionView).SelectionChanged)

[<Extension>]
type CollectionViewMvuModifiers =
    /// <summary>Listen for the SelectionChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSelectionChanged(this: WidgetBuilder<'msg, #IFabCollectionView>, fn: SelectionChangedEventArgs -> 'msg) =
        this.AddScalar(CollectionViewMvu.SelectionChanged.WithValue(fun args -> fn args |> box))
