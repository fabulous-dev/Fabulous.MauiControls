namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.ApplicationModel

type IFabApplication =
    inherit IFabElement

type FabApplication() =
    inherit Application()

    let start = Event<EventHandler, EventArgs>()
    let sleep = Event<EventHandler, EventArgs>()
    let resume = Event<EventHandler, EventArgs>()

    [<CLIEvent>]
    member _.Start = start.Publish

    override this.OnStart() = start.Trigger(this, EventArgs())

    [<CLIEvent>]
    member _.Sleep = sleep.Publish

    override this.OnSleep() = sleep.Trigger(this, EventArgs())

    [<CLIEvent>]
    member _.Resume = resume.Publish

    override this.OnResume() = resume.Trigger(this, EventArgs())

module Application =
    let WidgetKey = Widgets.register<FabApplication>()

    let MainPage =
        Attributes.definePropertyWidget "Application_MainPage" (fun target -> (target :?> Application).MainPage :> obj) (fun target value ->
            (target :?> Application).MainPage <- value)

    let Resources =
        Attributes.defineSimpleScalarWithEquality<ResourceDictionary> "Application_Resources" (fun _ newValueOpt node ->
            let application = node.Target :?> Application

            let value =
                match newValueOpt with
                | ValueNone -> application.Resources
                | ValueSome v -> v

            application.Resources <- value)

    let UserAppTheme =
        Attributes.defineEnum<AppTheme> "Application_UserAppTheme" (fun _ newValueOpt node ->
            let application = node.Target :?> Application

            let value =
                match newValueOpt with
                | ValueNone -> AppTheme.Unspecified
                | ValueSome v -> v

            application.UserAppTheme <- value)

    let RequestedThemeChanged =
        Attributes.defineEvent<AppThemeChangedEventArgs> "Application_RequestedThemeChanged" (fun target -> (target :?> Application).RequestedThemeChanged)

    let ModalPopped =
        Attributes.defineEvent<ModalPoppedEventArgs> "Application_ModalPopped" (fun target -> (target :?> Application).ModalPopped)

    let ModalPopping =
        Attributes.defineEvent<ModalPoppingEventArgs> "Application_ModalPopping" (fun target -> (target :?> Application).ModalPopping)

    let ModalPushed =
        Attributes.defineEvent<ModalPushedEventArgs> "Application_ModalPushed" (fun target -> (target :?> Application).ModalPushed)

    let ModalPushing =
        Attributes.defineEvent<ModalPushingEventArgs> "Application_ModalPushing" (fun target -> (target :?> Application).ModalPushing)

    let Start =
        Attributes.defineEventNoArg "Application_Start" (fun target -> (target :?> FabApplication).Start)

    let Sleep =
        Attributes.defineEventNoArg "Application_Sleep" (fun target -> (target :?> FabApplication).Sleep)

    let Resume =
        Attributes.defineEventNoArg "Application_Resume" (fun target -> (target :?> FabApplication).Resume)

[<AutoOpen>]
module ApplicationBuilders =
    type Fabulous.Maui.View with

        static member inline Application<'msg, 'marker when 'marker :> IFabPage>(mainPage: WidgetBuilder<'msg, 'marker>) =
            WidgetHelpers.buildWidgets<'msg, IFabApplication> Application.WidgetKey [| Application.MainPage.WithValue(mainPage.Compile()) |]

[<Extension>]
type ApplicationModifiers =
    [<Extension>]
    static member inline userAppTheme(this: WidgetBuilder<'msg, #IFabApplication>, value: AppTheme) =
        this.AddScalar(Application.UserAppTheme.WithValue(value))

    [<Extension>]
    static member inline resources(this: WidgetBuilder<'msg, #IFabApplication>, value: ResourceDictionary) =
        this.AddScalar(Application.Resources.WithValue(value))

    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<'msg, #IFabApplication>, onRequestedThemeChanged: AppTheme -> 'msg) =
        this.AddScalar(Application.RequestedThemeChanged.WithValue(fun args -> onRequestedThemeChanged args.RequestedTheme |> box))

    [<Extension>]
    static member inline onModalPopped(this: WidgetBuilder<'msg, #IFabApplication>, onModalPopped: ModalPoppedEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPopped.WithValue(onModalPopped >> box))

    [<Extension>]
    static member inline onModalPopping(this: WidgetBuilder<'msg, #IFabApplication>, onModalPopping: ModalPoppingEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPopping.WithValue(onModalPopping >> box))

    [<Extension>]
    static member inline onModalPushed(this: WidgetBuilder<'msg, #IFabApplication>, onModalPushed: ModalPushedEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPushed.WithValue(onModalPushed >> box))

    [<Extension>]
    static member inline onModalPushing(this: WidgetBuilder<'msg, #IFabApplication>, onModalPushing: ModalPushingEventArgs -> 'msg) =
        this.AddScalar(Application.ModalPushing.WithValue(onModalPushing >> box))

    /// Dispatch a message when the application starts
    [<Extension>]
    static member inline onStart(this: WidgetBuilder<'msg, #IFabApplication>, onStart: 'msg) =
        this.AddScalar(Application.Start.WithValue(onStart))

    /// Dispatch a message when the application is paused by the OS
    [<Extension>]
    static member inline onSleep(this: WidgetBuilder<'msg, #IFabApplication>, onSleep: 'msg) =
        this.AddScalar(Application.Sleep.WithValue(onSleep))

    /// Dispatch a message when the application is resumed by the OS
    [<Extension>]
    static member inline onResume(this: WidgetBuilder<'msg, #IFabApplication>, onResume: 'msg) =
        this.AddScalar(Application.Resume.WithValue(onResume))

    /// Link a ViewRef to access the direct Application instance
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabApplication>, value: ViewRef<Application>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
