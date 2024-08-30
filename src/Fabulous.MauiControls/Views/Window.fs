namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Microsoft.Maui
open Microsoft.Maui.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Maui

type IFabWindow =
    inherit IFabNavigableElement

module Window =
    let WidgetKey = Widgets.register<Window>()

    let Page = Attributes.defineBindableWidget Window.PageProperty

    let FlowDirection =
        Attributes.defineBindableWithEquality Window.FlowDirectionProperty

    let Height = Attributes.defineBindableWithEquality Window.HeightProperty

    let MaximumHeight =
        Attributes.defineBindableWithEquality Window.MaximumHeightProperty

    let MaximumWidth = Attributes.defineBindableWithEquality Window.MaximumWidthProperty

    let MinimumHeight =
        Attributes.defineBindableWithEquality Window.MinimumHeightProperty

    let MinimumWidth = Attributes.defineBindableWithEquality Window.MinimumWidthProperty

    let ActivatedMsg =
        Attributes.defineEventNoArg "Window_ActivatedMsg" (fun target -> (target :?> Window).Activated)

    let ActivatedFn =
        Attributes.defineEventNoArgNoDispatch "Window_ActivatedFn" (fun target -> (target :?> Window).Activated)

    let BackgroundingMsg =
        Attributes.defineEvent "Window_BackgroundingMsg" (fun target -> (target :?> Window).Backgrounding)

    let BackgroundingFn =
        Attributes.defineEventNoDispatch "Window_BackgroundingFn" (fun target -> (target :?> Window).Backgrounding)

    let CreatedMsg =
        Attributes.defineEventNoArg "Window_CreatedMsg" (fun target -> (target :?> Window).Created)

    let CreatedFn =
        Attributes.defineEventNoArgNoDispatch "Window_CreatedFn" (fun target -> (target :?> Window).Created)

    let DeactivatedMsg =
        Attributes.defineEventNoArg "Window_DeactivatedMsg" (fun target -> (target :?> Window).Deactivated)

    let DeactivatedFn =
        Attributes.defineEventNoArgNoDispatch "Window_DeactivatedFn" (fun target -> (target :?> Window).Deactivated)

    let DestroyingMsg =
        Attributes.defineEventNoArg "Window_DestroyingMsg" (fun target -> (target :?> Window).Destroying)

    let DestroyingFn =
        Attributes.defineEventNoArgNoDispatch "Window_DestroyingFn" (fun target -> (target :?> Window).Destroying)

    let DisplayDensityChangedMsg =
        Attributes.defineEvent "Window_DisplayDensityChangedMsg" (fun target -> (target :?> Window).DisplayDensityChanged)

    let DisplayDensityChangedFn =
        Attributes.defineEventNoDispatch "Window_DisplayDensityChangedFn" (fun target -> (target :?> Window).DisplayDensityChanged)

    let SizeChangedMsg =
        Attributes.defineEventNoArg "Window_SizeChangedMsg" (fun target -> (target :?> Window).SizeChanged)

    let SizeChangedFn =
        Attributes.defineEventNoArgNoDispatch "Window_SizeChangedFn" (fun target -> (target :?> Window).SizeChanged)

    let ResumedMsg =
        Attributes.defineEventNoArg "Window_ResumedMsg" (fun target -> (target :?> Window).Resumed)

    let ResumedFn =
        Attributes.defineEventNoArgNoDispatch "Window_ResumedFn" (fun target -> (target :?> Window).Resumed)

    let StoppedMsg =
        Attributes.defineEventNoArg "Window_StoppedMsg" (fun target -> (target :?> Window).Stopped)

    let StoppedFn =
        Attributes.defineEventNoArgNoDispatch "Window_StoppedFn" (fun target -> (target :?> Window).Stopped)

    let Title = Attributes.defineBindableWithEquality Window.TitleProperty

    let Width = Attributes.defineBindableWithEquality Window.WidthProperty

    let X = Attributes.defineBindableWithEquality Window.XProperty

    let Y = Attributes.defineBindableWithEquality Window.YProperty

