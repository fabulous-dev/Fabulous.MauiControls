namespace Fabulous.Maui

open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Microsoft.Maui.Controls
open System.Runtime.CompilerServices

module MultiPageOfPageComponent =
    let CurrentPageWithEvent =
        let name = "MultiPageOfPageComponent_CurrentPageWithEvent"

        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun oldValueOpt (newValueOpt: ValueEventData<int, int> voption) node ->
                    let target = node.Target :?> MultiPage<Page>

                    match newValueOpt with
                    | ValueNone ->
                        // The attribute is no longer applied, so we clean up the event
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> handler.Dispose()

                        // Only clear the property if a value was set before
                        match oldValueOpt with
                        | ValueNone -> ()
                        | ValueSome _ -> target.CurrentPage <- target.Children.[0]

                    | ValueSome curr ->
                        // Clean up the old event handler if any
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> handler.Dispose()

                        // Set the new value
                        target.CurrentPage <- target.Children.[curr.Value]

                        // Set the new event handler
                        let handler =
                            target.CurrentPageChanged.Subscribe(fun _args ->
                                let currentPageIndex = target.Children.IndexOf(target.CurrentPage)
                                curr.Event currentPageIndex)

                        node.SetHandler(name, handler))
            )
            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }: SimpleScalarAttributeDefinition<ValueEventData<int, int>>

[<Extension>]
type MultiPageOfPageComponentModifiers =
    /// <summary>Set the current page and listen for changes</summary>
    /// <param name="this">Current widget</param>
    /// <param name="currentPage">The current page index</param>
    /// <param name="onCurrentPageChanged">Function to invoke</param>
    [<Extension>]
    static member inline currentPage(this: WidgetBuilder<unit, #IFabMultiPageOfPage>, currentPage: int, onCurrentPageChanged: int -> unit) =
        this.AddScalar(MultiPageOfPageComponent.CurrentPageWithEvent.WithValue(ValueEventData.create currentPage onCurrentPageChanged))
