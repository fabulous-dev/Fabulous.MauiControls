namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui

[<AbstractClass; Sealed>]
type View =
    class
    end

type IFabComponentElement =
    inherit IFabElement

[<Extension>]
type ElementModifiers =
    /// <summary>Listen to the widget being mounted</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch on trigger</param>
    [<Extension>]
    static member inline onMounted(this: WidgetBuilder<'msg, #IFabComponentElement>, fn: unit -> unit) =
        this.AddScalar(ComponentLifecycle.Mounted.WithValue(fn))

    /// <summary>Listen to the widget being unmounted</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch on trigger</param>
    [<Extension>]
    static member inline onUnmounted(this: WidgetBuilder<'msg, #IFabComponentElement>, fn: unit -> unit) =
        this.AddScalar(ComponentLifecycle.Unmounted.WithValue(fn))
