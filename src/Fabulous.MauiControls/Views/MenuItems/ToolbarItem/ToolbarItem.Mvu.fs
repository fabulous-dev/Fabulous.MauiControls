namespace Fabulous.Maui

open Fabulous

[<AutoOpen>]
module ToolbarItemMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a ToolbarItem widget with a text and a Click callback</summary>
        /// <param name="text">The text</param>
        /// <param name="onClicked">The click callback</param>
        static member inline ToolbarItem(text: string, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabToolbarItem>(ToolbarItem.WidgetKey, MenuItem.Text.WithValue(text), MenuItemMvu.Clicked.WithValue(MsgValue(onClicked)))
