namespace Fabulous.Maui

open Fabulous
open System.Runtime.CompilerServices
open Microsoft.Maui.Hosting
open Microsoft.Maui.Controls.Hosting
open System

[<Extension>]
type AppHostBuilderExtensions =
    [<Extension>]
    static member inline private UseFabulousApp
        (this: MauiAppBuilder, canReuseView, logger, syncAction: (unit -> unit) -> unit, [<InlineIfLambda>] viewFn: unit -> Widget)
        : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            let widget = viewFn()

            let treeContext: ViewTreeContext =
                { CanReuseView = canReuseView
                  Logger = logger
                  Dispatch = ignore
                  SyncAction = syncAction
                  GetViewNode = ViewNode.get
                  GetComponent = Component.get
                  SetComponent = Component.set }

            let envContext = new EnvironmentContext()
            let app = FabApplication()

            envContext.Set(EnvironmentKeys.Theme, app.RequestedTheme, false)

            let def = WidgetDefinitionStore.get widget.Key
            let node = def.AttachView(widget, envContext, treeContext, ValueNone, app)

            node.SetHandler("Theme", app.RequestedThemeChanged.Subscribe(fun args -> envContext.Set(EnvironmentKeys.Theme, args.RequestedTheme, false)))

            app)

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<'arg, 'model, 'msg, #IFabApplication>, arg: 'arg) : MauiAppBuilder =
        this.UseFabulousApp(
            program.CanReuseView,
            program.State.Logger,
            program.SyncAction,
            fun () ->
                (View.Component("_") {
                    let! model = Context.Mvu(program.State, arg)
                    program.View model
                })
                    .Compile()
        )

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<unit, 'model, 'msg, #IFabApplication>) : MauiAppBuilder =
        this.UseFabulousApp(program, ())

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<'arg, 'model, 'msg, Memo.Memoized<#IFabApplication>>, arg: 'arg) : MauiAppBuilder =
        this.UseFabulousApp(
            program.CanReuseView,
            program.State.Logger,
            program.SyncAction,
            fun () ->
                (View.Component("_") {
                    let! model = Context.Mvu(program.State, arg)
                    program.View model
                })
                    .Compile()
        )

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<unit, 'model, 'msg, Memo.Memoized<#IFabApplication>>) : MauiAppBuilder =
        this.UseFabulousApp(program, ())

    [<Extension>]
    static member UseFabulousApp
        (this: MauiAppBuilder, view: unit -> WidgetBuilder<unit, #IFabApplication>, ?canReuseView, ?logger, ?syncAction: (unit -> unit) -> unit)
        : MauiAppBuilder =
        this.UseFabulousApp(
            (defaultArg canReuseView MauiViewHelpers.canReuseView),
            (defaultArg logger (ProgramDefaults.defaultLogger())),
            (defaultArg syncAction MauiViewHelpers.defaultSyncAction),
            fun () -> view().Compile()
        )
