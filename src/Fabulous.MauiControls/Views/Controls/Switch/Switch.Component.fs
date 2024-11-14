namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module SwitchComponent =
    let IsToggledWithEvent =
        Attributes.Component.defineBindableWithEvent "SwitchComponent_Toggled" Switch.IsToggledProperty (fun target -> (target :?> Switch).Toggled)

[<AutoOpen>]
module SwitchComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Switch widget with a toggle state and listen for toggle state changes</summary>
        /// <param name="isToggled">The toggle state</param>
        /// <param name="onToggled">Message to dispatch</param>
        static member inline Switch(isToggled: bool, onToggled: bool -> unit) =
            WidgetBuilder<unit, IFabSwitch>(
                Switch.WidgetKey,
                SwitchComponent.IsToggledWithEvent.WithValue(ValueEventData.create isToggled (fun (args: ToggledEventArgs) -> onToggled args.Value))
            )
