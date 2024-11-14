namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module FlyoutPageComponent =
    let BackButtonPressed =
        Attributes.Component.defineEvent "FlyoutPageComponent_BackButtonPressed" (fun target -> (target :?> FlyoutPage).BackButtonPressed)

    let IsPresented =
        Attributes.Component.defineBindableWithEvent "FlyoutPageComponent_IsPresentedChanged" FlyoutPage.IsPresentedProperty (fun target ->
            (target :?> FabFlyoutPage).CustomIsPresentedChanged)

[<Extension>]
type FlyoutPageComponentModifiers =
    /// <summary>Set whether the flyout is presented, and listen for presentation state changes</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the flyout is presented</param>
    /// <param name="onChanged">Message to dispatch</param>
    [<Extension>]
    static member inline isPresented(this: WidgetBuilder<unit, #IFabFlyoutPage>, value: bool, onChanged: bool -> unit) =
        this.AddScalar(FlyoutPageComponent.IsPresented.WithValue(ValueEventData.create value onChanged))

    /// <summary>Listen for back button pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onBackButtonPressed(this: WidgetBuilder<unit, #IFabFlyoutPage>, fn: bool -> unit) =
        this.AddScalar(FlyoutPageComponent.BackButtonPressed.WithValue(fun args -> fn args.Handled))
