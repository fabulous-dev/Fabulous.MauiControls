namespace HelloComponent

open Fabulous.Maui
open Microsoft.Maui.Hosting
open type Fabulous.Maui.View

module App =
    let view () =
        Component() { Application(ContentPage(Label("Hello from Fabulous for Maui with components!").center())) }

    let createMauiApp () =
        MauiApp
            .CreateBuilder()
            .UseFabulousApp(view())
            .ConfigureFonts(fun fonts ->
                fonts
                    .AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
                    .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold")
                |> ignore)
            .Build()
