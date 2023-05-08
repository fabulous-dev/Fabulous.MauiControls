namespace NavigationSample

open Android.App
open Microsoft.Maui

[<Application>]
type MainApplication(handle, ownership) =
    inherit MauiApplication(handle, ownership)

    do NavigationSample.Resource.UpdateIdValues()

    override _.CreateMauiApp() = MauiProgram.CreateMauiApp()
