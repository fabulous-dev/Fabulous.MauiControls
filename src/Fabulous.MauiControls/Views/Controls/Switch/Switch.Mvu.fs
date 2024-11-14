namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module SwitchMvu =
    let IsToggledWithEvent =
        Attributes.Mvu.defineBindableWithEvent "SwitchMvu_Toggled" Switch.IsToggledProperty (fun target -> (target :?> Switch).Toggled)

[<AutoOpen>]
module SwitchMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Switch widget with a toggle state and listen for toggle state changes</summary>
        /// <param name="isToggled">The toggle state</param>
        /// <param name="onToggled">Message to dispatch</param>
        static member inline Switch(isToggled: bool, onToggled: bool -> 'msg) =
            WidgetBuilder<'msg, IFabSwitch>(
                Switch.WidgetKey,
                SwitchMvu.IsToggledWithEvent.WithValue(MsgValueEventData.create isToggled (fun (args: ToggledEventArgs) -> onToggled args.Value))
            )
