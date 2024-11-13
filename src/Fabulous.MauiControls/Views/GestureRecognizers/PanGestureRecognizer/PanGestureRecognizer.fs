namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabPanGestureRecognizer =
    inherit IFabGestureRecognizer

module PanGestureRecognizer =
    let WidgetKey = Widgets.register<PanGestureRecognizer>()
    
    let TouchPoints =
        Attributes.defineBindableInt PanGestureRecognizer.TouchPointsProperty

[<Extension>]
type PanGestureRecognizerModifiers =
    /// <summary>Set the number of touch points to trigger the gesture</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The number of touch points that must be present on the screen to trigger the gesture</param>
    [<Extension>]
    static member inline touchPoints(this: WidgetBuilder<'msg, #IFabPanGestureRecognizer>, value: int) =
        this.AddScalar(PanGestureRecognizer.TouchPoints.WithValue(value))

    /// <summary>Link a ViewRef to access the direct PanGestureRecognizer control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabPanGestureRecognizer>, value: ViewRef<PanGestureRecognizer>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
