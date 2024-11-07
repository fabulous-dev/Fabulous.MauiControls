namespace SimpleCounter

open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Hosting

open type Fabulous.Maui.View

module EnvironmentKeys =
    let Count = EnvironmentKey<int>("Count")

module Child =
    let view name =
        Component(name) {
            let! count = Context.Environment(EnvironmentKeys.Count)
            Label($"[{name}] Count: {count}")
        }

module App =
    type Msg =
        | Increment
        | Decrement
        | CountChanged of int
        
    let program =
        Program.stateful
            (fun () -> 0)
            (fun msg model ->
                match msg with
                | Increment -> model + 1
                | Decrement -> model - 1
                | CountChanged count -> count)
    
    let view () =
        Component("root") {
            let! theme = Context.Environment(EnvironmentKeys.Theme)
            let! model = Context.Mvu(program)

            Application(
                ContentPage(
                    (VStack() {
                        Label("Theme is: " + theme.ToString()).centerTextHorizontal()
                        Label($"%d{model}").centerTextHorizontal()
                        Button("Increment", Increment)
                        Button("Decrement", Decrement)
                        Child.view "Child 1"
                        Child.view "Child 2"
                        Child.view "Child 3"
                    })
                        .center()
                )
            )
                .environment(EnvironmentKeys.Count, model)
        }

    let createMauiApp () =
        MauiApp
            .CreateBuilder()
            .UseFabulousApp(view)
            .ConfigureFonts(fun fonts ->
                fonts
                    .AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
                    .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold")
                |> ignore)
            .Build()
