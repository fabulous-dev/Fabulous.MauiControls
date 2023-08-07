namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls
open System.Runtime.CompilerServices

type IFabMultiPageOfPage =
    inherit IFabPage

module MultiPageOfPage =
    let Children =
        Attributes.defineListWidgetCollection "MultiPageOfPage" (fun target -> (target :?> MultiPage<Page>).Children)

    let CurrentPageChanged =
        Attributes.defineEventNoArg "MultiPageOfPage_CurrentPageChanged" (fun target -> (target :?> MultiPage<Page>).CurrentPageChanged)

[<Extension>]
type MultiPageOfPageModifiers =
    /// <summary>Listen for the CurrentPageChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onCurrentPageChanged(this: WidgetBuilder<'msg, #IFabMultiPageOfPage>, msg: 'msg) =
        this.AddScalar(MultiPageOfPage.CurrentPageChanged.WithValue(MsgValue(msg)))
