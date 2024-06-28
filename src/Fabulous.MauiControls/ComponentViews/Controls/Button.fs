namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Microsoft.Maui.Controls
open Fabulous
open Fabulous.Maui

type IFabComponentButton =
    inherit IFabButton
    inherit IFabComponentView
    
module Button =
    let Clicked =
        Attributes.defineEventNoArgNoDispatch "Button_Clicked" (fun target -> (target :?> Button).Clicked)

    let Pressed =
        Attributes.defineEventNoArgNoDispatch "Button_Pressed" (fun target -> (target :?> Button).Pressed)
    
    let Released =
        Attributes.defineEventNoArgNoDispatch "Button_Released" (fun target -> (target :?> Button).Released)

[<AutoOpen>]
module ButtonBuilders =
    type Fabulous.Maui.View with
    
        /// <summary>Create a Button widget with a text and listen for the Click event</summary>
        /// <param name="text">The button on the tex</param>
        /// <param name="onClicked">Function to execute</param>
        static member inline Button(text: string, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabComponentButton>(Button.WidgetKey, Button.Text.WithValue(text), Button.Clicked.WithValue(onClicked))
            
[<Extension>]
type ButtonModifiers =

    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">TODO</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<'msg, #IFabComponentButton>, fn: unit -> unit) =
        this.AddScalar(Button.Pressed.WithValue(fn))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">TODO</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<'msg, #IFabComponentButton>, fn: unit -> unit) =
        this.AddScalar(Button.Released.WithValue(fn))

    /// <summary>Link a ViewRef to access the direct Button control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentButton>, value: ViewRef<Button>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))