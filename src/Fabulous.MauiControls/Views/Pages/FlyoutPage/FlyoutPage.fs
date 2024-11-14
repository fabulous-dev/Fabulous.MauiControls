namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabFlyoutPage =
    inherit IFabPage

/// FlyoutPage doesn't say if the Flyout is visible or not on IsPresentedChanged, so we implement it
type FabFlyoutPage() as this =
    inherit FlyoutPage()

    let isPresentedChanged = Event<EventHandler<bool>, bool>()

    do this.IsPresentedChanged.Add(this.OnIsPresentedChanged)

    [<CLIEvent>]
    member _.CustomIsPresentedChanged = isPresentedChanged.Publish

    member _.OnIsPresentedChanged(_) =
        isPresentedChanged.Trigger(this, this.IsPresented)

module FlyoutPage =
    let WidgetKey = Widgets.register<FabFlyoutPage>()

    let Detail =
        Attributes.definePropertyWidget "FlyoutPage_Detail" (fun target -> (target :?> FlyoutPage).Detail :> obj) (fun target value ->
            (target :?> FlyoutPage).Detail <- value)

    let Flyout =
        Attributes.definePropertyWidget "FlyoutPage_Flyout" (fun target -> (target :?> FlyoutPage).Flyout :> obj) (fun target value ->
            (target :?> FlyoutPage).Flyout <- value)

    let FlyoutLayoutBehavior =
        Attributes.defineBindableEnum<FlyoutLayoutBehavior> FlyoutPage.FlyoutLayoutBehaviorProperty

    let IsGestureEnabled =
        Attributes.defineBindableBool FlyoutPage.IsGestureEnabledProperty

[<AutoOpen>]
module FlyoutPageBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a flyout page with a flyout and a detail widgets</summary>
        /// <param name="flyout">The flyout widget</param>
        /// <param name="detail">The detail widget</param>
        static member inline FlyoutPage(flyout: WidgetBuilder<'msg, #IFabPage>, detail: WidgetBuilder<'msg, #IFabPage>) =
            WidgetHelpers.buildWidgets<'msg, IFabFlyoutPage>
                FlyoutPage.WidgetKey
                [| FlyoutPage.Flyout.WithValue(flyout.Compile())
                   FlyoutPage.Detail.WithValue(detail.Compile()) |]

[<Extension>]
type FlyoutPageModifiers =
    /// <summary>Set the flyout layout behavior</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The flyout layout behavior</param>
    [<Extension>]
    static member inline flyoutLayoutBehavior(this: WidgetBuilder<'msg, #IFabFlyoutPage>, value: FlyoutLayoutBehavior) =
        this.AddScalar(FlyoutPage.FlyoutLayoutBehavior.WithValue(value))

    /// <summary>Set whether gesture is enabled to open the flyout</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether gesture is enabled</param>
    [<Extension>]
    static member inline isGestureEnabled(this: WidgetBuilder<'msg, #IFabFlyoutPage>, value: bool) =
        this.AddScalar(FlyoutPage.IsGestureEnabled.WithValue(value))

    /// <summary>Link a ViewRef to access the direct FlyoutPage control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabFlyoutPage>, value: ViewRef<FlyoutPage>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
