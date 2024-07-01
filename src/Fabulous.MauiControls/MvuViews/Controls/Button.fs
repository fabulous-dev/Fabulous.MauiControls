namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Microsoft.Maui.Controls
open Fabulous
open Fabulous.Maui

type IFabMvuButton =
    inherit IFabButton
    inherit IFabMvuView

module Button =
    let Clicked =
        MvuAttributes.defineEventNoArg "Button_Clicked" (fun target -> (target :?> Button).Clicked)

    let Pressed =
        MvuAttributes.defineEventNoArg "Button_Pressed" (fun target -> (target :?> Button).Pressed)

    let Released =
        MvuAttributes.defineEventNoArg "Button_Released" (fun target -> (target :?> Button).Released)

[<AutoOpen>]
module ButtonBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a Button widget with a text and listen for the Click event</summary>
        /// <param name="text">The button on the tex</param>
        /// <param name="onClicked">Message to dispatch</param>
        static member inline Button<'msg>(text: string, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabMvuButton>(Button.WidgetKey, Button.Text.WithValue(text), Button.Clicked.WithValue(MsgValue(onClicked)))

[<Extension>]
type ButtonModifiers =

    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<'msg, #IFabMvuButton>, msg: 'msg) =
        this.AddScalar(Button.Pressed.WithValue(MsgValue(msg)))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="msg">Message to dispatch</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<'msg, #IFabMvuButton>, msg: 'msg) =
        this.AddScalar(Button.Released.WithValue(MsgValue(msg)))

    /// <summary>Link a ViewRef to access the direct Button control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuButton>, value: ViewRef<Button>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
