namespace Fabulous.Maui

open Fabulous
open System.Runtime.CompilerServices
open Microsoft.Maui.Hosting
open Microsoft.Maui.Controls.Hosting
open System

[<Extension>]
type AppHostBuilderExtensions =
    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<unit, 'model, 'msg, #IFabApplication>) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) -> (Program.startApplication program) :> Microsoft.Maui.IApplication)

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<'arg, 'model, 'msg, #IFabApplication>, arg: 'arg) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) -> (Program.startApplicationWithArgs arg program) :> Microsoft.Maui.IApplication)
