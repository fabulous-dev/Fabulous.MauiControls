namespace Fabulous.Maui

open Fabulous
open System.Runtime.CompilerServices
open Microsoft.Maui.Hosting
open Microsoft.Maui.Controls.Hosting
open System

[<Extension>]
type AppHostBuilderExtensions =
    [<Extension>]
    static member inline private UseFabulousApp(this: MauiAppBuilder, canReuseView, logger, [<InlineIfLambda>] viewFn: unit -> Widget) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            let widget = viewFn()

            let treeContext: ViewTreeContext =
                { CanReuseView = canReuseView
                  Logger = logger
                  Dispatch = ignore
                  GetViewNode = ViewNode.get
                  GetComponent = Component.get }

            let def = WidgetDefinitionStore.get widget.Key
            let struct (_, view) = def.CreateView(widget, treeContext, ValueNone)
            let app = view :?> Microsoft.Maui.Controls.Application
            Theme.ListenForChanges(app)
            app)

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<'arg, 'model, 'msg, #IFabApplication>, arg: 'arg) : MauiAppBuilder =
        this.UseFabulousApp(
            program.CanReuseView,
            program.State.Logger,
            fun () ->
                (View.Component(program.State, arg) {
                    let! model = Mvu.State
                    program.View model
                })
                    .Compile()
        )

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<unit, 'model, 'msg, #IFabApplication>) : MauiAppBuilder =
        this.UseFabulousApp(program, ())

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, view: unit -> WidgetBuilder<unit, #IFabApplication>, ?canReuseView, ?logger) : MauiAppBuilder =
        this.UseFabulousApp(
            (match canReuseView with
             | Some fn -> fn
             | None -> MauiViewHelpers.canReuseView),
            (match logger with
             | Some logger -> logger
             | None -> ProgramDefaults.defaultLogger()),
            fun () -> (View.Component() { view() }).Compile()
        )
