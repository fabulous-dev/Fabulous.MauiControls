namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentSwitch =
    inherit IFabComponentView
    inherit IFabSwitch

module Switch =
    let IsToggledWithEvent =
        ComponentAttributes.defineBindableWithEvent "Switch_Toggled" Switch.IsToggledProperty (fun target -> (target :?> Switch).Toggled)

[<AutoOpen>]
module SwitchBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a Switch widget with a toggle state and listen for toggle state changes</summary>
        /// <param name="isToggled">The toggle state</param>
        /// <param name="onToggled">Message to dispatch</param>
        static member inline Switch<'msg>(isToggled: bool, onToggled: bool -> unit) =
            WidgetBuilder<'msg, IFabComponentSwitch>(
                Switch.WidgetKey,
                Switch.IsToggledWithEvent.WithValue(ComponentValueEventData.create isToggled (fun (args: ToggledEventArgs) -> onToggled args.Value))
            )

[<Extension>]
type SwitchModifiers =
    /// <summary>Link a ViewRef to access the direct Switch control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentSwitch>, value: ViewRef<Switch>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
