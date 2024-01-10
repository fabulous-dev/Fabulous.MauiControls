namespace Fabulous.Maui

open System
open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Fabulous.WidgetCollectionAttributeDefinitions
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Controls

module MauiViewHelpers =
    let private tryGetScalarValue (widget: Widget) (def: SimpleScalarAttributeDefinition<'data>) =
        match widget.ScalarAttributes with
        | ValueNone -> ValueNone
        | ValueSome scalarAttrs ->
            match Array.tryFind (fun (attr: ScalarAttribute) -> attr.Key = def.Key) scalarAttrs with
            | None -> ValueNone
            | Some attr -> ValueSome(unbox<'data> attr.Value)

    let private tryGetWidgetCollectionValue (widget: Widget) (def: WidgetCollectionAttributeDefinition) =
        match widget.WidgetCollectionAttributes with
        | ValueNone -> ValueNone
        | ValueSome collectionAttrs ->
            match Array.tryFind (fun (attr: WidgetCollectionAttribute) -> attr.Key = def.Key) collectionAttrs with
            | None -> ValueNone
            | Some attr -> ValueSome attr.Value

    /// Extend the canReuseView function to check Microsoft.Maui specific constraints
    let rec canReuseView (prev: Widget) (curr: Widget) =
        if ViewHelpers.canReuseView prev curr && canReuseAutomationId prev curr then
            let def = WidgetDefinitionStore.get curr.Key

            // TargetType can be null for MemoWidget
            // but it has already been checked by Fabulous.ViewHelpers.canReuseView
            if def.TargetType <> null then
                if def.TargetType.IsAssignableTo(typeof<NavigationPage>) then
                    canReuseNavigationPage prev curr
                else
                    true
            else
                true
        else
            false

    /// Check whether widgets have compatible automation ids.
    /// Microsoft.Maui only allows setting the automation id once so we can't reuse a control if the id is not the same.
    and private canReuseAutomationId (prev: Widget) (curr: Widget) =
        let prevIdOpt = tryGetScalarValue prev Element.AutomationId

        let currIdOpt = tryGetScalarValue curr Element.AutomationId

        match prevIdOpt with
        | ValueSome _ when prevIdOpt <> currIdOpt -> false
        | _ -> true

    /// Checks whether an underlying NavigationPage control can be reused given the previous and new view elements
    //
    // NavigationPage can be reused only if the pages don't change their type (added/removed pages don't prevent reuse)
    // E.g. If the first page switch from ContentPage to TabbedPage, the NavigationPage can't be reused.
    and private canReuseNavigationPage (prev: Widget) (curr: Widget) =
        let prevPages = tryGetWidgetCollectionValue prev NavigationPage.Pages

        let currPages = tryGetWidgetCollectionValue curr NavigationPage.Pages

        match struct (prevPages, currPages) with
        | ValueSome prevPages, ValueSome currPages ->
            let struct (prevLength, prevPages) = prevPages
            let struct (currLength, currPages) = currPages

            if prevLength = currLength then
                Array.forall2 (fun (a: Widget) (b: Widget) -> a.Key = b.Key) prevPages currPages
            else
                true

        | _ -> true

module Program =
    let inline private define (view: 'model -> WidgetBuilder<'msg, 'marker>) (program: Program<'arg, 'model, 'msg>) : Program<'arg, 'model, 'msg, 'marker> =
        let env =
            { Initialize =
                fun env ->
                    program.Environment.Initialize(env)
                    EnvironmentHelpers.initialize(env)
              Subscribe =
                fun (env, target) ->
                    let fab = program.Environment.Subscribe(env, target)
                    let maui = EnvironmentHelpers.subscribe(env, target)

                    { new IDisposable with
                        member this.Dispose() =
                            fab.Dispose()
                            maui.Dispose() } }

        { Program = { program with Environment = env }
          View = view
          CanReuseView = MauiViewHelpers.canReuseView
          SyncAction = MainThread.BeginInvokeOnMainThread }

    /// Create a program for a static view
    let stateless (view: unit -> WidgetBuilder<unit, 'marker>) =
        let program =
            Program.ForComponent.define (fun () -> (), Cmd.none) (fun () () -> (), Cmd.none)

        define view program

    /// Create a program using an MVU loop
    let stateful (init: 'arg -> 'model) (update: 'msg -> 'model -> 'model) (view: 'model -> WidgetBuilder<'msg, 'marker>) =
        define view (Program.ForComponent.stateful init update)

    /// Create a program using an MVU loop. Add support for Cmd
    let statefulWithCmd
        (init: 'arg -> 'model * Cmd<'msg>)
        (update: 'msg -> 'model -> 'model * Cmd<'msg>)
        (view: 'model -> WidgetBuilder<'msg, #IFabApplication>)
        =
        define view (Program.ForComponent.statefulWithCmd init update)

    /// Create a program using an MVU loop. Add support for Cmd
    let statefulWithCmdMemo
        (init: 'arg -> 'model * Cmd<'msg>)
        (update: 'msg -> 'model -> 'model * Cmd<'msg>)
        (view: 'model -> WidgetBuilder<'msg, Memo.Memoized<#IFabApplication>>)
        =
        define view (Program.ForComponent.statefulWithCmd init update)

    /// Create a program using an MVU loop. Add support for CmdMsg
    let statefulWithCmdMsg
        (init: 'arg -> 'model * 'cmdMsg list)
        (update: 'msg -> 'model -> 'model * 'cmdMsg list)
        (view: 'model -> WidgetBuilder<'msg, 'marker>)
        (mapCmd: 'cmdMsg -> Cmd<'msg>)
        =
        define view (Program.ForComponent.statefulWithCmdMsg init update mapCmd)

    /// Start the program
    let startApplicationWithArgs (arg: 'arg) (program: Program<'arg, 'model, 'msg, #IFabApplication>) : Application =
        let stateKey = StateStore.getNextKey()
        let runner = Runners.create stateKey program.Program
        runner.Start(arg)
        let adapter = ViewAdapters.create ViewNode.get stateKey program runner
        adapter.CreateView() |> unbox

    /// Start the program
    let startApplication (program: Program<unit, 'model, 'msg, #IFabApplication>) : Application = startApplicationWithArgs () program

    /// Start the program
    let startApplicationWithArgsMemo (arg: 'arg) (program: Program<'arg, 'model, 'msg, Memo.Memoized<#IFabApplication>>) : Application =
        let stateKey = StateStore.getNextKey()
        let runner = Runners.create stateKey program.Program
        runner.Start(arg)
        let adapter = ViewAdapters.create ViewNode.get stateKey program runner
        let view = adapter.CreateView()
        unbox view

    /// Start the program
    let startApplicationMemo (program: Program<unit, 'model, 'msg, Memo.Memoized<'marker>>) : Application = startApplicationWithArgsMemo () program

    /// Subscribe to external source of events.
    /// The subscription is called once - with the initial model, but can dispatch new messages at any time.
    let withSubscription (subscribe: 'model -> Cmd<'msg>) (program: Program<'arg, 'model, 'msg, 'marker>) =
        { program with
            Program = Program.ForComponent.withSubscription subscribe program.Program }

    /// Configure how the output messages from Fabulous will be handled
    let withLogger (logger: Logger) (program: Program<'arg, 'model, 'msg, 'marker>) =
        { program with
            Program = Program.ForComponent.withLogger logger program.Program }

    /// Trace all the updates to the debug output
    let withTrace (trace: string * string -> unit) (program: Program<'arg, 'model, 'msg, 'marker>) =
        let traceView model =
            trace("View, model = {0}", $"%0A{model}")

            try
                let info = program.View(model)
                trace("View result: {0}", $"%0A{info}")
                info
            with e ->
                trace("Error in view function: {0}", $"%0A{e}")
                reraise()

        { program with
            Program = Program.ForComponent.withTrace trace program.Program
            View = traceView }

    /// Configure how the unhandled exceptions happening during the execution of a Fabulous app with be handled
    let withExceptionHandler (handler: exn -> bool) (program: Program<'arg, 'model, 'msg, 'marker>) =
        { program with
            Program = Program.ForComponent.withExceptionHandler handler program.Program }

    /// Allow the app to react to theme changes
    let withThemeAwareness (program: Program<'arg, 'model, 'msg, #IFabApplication>) =
        { Program =
            { Environment = program.Program.Environment
              Init = ThemeAwareProgram.init program.Program.Init
              Update = ThemeAwareProgram.update program.Program.Update
              Subscribe = fun model -> program.Program.Subscribe model.Model |> Cmd.map ThemeAwareProgram.Msg.ModelMsg
              Logger = program.Program.Logger
              ExceptionHandler = program.Program.ExceptionHandler }
          View = ThemeAwareProgram.view program.View
          CanReuseView = program.CanReuseView
          SyncAction = program.SyncAction }

[<RequireQualifiedAccess>]
module CmdMsg =
    let batch mapCmdMsgFn mapCmdFn cmdMsgs =
        cmdMsgs |> List.map(mapCmdMsgFn >> Cmd.map mapCmdFn) |> Cmd.batch
