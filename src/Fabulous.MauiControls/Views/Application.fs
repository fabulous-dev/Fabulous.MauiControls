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
        windows[0]
        
    override this.OpenWindow(window) =
        windows.Add(window)
        
    override this.CloseWindow(window) =
        windows.Remove(window) |> ignore

module Application =
    let WidgetKey = Widgets.register<FabApplication>()

    let MainPage =
        Attributes.definePropertyWidget "Application_MainPage" (fun target -> (target :?> Application).MainPage :> obj) (fun target value ->
            (target :?> Application).MainPage <- value)

    let ModalPopped =
        Attributes.defineEvent<ModalPoppedEventArgs> "Application_ModalPopped" (fun target -> (target :?> Application).ModalPopped)

    let ModalPopping =
        Attributes.defineEvent<ModalPoppingEventArgs> "Application_ModalPopping" (fun target -> (target :?> Application).ModalPopping)

    let ModalPushed =
        Attributes.defineEvent<ModalPushedEventArgs> "Application_ModalPushed" (fun target -> (target :?> Application).ModalPushed)

    let ModalPushing =
        Attributes.defineEvent<ModalPushingEventArgs> "Application_ModalPushing" (fun target -> (target :?> Application).ModalPushing)

    let RequestedThemeChanged =
        Attributes.defineEvent<AppThemeChangedEventArgs> "Application_RequestedThemeChanged" (fun target -> (target :?> Application).RequestedThemeChanged)

    let Resume =
        Attributes.defineEventNoArg "Application_Resume" (fun target -> (target :?> FabApplication).Resume)

    let Sleep =
        Attributes.defineEventNoArg "Application_Sleep" (fun target -> (target :?> FabApplication).Sleep)

    let Start =
        Attributes.defineEventNoArg "Application_Start" (fun target -> (target :?> FabApplication).Start)

    let AppLinkRequestReceived =
        Attributes.defineEvent "Application_AppLinkRequestReceived" (fun target -> (target :?> FabApplication).AppLinkRequestReceived)

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
        
        // static member inline Application<'msg, 'childMarker when 'childMarker :> IFabPage>() =
        //     SingleChildBuilder<'msg, IFabApplication, 'childMarker>(Application.WidgetKey, Application.MainPage)

        static member inline Application<'msg, 'itemMarker when 'itemMarker :> IFabWindow>() =
            CollectionBuilder<'msg, IFabApplication, 'itemMarker>(Application.WidgetKey, Application.Windows)

[<Extension>]
type ApplicationModifiers =
    /// <summary>Listen for the ModalPopped event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopped(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppedEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPopped.WithValue(fn))

    /// <summary>Listen for the ModalPopping event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPopping(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPoppingEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPopping.WithValue(fn))

    /// <summary>Listen for the ModalPushed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushed(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushedEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPushed.WithValue(fn))

    /// <summary>Listen for the ModalPushing event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onModalPushing(this: WidgetBuilder<'msg, #IFabApplication>, fn: ModalPushingEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPushing.WithValue(fn))

    /// <summary>Listen for the Resume event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onResume(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(Application.Resume.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Start event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onStart(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(Application.Start.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Sleep event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onSleep(this: WidgetBuilder<'msg, #IFabApplication>, msg: 'msg) =
        this.AddScalar(Application.Sleep.WithValue(MsgValue(msg)))

    /// <summary>Listen for the RequestedThemeChanged event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: AppTheme -> 'msg) =
        this.AddScalar(Application.RequestedThemeChanged.WithValue(fun args -> fn args.RequestedTheme |> box))

    /// <summary>Listen for the AppLinkRequestReceived event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onAppLinkRequestReceived(this: WidgetBuilder<'msg, #IFabApplication>, fn: Uri -> 'msg) =
        this.AddScalar(Application.AppLinkRequestReceived.WithValue(fun args -> fn args |> box))

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
    static member inline Yield<'msg, 'marker, 'itemType when 'marker :> IFabApplication and 'itemType :> IFabWindow>
        (
            _: CollectionBuilder<'msg, 'marker, IFabWindow>,
            x: WidgetBuilder<'msg, 'itemType>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }