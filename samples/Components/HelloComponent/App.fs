namespace HelloComponent

open Fabulous
open Fabulous.Maui
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Hosting
open type Fabulous.Maui.View

module App =
    let Count = EnvironmentKey("Count", 0)
    
    let subCountViewer() =
        Component() {
            let! count = Environment(Count)
            Label($"[SubCountViewer] Count = {count.Current}")
        }
    
    let subCountSetter() =
        Component() {
            let! count = Environment(Count)
            VStack() {
                Button("[SubCountSetter] Increment", fun () -> count.Set(count.Current + 1))
                Button("[SubCountSetter] Decrement", fun () -> count.Set(count.Current - 1))
            }
        }
        
    let subCount() =
        (Component() {
            VStack() {
                subCountViewer()
                subCountSetter()
            }
        })
            .environment(Count, 10)
        
    let countViewer() =
        Component() {
            let! count = Environment(Count)
            VStack() {
                Label($"[CountViewer] Count = {count.Current}")
            }
        }
        
    let countSetter() =
        Component() {
            let! count = Environment(Count)
            VStack() {
                Button("[CountSetter] Increment", fun () -> count.Set(count.Current + 1))
                Button("[CountSetter] Decrement", fun () -> count.Set(count.Current - 1))
            }
        }
        
    let count'() =
        Component() {
            VStack() {
                countViewer()
                countSetter()
            }
        }

    let view() =
        (Component() {
            let! count = Environment(Count)
            Application() {
                ContentPage() {
                    VStack() {
                        Label($"[view] Count = {count.Current}")
                        Button("[view] Increment", fun () -> count.Set(count.Current + 1))
                        Button("[view] Decrement", fun () -> count.Set(count.Current - 1))
                        
                        count'()
                        subCount()
                    }
                }
            }
        })
            .environment(Count, 0)
    
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
