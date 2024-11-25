namespace HelloComponent

open System
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Hosting

open type Fabulous.Maui.View
open type Fabulous.Context

open Microsoft.Maui.Storage

type Settings() =
    inherit EnvironmentObject()

    let mutable usePaidMode = Preferences.Default.Get("UsePaidMode", false)

    member this.UsePaidMode
        with get () = usePaidMode
        and set v =
            usePaidMode <- v
            Preferences.Default.Set("UsePaidMode", v)
            this.NotifyChanged()

module EnvironmentKeys =

    let Settings = EnvironmentKey<Settings>("Settings")

module Child =
    let view name () =
        Component(name) {
            let! settings = Context.EnvironmentObject(EnvironmentKeys.Settings)

            VStack() {
                Label($"Paid mode = {settings.UsePaidMode}")
                Button("Toggle paid mode", fun () -> settings.UsePaidMode <- not settings.UsePaidMode)
            }
        }

module MvuChild =
    type Model = { Settings: Settings }

    type Msg = | TogglePaidMode

    let init (settings: Settings) = { Settings = settings }, Cmd.none

    let update msg model =
        match msg with
        | TogglePaidMode -> model, Cmd.ofEffect(fun _ -> model.Settings.UsePaidMode <- not model.Settings.UsePaidMode)

    let program = Program.statefulWithCmd init update

    let view name () =
        Component(name) {
            let! settings = EnvironmentObject(EnvironmentKeys.Settings)
            let! _ = Mvu(program, settings)

            VStack() {
                Label($"Paid mode = {settings.UsePaidMode}")
                Button("Toggle paid mode", TogglePaidMode)
            }
        }

module App =
    let view () =
        Component("root") {
            (Application() {
                Window(
                    ContentPage(
                        (VStack() {
                            Child.view "1" ()
                            Child.view "2" ()
                        })
                            .center()
                    )
                )
            })
                .environment(EnvironmentKeys.Settings, new Settings())
        }

    let createMauiApp () =
        MauiApp.CreateBuilder().UseFabulousApp(view).Build()
