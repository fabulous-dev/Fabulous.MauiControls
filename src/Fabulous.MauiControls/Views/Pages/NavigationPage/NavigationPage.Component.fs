namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous

module NavigationPageComponent =
    let WidgetKey = Widgets.register<FabNavigationPage>()

    let BackButtonPressed =
        Attributes.Component.defineEventNoArg "NavigationPageComponent_BackButtonPressed" (fun target -> (target :?> FabNavigationPage).BackButtonPressed)

    let BackNavigated =
        Attributes.Component.defineEventNoArg "NavigationPageComponent_BackNavigated" (fun target -> (target :?> FabNavigationPage).BackNavigated)

[<Extension>]
type NavigationPageComponentModifiers =
    /// <summary>Listen to the user pressing the system back button. Doesn't support the iOS back button</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    /// <remarks>Setting this modifier will prevent the default behavior of the system back button. It's up to you to update the navigation stack.</remarks>
    [<Extension>]
    static member inline onBackButtonPressed(this: WidgetBuilder<unit, #IFabNavigationPage>, fn: unit -> unit) =
        this.AddScalar(NavigationPageComponent.BackButtonPressed.WithValue(fn))

    /// <summary>Listen to the user back navigating</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onBackNavigated(this: WidgetBuilder<unit, #IFabNavigationPage>, fn: unit -> unit) =
        this.AddScalar(NavigationPageComponent.BackNavigated.WithValue(fn))
