namespace Fabulous.Maui

open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

module RefreshViewMvu =
    let Refreshing =
        Attributes.Mvu.defineEventNoArg "RefreshViewMvu_Refreshing" (fun target -> (target :?> RefreshView).Refreshing)

[<AutoOpen>]
module RefreshViewMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a RefreshView widget with content</summary>
        /// <param name="isRefreshing">The refresh state</param>
        /// <param name="onRefreshing">Message to dispatch when refresh state changes</param>
        /// <param name="content">The content widget</param>
        static member inline RefreshView(isRefreshing: bool, onRefreshing: 'msg, content: WidgetBuilder<'msg, #IFabView>) =
            WidgetBuilder<'msg, IFabRefreshView>(
                RefreshView.WidgetKey,
                AttributesBundle(
                    StackList.two(RefreshView.IsRefreshing.WithValue(isRefreshing), RefreshViewMvu.Refreshing.WithValue(MsgValue(onRefreshing))),
                    ValueSome [| ContentView.Content.WithValue(content.Compile()) |],
                    ValueNone
                )
            )
