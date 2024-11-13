namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module FlyoutPageMvu =
    let BackButtonPressed =
        Attributes.Mvu.defineEvent "FlyoutPageMvu_BackButtonPressed" (fun target -> (target :?> FlyoutPage).BackButtonPressed)

    let IsPresented =
        Attributes.Mvu.defineBindableWithEvent "FlyoutPageMvu_IsPresentedChanged" FlyoutPage.IsPresentedProperty (fun target ->
            (target :?> FabFlyoutPage).CustomIsPresentedChanged)

[<Extension>]
type FlyoutPageMvuModifiers =
    /// <summary>Set whether the flyout is presented, and listen for presentation state changes</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the flyout is presented</param>
    /// <param name="onChanged">Message to dispatch</param>
    [<Extension>]
    static member inline isPresented(this: WidgetBuilder<'msg, #IFabFlyoutPage>, value: bool, onChanged: bool -> 'msg) =
        this.AddScalar(FlyoutPageMvu.IsPresented.WithValue(MsgValueEventData.create value onChanged))

    /// <summary>Listen for back button pressed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onBackButtonPressed(this: WidgetBuilder<'msg, #IFabFlyoutPage>, fn: bool -> 'msg) =
        this.AddScalar(FlyoutPageMvu.BackButtonPressed.WithValue(fun args -> fn args.Handled))
