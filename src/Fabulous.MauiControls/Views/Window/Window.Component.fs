namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Microsoft.Maui.Controls
open Fabulous
open Fabulous.Maui

module WindowComponent =
    let Activated =
        Attributes.Component.defineEventNoArg "WindowComponent_Activated" (fun target -> (target :?> Window).Activated)

    let Backgrounding =
        Attributes.Component.defineEvent "WindowComponent_Backgrounding" (fun target -> (target :?> Window).Backgrounding)

    let Created =
        Attributes.Component.defineEventNoArg "WindowComponent_Created" (fun target -> (target :?> Window).Created)

    let Deactivated =
        Attributes.Component.defineEventNoArg "WindowComponent_Deactivated" (fun target -> (target :?> Window).Deactivated)

    let Destroying =
        Attributes.Component.defineEventNoArg "WindowComponent_Destroying" (fun target -> (target :?> Window).Destroying)

    let DisplayDensityChanged =
        Attributes.Component.defineEvent "WindowComponent_DisplayDensityChanged" (fun target -> (target :?> Window).DisplayDensityChanged)

    let SizeChanged =
        Attributes.Component.defineEventNoArg "WindowComponent_SizeChanged" (fun target -> (target :?> Window).SizeChanged)

    let Resumed =
        Attributes.Component.defineEventNoArg "WindowComponent_Resumed" (fun target -> (target :?> Window).Resumed)

    let Stopped =
        Attributes.Component.defineEventNoArg "WindowComponent_Stopped" (fun target -> (target :?> Window).Stopped)

[<Extension>]
type WindowComponentModifiers =
    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<unit, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(WindowComponent.Activated.WithValue(fn))

    [<Extension>]
    static member inline onBackgrounding(this: WidgetBuilder<unit, #IFabWindow>, fn: BackgroundingEventArgs -> unit) =
        this.AddScalar(WindowComponent.Backgrounding.WithValue(fn))

    [<Extension>]
    static member inline onCreated(this: WidgetBuilder<unit, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(WindowComponent.Created.WithValue(fn))

    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<unit, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(WindowComponent.Deactivated.WithValue(fn))

    [<Extension>]
    static member inline onDestroying(this: WidgetBuilder<unit, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(WindowComponent.Destroying.WithValue(fn))

    [<Extension>]
    static member inline onDisplayDensityChanged(this: WidgetBuilder<unit, #IFabWindow>, fn: DisplayDensityChangedEventArgs -> unit) =
        this.AddScalar(WindowComponent.DisplayDensityChanged.WithValue(fn))

    [<Extension>]
    static member inline onSizeChanged(this: WidgetBuilder<unit, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(WindowComponent.SizeChanged.WithValue(fn))

    [<Extension>]
    static member inline onResumed(this: WidgetBuilder<unit, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(WindowComponent.Resumed.WithValue(fn))

    [<Extension>]
    static member inline onStopped(this: WidgetBuilder<unit, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(WindowComponent.Stopped.WithValue(fn))
