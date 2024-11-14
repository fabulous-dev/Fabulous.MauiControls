namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

module ButtonComponent =
    let Clicked =
        Attributes.Component.defineEventNoArg "ButtonComponent_Clicked" (fun target -> (target :?> Button).Clicked)

    let Pressed =
        Attributes.Component.defineEventNoArg "ButtonComponent_Pressed" (fun target -> (target :?> Button).Pressed)

    let Released =
        Attributes.Component.defineEventNoArg "ButtonComponent_Released" (fun target -> (target :?> Button).Released)

[<AutoOpen>]
module ButtonComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a Button widget with a text and listen for the Click event</summary>
        /// <param name="text">The button on the tex</param>
        /// <param name="onClicked">Function to execute</param>
        static member inline Button(text: string, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabButton>(Button.WidgetKey, Button.Text.WithValue(text), ButtonComponent.Clicked.WithValue(onClicked))

[<Extension>]
type ButtonComponentModifiers =
    /// <summary>Listen for the Pressed event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onPressed(this: WidgetBuilder<unit, #IFabButton>, fn: unit -> unit) =
        this.AddScalar(ButtonComponent.Pressed.WithValue(fn))

    /// <summary>Listen for the Released event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to execute</param>
    [<Extension>]
    static member inline onReleased(this: WidgetBuilder<unit, #IFabButton>, fn: unit -> unit) =
        this.AddScalar(ButtonComponent.Released.WithValue(fn))
