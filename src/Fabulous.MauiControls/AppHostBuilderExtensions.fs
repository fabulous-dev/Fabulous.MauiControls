namespace Fabulous.Maui

open Fabulous
open System.Runtime.CompilerServices
open Microsoft.Maui.Controls
open Microsoft.Maui.Hosting
open Microsoft.Maui.Controls.Hosting
open System

module AppHostBuilderHelper =
    let registerComponentFunctions() =
        Component.SetComponentFunctions(
            (fun view -> (view :?> BindableObject).GetValue(ComponentProperty) :?> Component),
            (fun view comp -> (view :?> BindableObject).SetValue(ComponentProperty, comp))
        )

[<Extension>]
type AppHostBuilderExtensions =
    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<unit, 'model, 'msg, #IFabApplication>) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            AppHostBuilderHelper.registerComponentFunctions()
            (Program.startApplication program) :> Microsoft.Maui.IApplication
        )

    [<Extension>]
    static member UseFabulousApp(this: MauiAppBuilder, program: Program<'arg, 'model, 'msg, #IFabApplication>, arg: 'arg) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            AppHostBuilderHelper.registerComponentFunctions()
            (Program.startApplicationWithArgs arg program) :> Microsoft.Maui.IApplication
        )
        
    [<Extension>]
    static member inline UseFabulousApp(this: MauiAppBuilder, [<InlineIfLambda>] root: unit -> WidgetBuilder<'msg, #IFabApplication>) : MauiAppBuilder =
        this.UseMauiApp(fun (_serviceProvider: IServiceProvider) ->
            AppHostBuilderHelper.registerComponentFunctions()
            
            let widget = root().Compile()
            let widgetDef = WidgetDefinitionStore.get widget.Key
            
            let viewTreeContext =
                { CanReuseView = MauiViewHelpers.canReuseView
                  GetViewNode = ViewNode.get
                  Logger = MauiViewHelpers.defaultLogger()
                  Dispatch = ignore }
            
            let struct (_node, view) = widgetDef.CreateView(widget, viewTreeContext, ValueNone)
            
            view :?> Microsoft.Maui.IApplication
        )
