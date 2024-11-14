namespace Fabulous.Maui

open Fabulous
open System.Runtime.CompilerServices
open Microsoft.Maui.Controls

module PointerGestureRecognizerMvu =
    let PointerEntered = Attributes.Mvu.defineEvent "PointerGestureRecognizerMvu_PointerEntered" (fun target -> (target :?> PointerGestureRecognizer).PointerEntered)
    
    let PointerExited = Attributes.Mvu.defineEvent "PointerGestureRecognizerMvu_PointerExited" (fun target -> (target :?> PointerGestureRecognizer).PointerExited)
    
    let PointerMoved = Attributes.Mvu.defineEvent "PointerGestureRecognizerMvu_PointerMoved" (fun target -> (target :?> PointerGestureRecognizer).PointerMoved)
    
    let PointerPressed = Attributes.Mvu.defineEvent "PointerGestureRecognizerMvu_PointerPressed" (fun target -> (target :?> PointerGestureRecognizer).PointerPressed)
    
    let PointerReleased = Attributes.Mvu.defineEvent "PointerGestureRecognizerMvu_PointerReleased" (fun target -> (target :?> PointerGestureRecognizer).PointerReleased)

[<Extension>]
type PointerGestureRecognizerMvuModifiers =
    [<Extension>]
    static member inline onPointerEntered(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerMvu.PointerEntered.WithValue(fn))
        
    [<Extension>]
    static member inline onPointerExited(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerMvu.PointerExited.WithValue(fn))
    [<Extension>]
    static member inline onPointerMoved(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerMvu.PointerMoved.WithValue(fn))
        
    [<Extension>]
    static member inline onPointerPressed(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerMvu.PointerPressed.WithValue(fn))
        
    [<Extension>]
    static member inline onPointerReleased(this: WidgetBuilder<'msg, #IFabPointerGestureRecognizer>, fn: PointerEventArgs -> unit) =
        this.AddScalar(PointerGestureRecognizerMvu.PointerReleased.WithValue(fn))