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
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            let app = Program.startApplication program
            Theme.ListenForChanges(app)
            app
        )

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<unit, 'model, 'msg, Memo.Memoized<#IFabApplication>>) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            let app = Program.startApplicationMemo program
            Theme.ListenForChanges(app)
            app
        )

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<'arg, 'model, 'msg, #IFabApplication>, arg: 'arg) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            let app = Program.startApplicationWithArgs arg program
            Theme.ListenForChanges(app)
            app
        )

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<'arg, 'model, 'msg, Memo.Memoized<#IFabApplication>>, arg: 'arg) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            let app = Program.startApplicationWithArgsMemo arg program
            Theme.ListenForChanges(app)
            app
        )