[<AutoOpen>]
module WindowBuilders =
    type Fabulous.Maui.View with

        static member inline Window(content: WidgetBuilder<'msg, #IFabPage>) =
            WidgetBuilder<'msg, IFabWindow>(
                Window.WidgetKey,
                AttributesBundle(StackList.empty(), ValueSome [| Window.Page.WithValue(content.Compile()) |], ValueNone)
            )

[<Extension>]
type WindowModifiers =
    [<Extension>]
    static member inline flowDirection(this: WidgetBuilder<'msg, #IFabWindow>, value: FlowDirection) =
        this.AddScalar(Window.FlowDirection.WithValue(value))

    [<Extension>]
    static member inline height(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.Height.WithValue(value))

    [<Extension>]
    static member inline maximumHeight(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MaximumHeight.WithValue(value))

    [<Extension>]
    static member inline maximumWidth(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MaximumWidth.WithValue(value))

    [<Extension>]
    static member inline minimumHeight(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MinimumHeight.WithValue(value))

    [<Extension>]
    static member inline minimumWidth(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MinimumWidth.WithValue(value))

    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(Window.ActivatedMsg.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<'msg, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(Window.ActivatedFn.WithValue(fn))

    [<Extension>]
    static member inline onBackgrounding(this: WidgetBuilder<'msg, #IFabWindow>, fn: BackgroundingEventArgs -> 'msg) =
        this.AddScalar(Window.BackgroundingMsg.WithValue(fn))

    [<Extension>]
    static member inline onBackgrounding(this: WidgetBuilder<'msg, #IFabWindow>, fn: BackgroundingEventArgs -> unit) =
        this.AddScalar(Window.BackgroundingFn.WithValue(fn))

    [<Extension>]
    static member inline onCreated(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(Window.CreatedMsg.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onCreated(this: WidgetBuilder<'msg, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(Window.CreatedFn.WithValue(fn))

    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(Window.DeactivatedMsg.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(Window.DeactivatedFn.WithValue(fn))

    [<Extension>]
    static member inline onDestroying(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(Window.DestroyingMsg.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onDestroying(this: WidgetBuilder<'msg, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(Window.DestroyingFn.WithValue(fn))

    [<Extension>]
    static member inline onDisplayDensityChanged(this: WidgetBuilder<'msg, #IFabWindow>, fn: DisplayDensityChangedEventArgs -> 'msg) =
        this.AddScalar(Window.DisplayDensityChangedMsg.WithValue(fn))

    [<Extension>]
    static member inline onDisplayDensityChanged(this: WidgetBuilder<'msg, #IFabWindow>, fn: DisplayDensityChangedEventArgs -> unit) =
        this.AddScalar(Window.DisplayDensityChangedFn.WithValue(fn))

    [<Extension>]
    static member inline onSizeChanged(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(Window.SizeChangedMsg.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onSizeChanged(this: WidgetBuilder<'msg, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(Window.SizeChangedFn.WithValue(fn))

    [<Extension>]
    static member inline onResumed(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(Window.ResumedMsg.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onResumed(this: WidgetBuilder<'msg, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(Window.ResumedFn.WithValue(fn))

    [<Extension>]
    static member inline onStopped(this: WidgetBuilder<'msg, #IFabWindow>, msg: 'msg) =
        this.AddScalar(Window.StoppedMsg.WithValue(MsgValue msg))

    [<Extension>]
    static member inline onStopped(this: WidgetBuilder<'msg, #IFabWindow>, fn: unit -> unit) =
        this.AddScalar(Window.StoppedFn.WithValue(fn))

    [<Extension>]
    static member inline title(this: WidgetBuilder<'msg, #IFabWindow>, value: string) =
        this.AddScalar(Window.Title.WithValue(value))

    [<Extension>]
    static member inline width(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.Width.WithValue(value))

    [<Extension>]
    static member inline x(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.X.WithValue(value))

    [<Extension>]
    static member inline y(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.Y.WithValue(value))
