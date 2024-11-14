namespace Fabulous.Maui

open Fabulous
open System.Runtime.CompilerServices
open Microsoft.Maui.Controls

module PointerGestureRecognizerComponent =
    let PointerEntered = Attributes.Component.defineEvent "PointerGestureRecognizerComponent_PointerEntered" (fun target -> (target :?> PointerGestureRecognizer).PointerEntered)
    
    let PointerExited = Attributes.Component.defineEvent "PointerGestureRecognizerComponent_PointerExited" (fun target -> (target :?> PointerGestureRecognizer).PointerExited)
    
    let PointerMoved = Attributes.Component.defineEvent "PointerGestureRecognizerComponent_PointerMoved" (fun target -> (target :?> PointerGestureRecognizer).PointerMoved)
    
    let PointerPressed = Attributes.Component.defineEvent "PointerGestureRecognizerComponent_PointerPressed" (fun target -> (target :?> PointerGestureRecognizer).PointerPressed)
    
    let PointerReleased = Attributes.Component.defineEvent "PointerGestureRecognizerComponent_PointerReleased" (fun target -> (target :?> PointerGestureRecognizer).PointerReleased)

[<Extension>]
type PointerGestureRecognizerComponentModifiers =
    [<Extension>]
    static member inline onPointerEntered(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerComponent.PointerEntered.WithValue(fn))
        
    [<Extension>]
    static member inline onPointerExited(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerComponent.PointerExited.WithValue(fn))
    [<Extension>]
    static member inline onPointerMoved(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerComponent.PointerMoved.WithValue(fn))
        
    [<Extension>]
    static member inline onPointerPressed(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerComponent.PointerPressed.WithValue(fn))
        
    [<Extension>]
    static member inline onPointerReleased(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerComponent.PointerReleased.WithValue(fn))