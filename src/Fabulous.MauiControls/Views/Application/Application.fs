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

        /// <summary>Create an Application widget with a list of windows</summary>
        static member inline Application<'msg, 'itemMarker when 'msg: equality and 'itemMarker :> IFabWindow>() =
            CollectionBuilder<'msg, IFabApplication, 'itemMarker>(Application.WidgetKey, Application.Windows)

[<Extension>]
type ApplicationModifiers =
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
