﻿namespace HelloComponent

open Foundation
open Microsoft.Maui

[<Register("AppDelegate")>]
type AppDelegate() =
    inherit MauiUIApplicationDelegate()

    override this.CreateMauiApp() = App.createMauiApp()
