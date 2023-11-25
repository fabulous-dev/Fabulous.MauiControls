namespace HelloComponent

open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Hosting
open type Fabulous.Maui.View

module App =
    [<Literal>]
    let letsTry = "key"
    [<Literal>]
    let letsTry2 = "key2"
    
    let grandChild() =
        Component() {
            let! letsTryValue = Environment.Get<int>(letsTry)
            let! letsTryValue2 = Environment.Get<int>(letsTry2)
            
            (VStack() {
                Label($"Environment value 1 is {letsTryValue}")
                    .center()
                Label($"Environment value 2 is {letsTryValue2}")
                    .center()
            })
                .center()
        }
        
    let child() =
        Component() {
            do! Environment.Set(letsTry2, 15)
            
            VStack() {
                Label("Child")
                    .center()
                grandChild()
            }
        }
    
    let view() =
        Component() {
            do! Environment.Set(letsTry, 10)
            
            Application() {
                ContentPage() {
                    (VStack() {
                        Label("Parent")
                            .center()
                        child()
                    })
                        .center()
                }
            }
        }
    
    let createMauiApp() =
        MauiApp
            .CreateBuilder()
            .UseFabulousApp(view())
            .ConfigureFonts(fun fonts ->
                fonts
                    .AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
                    .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold")
                |> ignore)
            .Build()
