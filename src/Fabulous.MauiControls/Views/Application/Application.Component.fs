namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.ApplicationModel

module ApplicationComponent =
    let ModalPopped =
        Attributes.Component.defineEvent<ModalPoppedEventArgs> "ApplicationComponent_ModalPopped" (fun target -> (target :?> Application).ModalPopped)

    let ModalPopping =
        Attributes.Component.defineEvent<ModalPoppingEventArgs> "ApplicationComponent_ModalPopping" (fun target -> (target :?> Application).ModalPopping)

    let ModalPushed =
        Attributes.Component.defineEvent<ModalPushedEventArgs> "ApplicationComponent_ModalPushed" (fun target -> (target :?> Application).ModalPushed)

    let ModalPushing =
        Attributes.Component.defineEvent<ModalPushingEventArgs> "ApplicationComponent_ModalPushing" (fun target -> (target :?> Application).ModalPushing)

    let RequestedThemeChanged =
        Attributes.Component.defineEvent<AppThemeChangedEventArgs> "ApplicationComponent_RequestedThemeChanged" (fun target ->
            (target :?> Application).RequestedThemeChanged)

    let Resume =
        Attributes.Component.defineEventNoArg "ApplicationComponent_Resume" (fun target -> (target :?> FabApplication).Resume)

    let Sleep =
        Attributes.Component.defineEventNoArg "ApplicationComponent_Sleep" (fun target -> (target :?> FabApplication).Sleep)

    let Start =
        Attributes.Component.defineEventNoArg "ApplicationComponent_Start" (fun target -> (target :?> FabApplication).Start)

    let AppLinkRequestReceived =
        Attributes.Component.defineEvent "ApplicationComponent_AppLinkRequestReceived" (fun target -> (target :?> FabApplication).AppLinkRequestReceived)

[<Extension>]
type ApplicationComponentModifiers =
    /// <summary>Listen for the ModalPopped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopped(this: WidgetBuilder<unit, #IFabApplication>, fn: ModalPoppedEventArgs -> unit) =
        this.AddScalar(ApplicationComponent.ModalPopped.WithValue(fn))

    /// <summary>Listen for the ModalPopping event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopping(this: WidgetBuilder<unit, #IFabApplication>, fn: ModalPoppingEventArgs -> unit) =
        this.AddScalar(ApplicationComponent.ModalPopping.WithValue(fn))

    /// <summary>Listen for the ModalPushed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushed(this: WidgetBuilder<unit, #IFabApplication>, fn: ModalPushedEventArgs -> unit) =
        this.AddScalar(ApplicationComponent.ModalPushed.WithValue(fn))

    /// <summary>Listen for the ModalPushing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushing(this: WidgetBuilder<unit, #IFabApplication>, fn: ModalPushingEventArgs -> unit) =
        this.AddScalar(ApplicationComponent.ModalPushing.WithValue(fn))

    /// <summary>Listen for the Resume event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onResume(this: WidgetBuilder<unit, #IFabApplication>, fn: unit -> unit) =
        this.AddScalar(ApplicationComponent.Resume.WithValue(fn))

    /// <summary>Listen for the Start event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onStart(this: WidgetBuilder<unit, #IFabApplication>, fn: unit -> unit) =
        this.AddScalar(ApplicationComponent.Start.WithValue(fn))

    /// <summary>Listen for the Sleep event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onSleep(this: WidgetBuilder<unit, #IFabApplication>, fn: unit -> unit) =
        this.AddScalar(ApplicationComponent.Sleep.WithValue(fn))

    /// <summary>Listen for the RequestedThemeChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<unit, #IFabApplication>, fn: AppTheme -> unit) =
        this.AddScalar(ApplicationComponent.RequestedThemeChanged.WithValue(fun args -> fn args.RequestedTheme))

    /// <summary>Listen for the AppLinkRequestReceived event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onAppLinkRequestReceived(this: WidgetBuilder<unit, #IFabApplication>, fn: Uri -> unit) =
        this.AddScalar(ApplicationComponent.AppLinkRequestReceived.WithValue(fn))
