namespace HelloWorld

open Fabulous.Maui
open Fabulous.Maui.Mvu
open Microsoft.Maui.Hosting

open type Fabulous.Maui.View
open type Fabulous.Maui.Mvu.View

module App =
    let view () =
        Application(ContentPage(Label("Hello World").center()))

type MauiProgram =
    static member CreateMauiApp() =
        MauiApp.CreateBuilder().UseFabulousApp(App.view).Build()
