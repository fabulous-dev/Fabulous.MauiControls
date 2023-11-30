namespace HelloComponent

open Fabulous
open Fabulous.Maui
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Graphics
open Microsoft.Maui.Hosting
open type Fabulous.Maui.View

module App =
    let themeViewer() =
        Component() {
            printfn "Evaluated themeViewer()"
            let! theme = Environment(EnvironmentKeys.Theme)
            Label($"Theme is {theme.Current}")
                .center()
        }
        
    let themeSetter() =
        Component() {
            printfn "Evaluated themeSetter()"
            let! theme = Environment(EnvironmentKeys.Theme)
            VStack() {
                Button("Change theme", fun () ->
                    let newTheme =
                        match theme.Current with
                        | AppTheme.Light -> AppTheme.Dark
                        | _ -> AppTheme.Light
                    
                    theme.Set(newTheme)
                )
                
                Button("Unset theme", fun () ->
                    theme.Set(AppTheme.Unspecified)
                )
            }
        }
        
    let themeViewerAndSetter() =
        Component() {
            printfn "Evaluated themeViewerAndSetter()"
            (VStack() {
                themeViewer()
                themeSetter()
            })
                .center()
        }

    let view() =
        printfn "Evaluated view()"
        Application() {
            ContentPage() {
                themeViewerAndSetter()
            }
        }
    
    let createMauiApp() =
        MauiApp
            .CreateBuilder()
            .UseFabulousApp(view)
            .ConfigureFonts(fun fonts ->
                fonts
                    .AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
                    .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold")
                |> ignore)
            .Build()
