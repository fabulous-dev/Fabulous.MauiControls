namespace HelloComponent

open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Hosting
open type Fabulous.Maui.View

module EnvironmentKeys =
    let LetsTry = EnvironmentKey.Create<int>("letsTry")
    let LetsTry2 = EnvironmentKey.Create<int>("letsTry2")

module App =
    [<Literal>]
    let letsTry = "key"
    [<Literal>]
    let letsTry2 = "key2"
    
    let grandChild() =
        Component() {
            let! letsTryValue = Environment.Get(EnvironmentKeys.LetsTry)
            let! letsTryValue2 = Environment.Get(EnvironmentKeys.LetsTry2)
            
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
            let! valueBeforeOverriding = Environment.Get(EnvironmentKeys.LetsTry)
            do! Environment.Set(EnvironmentKeys.LetsTry, 100)
            do! Environment.Set(EnvironmentKeys.LetsTry2, 15)
            
            VStack() {
                Label("Child")
                    .center()
                    
                Label($"Environment value before overriding is {valueBeforeOverriding}")
                grandChild()
            }
        }
    
    let view() =
        Component() {
            do! Environment.Set(EnvironmentKeys.LetsTry, 10)
            
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
