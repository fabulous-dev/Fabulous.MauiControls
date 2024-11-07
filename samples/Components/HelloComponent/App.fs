namespace HelloComponent

open Fabulous.Maui
open Microsoft.Maui.Hosting

open type Fabulous.Maui.View

module App =    
    let view () =
        Component("root") {
            Application() {
                Window() {
                    ContentPage() {
                        (VStack() {
                            Label("Hello Component")
                                .centerTextHorizontal()
                        })
                            .centerVertical()
                    }
                }
            }
        }

    let createMauiApp () =
        MauiApp.CreateBuilder().UseFabulousApp(view).Build()
