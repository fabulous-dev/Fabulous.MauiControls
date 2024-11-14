namespace Fabulous.Maui

open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

module RefreshViewComponent =
    let Refreshing =
        Attributes.Component.defineEventNoArg "RefreshViewComponent_Refreshing" (fun target -> (target :?> RefreshView).Refreshing)

[<AutoOpen>]
module RefreshViewComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a RefreshView widget with content</summary>
        /// <param name="isRefreshing">The refresh state</param>
        /// <param name="onRefreshing">Message to dispatch when refresh state changes</param>
        /// <param name="content">The content widget</param>
        static member inline RefreshView(isRefreshing: bool, onRefreshing: unit -> unit, content: WidgetBuilder<unit, #IFabView>) =
            WidgetBuilder<unit, IFabRefreshView>(
                RefreshView.WidgetKey,
                AttributesBundle(
                    StackList.two(RefreshView.IsRefreshing.WithValue(isRefreshing), RefreshViewComponent.Refreshing.WithValue(onRefreshing)),
                    ValueSome [| ContentView.Content.WithValue(content.Compile()) |],
                    ValueNone
                )
            )
