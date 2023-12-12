namespace Fabulous.Maui

open System
open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Microsoft.Maui.Controls
open System.Runtime.CompilerServices

type IFabMultiPageOfPage =
    inherit IFabPage

module MultiPageOfPage =
    let Children =
        Attributes.defineListWidgetCollection "MultiPageOfPage" (fun target -> (target :?> MultiPage<Page>).Children)

    [<Obsolete("Use CurrentPageWithEvent instead")>]
    let CurrentPageChanged =
        Attributes.defineEventNoArg "MultiPageOfPage_CurrentPageChanged" (fun target -> (target :?> MultiPage<Page>).CurrentPageChanged)

    let CurrentPageWithEvent =
        let name = "MultiPageOfPage_CurrentPageWithEvent"

        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun oldValueOpt (newValueOpt: ValueEventData<int, int> voption) node ->
                    let target = node.Target :?> MultiPage<Page>
                    let event = target.CurrentPageChanged

                    match newValueOpt with
                    | ValueNone ->
                        // The attribute is no longer applied, so we clean up the event
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> event.RemoveHandler(handler)

                        // Only clear the property if a value was set before
                        match oldValueOpt with
                        | ValueNone -> ()
                        | ValueSome _ -> target.CurrentPage <- target.Children.[0]

                    | ValueSome curr ->
                        // Clean up the old event handler if any
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> event.RemoveHandler(handler)

                        // Set the new value
                        target.CurrentPage <- target.Children.[curr.Value]

                        // Set the new event handler
                        let handler =
                            EventHandler(fun sender args ->
                                let multiPage = sender :?> MultiPage<Page>
                                let currentPageIndex = multiPage.Children.IndexOf(multiPage.CurrentPage)
                                let (MsgValue r) = curr.Event currentPageIndex
                                Dispatcher.dispatch node r)

                        node.SetHandler(name, ValueSome handler)
                        event.AddHandler(handler))
            )
            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }: SimpleScalarAttributeDefinition<ValueEventData<int, int>>

[<Extension>]
type MultiPageOfPageModifiers =
    /// <summary>Listen for the CurrentPageChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension; Obsolete("Use currentPage instead")>]
    static member inline onCurrentPageChanged(this: WidgetBuilder<'msg, #IFabMultiPageOfPage>, msg: 'msg) =
        this.AddScalar(MultiPageOfPage.CurrentPageChanged.WithValue(MsgValue(msg)))

    /// <summary>Set the current page and listen for changes</summary>
    /// <param name="this">Current widget</param>
    /// <param name="currentPage">The current page index</param>
    /// <param name="onCurrentPageChanged">Function to invoke</param>
    [<Extension>]
    static member inline currentPage(this: WidgetBuilder<'msg, #IFabMultiPageOfPage>, currentPage: int, onCurrentPageChanged: int -> 'msg) =
        this.AddScalar(MultiPageOfPage.CurrentPageWithEvent.WithValue(ValueEventData.create currentPage onCurrentPageChanged))
