namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ButtonMvu =
    let Clicked =
        Attributes.Mvu.defineEventNoArg "ButtonMvu_Clicked" (fun target -> (target :?> Button).Clicked)

    let Pressed =
        Attributes.Mvu.defineEventNoArg "ButtonMvu_Pressed" (fun target -> (target :?> Button).Pressed)

    let Released =
        Attributes.Mvu.defineEventNoArg "ButtonMvu_Released" (fun target -> (target :?> Button).Released)

[<AutoOpen>]
module ButtonMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Button widget with a text and listen for the Click event</summary>
        /// <param name="text">The button on the tex</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline Button(text: string, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabButton>(Button.WidgetKey, Button.Text.WithValue(text), ButtonMvu.Clicked.WithValue(MsgValue(onClicked)))

[<Extension>]
type ButtonMvuModifiers =
    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<'msg, #IFabButton>, msg: 'msg) =
        this.AddScalar(ButtonMvu.Pressed.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<'msg, #IFabButton>, msg: 'msg) =
        this.AddScalar(ButtonMvu.Released.WithValue(MsgValue(msg)))
