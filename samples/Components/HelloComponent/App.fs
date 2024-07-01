namespace HelloComponent

open Fabulous.Maui
open Fabulous.Maui.Components
open Microsoft.Maui.Hosting

open type Fabulous.Maui.View
open type Fabulous.Maui.Components.View

module App =
    let view () =
        Component() { Application(ContentPage() { Label("Hello Component").center() }) }

    let createMauiApp () =
        MauiApp.CreateBuilder().UseFabulousApp(view).Build()
