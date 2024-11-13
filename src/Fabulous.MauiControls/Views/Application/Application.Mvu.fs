namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.ApplicationModel

module ApplicationMvu =
    let ModalPopped =
        Attributes.Mvu.defineEvent<ModalPoppedEventArgs> "ApplicationMvu_ModalPopped" (fun target -> (target :?> Application).ModalPopped)

    let ModalPopping =
        Attributes.Mvu.defineEvent<ModalPoppingEventArgs> "ApplicationMvu_ModalPopping" (fun target -> (target :?> Application).ModalPopping)

    let ModalPushed =
        Attributes.Mvu.defineEvent<ModalPushedEventArgs> "ApplicationMvu_ModalPushed" (fun target -> (target :?> Application).ModalPushed)

    let ModalPushing =
        Attributes.Mvu.defineEvent<ModalPushingEventArgs> "ApplicationMvu_ModalPushing" (fun target -> (target :?> Application).ModalPushing)

    let RequestedThemeChanged =
        Attributes.Mvu.defineEvent<AppThemeChangedEventArgs> "ApplicationMvu_RequestedThemeChanged" (fun target -> (target :?> Application).RequestedThemeChanged)

    let Resume =
        Attributes.Mvu.defineEventNoArg "ApplicationMvu_Resume" (fun target -> (target :?> FabApplication).Resume)

    let Sleep =
        Attributes.Mvu.defineEventNoArg "ApplicationMvu_Sleep" (fun target -> (target :?> FabApplication).Sleep)

    let Start =
        Attributes.Mvu.defineEventNoArg "ApplicationMvu_Start" (fun target -> (target :?> FabApplication).Start)

    let AppLinkRequestReceived =
        Attributes.Mvu.defineEvent "ApplicationMvu_AppLinkRequestReceived" (fun target -> (target :?> FabApplication).AppLinkRequestReceived)

[<Extension>]
type ApplicationMvuModifiers =
    /// <summary>Listen for the ModalPopped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopped(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppedEventArgs -> 'msg) =
        this.AddScalar(ApplicationMvu.ModalPopped.WithValue(fn))

    /// <summary>Listen for the ModalPopping event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopping(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppingEventArgs -> 'msg) =
        this.AddScalar(ApplicationMvu.ModalPopping.WithValue(fn))

    /// <summary>Listen for the ModalPushed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushed(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushedEventArgs -> 'msg) =
        this.AddScalar(ApplicationMvu.ModalPushed.WithValue(fn))

    /// <summary>Listen for the ModalPushing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushing(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushingEventArgs -> 'msg) =
        this.AddScalar(ApplicationMvu.ModalPushing.WithValue(fn))

    /// <summary>Listen for the Resume event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onResume(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(ApplicationMvu.Resume.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Start event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onStart(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(ApplicationMvu.Start.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Sleep event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onSleep(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(ApplicationMvu.Sleep.WithValue(MsgValue(msg)))

    /// <summary>Listen for the RequestedThemeChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: AppTheme -> 'msg) =
        this.AddScalar(ApplicationMvu.RequestedThemeChanged.WithValue(fun args -> fn args.RequestedTheme |> box))

    /// <summary>Listen for the AppLinkRequestReceived event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onAppLinkRequestReceived(this: WidgetBuilder<'msg, #IFabApplication>, fn: Uri -> 'msg) =
        this.AddScalar(ApplicationMvu.AppLinkRequestReceived.WithValue(fun args -> fn args |> box))
