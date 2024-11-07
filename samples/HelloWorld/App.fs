namespace HelloWorld

open Fabulous.Maui
open Microsoft.Maui.Hosting

open type Fabulous.Maui.View

module App =
    let view () =
        Application(ContentPage(Label("Hello World").center()))

type MauiProgram =
    static member CreateMauiApp() =
        MauiApp.CreateBuilder().UseFabulousApp(App.view).Build()
