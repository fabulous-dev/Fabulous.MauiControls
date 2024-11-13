namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous

module NavigationPageMvu =
    let BackButtonPressed =
        Attributes.Mvu.defineEventNoArg "NavigationPageMvu_BackButtonPressed" (fun target -> (target :?> FabNavigationPage).BackButtonPressed)

    let BackNavigated =
        Attributes.Mvu.defineEventNoArg "NavigationPageMvu_BackNavigated" (fun target -> (target :?> FabNavigationPage).BackNavigated)

[<Extension>]
type NavigationPageMvuModifiers =
    /// <summary>Listen to the user pressing the system back button. Doesn't support the iOS back button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Msg to dispatch</param>
    /// <remarks>Setting this modifier will prevent the default behavior of the system back button. It's up to you to update the navigation stack.</remarks>
    [<Extension>]
    static member inline onBackButtonPressed(this: WidgetBuilder<'msg, #IFabNavigationPage>, msg: 'msg) =
        this.AddScalar(NavigationPageMvu.BackButtonPressed.WithValue(MsgValue(msg)))

    /// <summary>Listen to the user back navigating</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onBackNavigated(this: WidgetBuilder<'msg, #IFabNavigationPage>, msg: 'msg) =
        this.AddScalar(NavigationPageMvu.BackNavigated.WithValue(MsgValue(msg)))