namespace Fabulous.Maui

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Fabulous.StackAllocatedCollections
open Microsoft.Maui.Controls
open Microsoft.Maui.ApplicationModel

type IFabApplication =
    inherit IFabElement

type FabApplication() =
    inherit Application()

    let start = Event<EventHandler, EventArgs>()
    let sleep = Event<EventHandler, EventArgs>()
    let resume = Event<EventHandler, EventArgs>()
    let appLinkRequestReceived = Event<EventHandler<Uri>, Uri>()

    let windows = List<Window>()

    member this.Windows = windows
    member this.EditableWindows = windows

    [<CLIEvent>]
    member _.Start = start.Publish

    override this.OnStart() = start.Trigger(this, EventArgs())

    [<CLIEvent>]
    member _.Sleep = sleep.Publish

    override this.OnSleep() = sleep.Trigger(this, EventArgs())

    [<CLIEvent>]
    member _.Resume = resume.Publish

    override this.OnResume() = resume.Trigger(this, EventArgs())

    [<CLIEvent>]
    member _.AppLinkRequestReceived = appLinkRequestReceived.Publish

    override this.OnAppLinkRequestReceived(uri) =
        appLinkRequestReceived.Trigger(this, uri)

    override this.CreateWindow(activationState) =
        if windows.Count > 0 then
            windows[0]
        else
            base.CreateWindow(activationState)

    override this.OpenWindow(window) =
        windows.Add(window)
        base.OpenWindow(window)

    override this.CloseWindow(window) =
        windows.Remove(window) |> ignore
        base.CloseWindow(window)

module Application =
    let WidgetKey = Widgets.register<FabApplication>()

    let MainPage =
        Attributes.definePropertyWidget "Application_MainPage" (fun target -> (target :?> Application).MainPage :> obj) (fun target value ->
            (target :?> Application).MainPage <- value)

    let ModalPoppedMsg =
        Attributes.defineEvent<ModalPoppedEventArgs> "Application_ModalPoppedMsg" (fun target -> (target :?> Application).ModalPopped)

    let ModalPoppedFn =
        Attributes.defineEventNoDispatch<ModalPoppedEventArgs> "Application_ModalPoppedFn" (fun target -> (target :?> Application).ModalPopped)

    let ModalPoppingMsg =
        Attributes.defineEvent<ModalPoppingEventArgs> "Application_ModalPoppingMsg" (fun target -> (target :?> Application).ModalPopping)

    let ModalPoppingFn =
        Attributes.defineEventNoDispatch<ModalPoppingEventArgs> "Application_ModalPoppingFn" (fun target -> (target :?> Application).ModalPopping)

    let ModalPushedMsg =
        Attributes.defineEvent<ModalPushedEventArgs> "Application_ModalPushedMsg" (fun target -> (target :?> Application).ModalPushed)

    let ModalPushedFn =
        Attributes.defineEventNoDispatch<ModalPushedEventArgs> "Application_ModalPushedFn" (fun target -> (target :?> Application).ModalPushed)

    let ModalPushingMsg =
        Attributes.defineEvent<ModalPushingEventArgs> "Application_ModalPushingMsg" (fun target -> (target :?> Application).ModalPushing)

    let ModalPushingFn =
        Attributes.defineEventNoDispatch<ModalPushingEventArgs> "Application_ModalPushingFn" (fun target -> (target :?> Application).ModalPushing)

    let RequestedThemeChangedMsg =
        Attributes.defineEvent<AppThemeChangedEventArgs> "Application_RequestedThemeChangedMsg" (fun target -> (target :?> Application).RequestedThemeChanged)

    let RequestedThemeChangedFn =
        Attributes.defineEventNoDispatch<AppThemeChangedEventArgs> "Application_RequestedThemeChangedFn" (fun target ->
            (target :?> Application).RequestedThemeChanged)

    let ResumeMsg =
        Attributes.defineEventNoArg "Application_ResumeMsg" (fun target -> (target :?> FabApplication).Resume)

    let ResumeFn =
        Attributes.defineEventNoArgNoDispatch "Application_ResumeFn" (fun target -> (target :?> FabApplication).Resume)

    let SleepMsg =
        Attributes.defineEventNoArg "Application_SleepMsg" (fun target -> (target :?> FabApplication).Sleep)

    let SleepFn =
        Attributes.defineEventNoArgNoDispatch "Application_SleepFn" (fun target -> (target :?> FabApplication).Sleep)

    let StartMsg =
        Attributes.defineEventNoArg "Application_StartMsg" (fun target -> (target :?> FabApplication).Start)

    let StartFn =
        Attributes.defineEventNoArgNoDispatch "Application_StartFn" (fun target -> (target :?> FabApplication).Start)

    let AppLinkRequestReceivedMsg =
        Attributes.defineEvent "Application_AppLinkRequestReceivedMsg" (fun target -> (target :?> FabApplication).AppLinkRequestReceived)

    let AppLinkRequestReceivedFn =
        Attributes.defineEventNoDispatch "Application_AppLinkRequestReceivedFn" (fun target -> (target :?> FabApplication).AppLinkRequestReceived)

    let UserAppTheme =
        Attributes.defineEnum<AppTheme> "Application_UserAppTheme" (fun _ newValueOpt node ->
            let application = node.Target :?> Application

            let value =
                match newValueOpt with
                | ValueNone -> AppTheme.Unspecified
                | ValueSome v -> v

            application.UserAppTheme <- value)

    let Windows =
        Attributes.defineListWidgetCollection "Application_Windows" (fun target -> (target :?> FabApplication).EditableWindows)

