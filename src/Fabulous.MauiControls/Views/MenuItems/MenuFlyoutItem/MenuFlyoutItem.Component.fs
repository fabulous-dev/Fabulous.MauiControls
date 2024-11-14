namespace Fabulous.Maui

open Fabulous

[<AutoOpen>]
module MenuFlyoutItemComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a MenuItem widget with a text and a Click callback</summary>
        /// <param name="text">The text</param>
        /// <param name="onClicked">The click callback</param>
        static member inline MenuFlyoutItem(text: string, onClicked: unit -> unit) =
            WidgetBuilder<'msg, IFabMenuFlyoutItem>(MenuItem.WidgetKey, MenuItem.Text.WithValue(text), MenuItemComponent.Clicked.WithValue(onClicked))
