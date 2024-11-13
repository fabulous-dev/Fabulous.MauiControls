namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module SwitchCellComponent =
    let OnWithEvent =
        Attributes.Component.defineBindableWithEvent "SwitchCellComponent_OnChanged" SwitchCell.OnProperty (fun target -> (target :?> SwitchCell).OnChanged)

[<AutoOpen>]
module SwitchCellComponentBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a SwitchCell with a text, a toggle state, and listen to toggle state changes</summary>
        /// <param name="text">The text value</param>
        /// <param name="value">The toggle state value</param>
        /// <param name="onChanged">Change callback</param>
        static member inline SwitchCell(text: string, value: bool, onChanged: bool -> unit) =
            WidgetBuilder<'msg, IFabSwitchCell>(
                SwitchCell.WidgetKey,
                SwitchCellComponent.OnWithEvent.WithValue(ValueEventData.create value (fun (args: ToggledEventArgs) -> onChanged args.Value)),
                SwitchCell.Text.WithValue(text)
            )