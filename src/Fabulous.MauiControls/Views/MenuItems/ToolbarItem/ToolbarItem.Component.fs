namespace Fabulous.Maui

open Fabulous

[<AutoOpen>]
module ToolbarItemComponentBuilders =
    type Fabulous.Maui.View with
    
        /// <summary>Create a ToolbarItem widget with a text and a Click callback</summary>
        /// <param name="text">The text</param>
        /// <param name="onClicked">The click callback</param>
        static member inline ToolbarItem(text: string, onClicked: unit -> unit) =
            WidgetBuilder<unit, IFabToolbarItem>(ToolbarItem.WidgetKey, MenuItem.Text.WithValue(text), MenuItemComponent.Clicked.WithValue(onClicked))
