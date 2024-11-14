namespace Fabulous.Maui

open Fabulous
open System.Runtime.CompilerServices
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

type IFabPointerGestureRecognizer =
    inherit IFabGestureRecognizer

module PointerGestureRecognizer =
    let WidgetKey = Widgets.register<PointerGestureRecognizer>()
    
[<AutoOpen>]
module PointerGestureRecognizerBuilders =
    type Fabulous.Maui.View with
        static member inline PointerGestureRecognizer() =
            WidgetBuilder<'msg, IFabPointerGestureRecognizer>(PointerGestureRecognizer.WidgetKey, AttributesBundle(StackList.empty(), ValueNone, ValueNone))

[<Extension>]
type PointerGestureRecognizerModifiers =
    /// <summary>Link a ViewRef to access the direct PointerGestureRecognizer control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabPointerGestureRecognizer>, value: ViewRef<PointerGestureRecognizer>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))