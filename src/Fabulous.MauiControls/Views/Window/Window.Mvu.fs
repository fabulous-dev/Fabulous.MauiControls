namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Microsoft.Maui.Controls
open Fabulous
open Fabulous.Maui

module WindowMvu =
    let Activated =
        Attributes.Mvu.defineEventNoArg "WindowMvu_Activated" (fun target -> (target :?> Window).Activated)

    let Backgrounding =
        Attributes.Mvu.defineEvent "WindowMvu_Backgrounding" (fun target -> (target :?> Window).Backgrounding)

    let Created =
        Attributes.Mvu.defineEventNoArg "WindowMvu_Created" (fun target -> (target :?> Window).Created)

    let Deactivated =
        Attributes.Mvu.defineEventNoArg "WindowMvu_Deactivated" (fun target -> (target :?> Window).Deactivated)

    let Destroying =
        Attributes.Mvu.defineEventNoArg "WindowMvu_Destroying" (fun target -> (target :?> Window).Destroying)

    let DisplayDensityChanged =
        Attributes.Mvu.defineEvent "WindowMvu_DisplayDensityChanged" (fun target -> (target :?> Window).DisplayDensityChanged)

    let SizeChanged =
        Attributes.Mvu.defineEventNoArg "WindowMvu_SizeChanged" (fun target -> (target :?> Window).SizeChanged)

    let Resumed =
        Attributes.Mvu.defineEventNoArg "WindowMvu_Resumed" (fun target -> (target :?> Window).Resumed)

    let Stopped =
        Attributes.Mvu.defineEventNoArg "WindowMvu_Stopped" (fun target -> (target :?> Window).Stopped)

[<Extension>]
type WindowMvuModifiers =
    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(WindowMvu.Activated.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onBackgrounding(this: WidgetBuilder<'msg, #IFabWindow>, fn: BackgroundingEventArgs -> 'msg) =
        this.AddScalar(WindowMvu.Backgrounding.WithValue(fn))

    [<Extension>]
    static member inline onCreated(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(WindowMvu.Created.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(WindowMvu.Deactivated.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onDestroying(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(WindowMvu.Destroying.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onDisplayDensityChanged(this: WidgetBuilder<'msg, #IFabWindow>, fn: DisplayDensityChangedEventArgs -> 'msg) =
        this.AddScalar(WindowMvu.DisplayDensityChanged.WithValue(fn))

    [<Extension>]
    static member inline onSizeChanged(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(WindowMvu.SizeChanged.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onResumed(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(WindowMvu.Resumed.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onStopped(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(WindowMvu.Stopped.WithValue(MsgValue msg))
