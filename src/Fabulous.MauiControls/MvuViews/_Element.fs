namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui

type IFabMvuElement =
    inherit IFabElement

[<Extension>]
type ElementModifiers =
    /// <summary>Listen to the widget being mounted</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch on trigger</param>
    [<Extension>]
    static member inline onMounted(this: WidgetBuilder<'msg, #IFabMvuElement>, msg: 'msg) =
        this.AddScalar(Lifecycle.Mounted.WithValue(msg))

    /// <summary>Listen to the widget being unmounted</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch on trigger</param>
    [<Extension>]
    static member inline onUnmounted(this: WidgetBuilder<'msg, #IFabMvuElement>, msg: 'msg) =
        this.AddScalar(Lifecycle.Unmounted.WithValue(msg))
