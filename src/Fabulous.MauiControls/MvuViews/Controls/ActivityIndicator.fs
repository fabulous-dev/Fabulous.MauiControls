namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuActivityIndicator =
    inherit IFabMvuView
    inherit IFabActivityIndicator

[<AutoOpen>]
module ActivityIndicatorBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create an ActivityIndicator widget with a running state</summary>
        /// <param name="isRunning">The running state</param>
        static member inline ActivityIndicator<'msg>(isRunning: bool) =
            WidgetBuilder<'msg, IFabMvuActivityIndicator>(ActivityIndicator.WidgetKey, ActivityIndicator.IsRunning.WithValue(isRunning))

[<Extension>]
type ActivityIndicatorModifiers =
    /// <summary>Link a ViewRef to access the direct ActivityIndicator control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuActivityIndicator>, value: ViewRef<ActivityIndicator>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
