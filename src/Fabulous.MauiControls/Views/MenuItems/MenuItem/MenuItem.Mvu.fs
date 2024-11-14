namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module MenuItemMvu =
    let Clicked =
        Attributes.Mvu.defineEventNoArg "MenuItemMvu_Clicked" (fun target -> (target :?> MenuItem).Clicked)

[<AutoOpen>]
module MenuItemMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a MenuItem widget with a text and a Click callback</summary>
        /// <param name="text">The text</param>
        /// <param name="onClicked">The click callback</param>
        static member inline MenuItem(text: string, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabMenuItem>(MenuItem.WidgetKey, MenuItem.Text.WithValue(text), MenuItemMvu.Clicked.WithValue(MsgValue(onClicked)))
