namespace Fabulous.Maui

open System
open Fabulous
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Controls

open type Fabulous.Maui.View

[<AbstractClass; Sealed>]
type EnvironmentKeys =
    class
    end

module Theme =
    let Key = EnvironmentKey("Theme", AppInfo.RequestedTheme)

    let initialize (env: EnvironmentContext) = env.Set(Key, Key.DefaultValue, false)

    let subscribe (env: EnvironmentContext) (target: obj) =
        let app = target :?> Application

        let fromSource =
            app.RequestedThemeChanged.Subscribe(fun args -> env.Set(Key, args.RequestedTheme, false))

        let toSource =
            env.ValueChanged.Subscribe(fun args ->
                if args.Key = Key.Key && args.FromUserCode = true then
                    let newValue =
                        match args.Value with
                        | ValueNone -> AppTheme.Unspecified
                        | ValueSome value -> value :?> AppTheme

                    app.UserAppTheme <- newValue)

        { new IDisposable with
            member _.Dispose() =
                fromSource.Dispose()
                toSource.Dispose() }

[<AutoOpen>]
module EnvironmentBuilders =
    type EnvironmentKeys with

        static member Theme = Theme.Key

module EnvironmentHelpers =
    let initialize (env: EnvironmentContext) = Theme.initialize env

    let subscribe (env: EnvironmentContext, target: obj) =
        let theme = Theme.subscribe env target

        { new IDisposable with
            member _.Dispose() = theme.Dispose() }
