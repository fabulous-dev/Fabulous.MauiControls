namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module MenuItemComponent =
    let Clicked =
        Attributes.Component.defineEventNoArg "MenuItemComponent_Clicked" (fun target -> (target :?> MenuItem).Clicked)

[<AutoOpen>]
module MenuItemComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a MenuItem widget with a text and a Click callback</summary>
        /// <param name="text">The text</param>
        /// <param name="onClicked">The click callback</param>
        static member inline MenuItem(text: string, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabMenuItem>(MenuItem.WidgetKey, MenuItem.Text.WithValue(text), MenuItemComponent.Clicked.WithValue(onClicked))
