namespace Playground

open Android.App
open Android.Widget
open Microsoft.Maui

[<Application>]
type MainApplication(handle, ownership) =
    inherit MauiApplication(handle, ownership)

    do Playground.Resource.UpdateIdValues()

    do CodeReceivedService.Instance <- KeyCodeReceivedService.Current

    override _.CreateMauiApp() = MauiProgram.CreateMauiApp()