[<AutoOpen>]
module ApplicationBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create an Application widget with a main page</summary>
        /// <param name="mainPage">The main page widget</param>
        static member inline Application(mainPage: WidgetBuilder<'msg, #IFabPage>) =
            WidgetHelpers.buildWidgets<'msg, IFabApplication> Application.WidgetKey [| Application.MainPage.WithValue(mainPage.Compile()) |]

        /// <summary>Create an Application widget with a list of windows</summary>
        static member inline Application<'msg, 'itemMarker when 'msg: equality and 'itemMarker :> IFabWindow>() =
            CollectionBuilder<'msg, IFabApplication, 'itemMarker>(Application.WidgetKey, Application.Windows)

[<Extension>]
type ApplicationModifiers =
    /// <summary>Listen for the ModalPopped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopped(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppedEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPoppedMsg.WithValue(fn))

    /// <summary>Listen for the ModalPopped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopped(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppedEventArgs -> unit) =
        this.AddScalar(Application.ModalPoppedFn.WithValue(fn))

    /// <summary>Listen for the ModalPopping event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopping(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppingEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPoppingMsg.WithValue(fn))

    /// <summary>Listen for the ModalPopping event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopping(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppingEventArgs -> unit) =
        this.AddScalar(Application.ModalPoppingFn.WithValue(fn))

    /// <summary>Listen for the ModalPushed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushed(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushedEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPushedMsg.WithValue(fn))

    /// <summary>Listen for the ModalPushed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushed(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushedEventArgs -> unit) =
        this.AddScalar(Application.ModalPushedFn.WithValue(fn))

    /// <summary>Listen for the ModalPushing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushing(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushingEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPushingMsg.WithValue(fn))

    /// <summary>Listen for the ModalPushing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushing(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushingEventArgs -> unit) =
        this.AddScalar(Application.ModalPushingFn.WithValue(fn))

    /// <summary>Listen for the Resume event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onResume(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(Application.ResumeMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Resume event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onResume(this: WidgetBuilder<'msg, #IFabApplication>, fn: unit -> unit) =
        this.AddScalar(Application.ResumeFn.WithValue(fn))

    /// <summary>Listen for the Start event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onStart(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(Application.StartMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Start event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onStart(this: WidgetBuilder<'msg, #IFabApplication>, fn: unit -> unit) =
        this.AddScalar(Application.StartFn.WithValue(fn))

    /// <summary>Listen for the Sleep event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onSleep(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(Application.SleepMsg.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Sleep event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onSleep(this: WidgetBuilder<'msg, #IFabApplication>, fn: unit -> unit) =
        this.AddScalar(Application.SleepFn.WithValue(fn))

    /// <summary>Listen for the RequestedThemeChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: AppTheme -> 'msg) =
        this.AddScalar(Application.RequestedThemeChangedMsg.WithValue(fun args -> fn args.RequestedTheme |> box))

    /// <summary>Listen for the RequestedThemeChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: AppTheme -> unit) =
        this.AddScalar(Application.RequestedThemeChangedFn.WithValue(fun args -> fn args.RequestedTheme))

    /// <summary>Listen for the AppLinkRequestReceived event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onAppLinkRequestReceived(this: WidgetBuilder<'msg, #IFabApplication>, fn: Uri -> 'msg) =
        this.AddScalar(Application.AppLinkRequestReceivedMsg.WithValue(fun args -> fn args |> box))

    /// <summary>Listen for the AppLinkRequestReceived event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onAppLinkRequestReceived(this: WidgetBuilder<'msg, #IFabApplication>, fn: Uri -> unit) =
        this.AddScalar(Application.AppLinkRequestReceivedFn.WithValue(fun args -> fn args))

    /// <summary>Set the user app theme</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The user app theme</param>
    [<Extension>]
    static member inline userAppTheme(this: WidgetBuilder<'msg, #IFabApplication>, value: AppTheme) =
        this.AddScalar(Application.UserAppTheme.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Application control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabApplication>, value: ViewRef<Application>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type ApplicationYieldExtensions =
    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'marker :> IFabApplication and 'itemType :> IFabWindow>
        (
            _: CollectionBuilder<'msg, 'marker, IFabWindow>,
            x: WidgetBuilder<'msg, 'itemType>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
