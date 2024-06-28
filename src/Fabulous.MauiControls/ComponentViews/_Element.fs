namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui

type IFabComponentElement = inherit IFabElement

[<Extension>]
type ElementModifiers =
    /// <summary>Listen to the widget being mounted</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">TODO</param>
    [<Extension>]
    static member inline onMounted(this: WidgetBuilder<'msg, #IFabComponentElement>, fn: unit -> unit) =
        this // TODO: Lifecycle.Mounted is inside Fabulous but doesn't have a component equivalent

    /// <summary>Listen to the widget being unmounted</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">TODO</param>
    [<Extension>]
    static member inline onUnmounted(this: WidgetBuilder<'msg, #IFabComponentElement>, fn: unit -> unit) =
        this // TODO: Lifecycle.Unmounted is inside Fabulous but doesn't have a component equivalent