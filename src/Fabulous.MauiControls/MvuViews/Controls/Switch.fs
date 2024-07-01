namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuSwitch =
    inherit IFabMvuView
    inherit IFabSwitch

module Switch =
    let IsToggledWithEvent =
        MvuAttributes.defineBindableWithEvent "Switch_Toggled" Switch.IsToggledProperty (fun target -> (target :?> Switch).Toggled)

[<AutoOpen>]
module SwitchBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a Switch widget with a toggle state and listen for toggle state changes</summary>
        /// <param name="isToggled">The toggle state</param>
        /// <param name="onToggled">Message to dispatch</param>
        static member inline Switch<'msg>(isToggled: bool, onToggled: bool -> 'msg) =
            WidgetBuilder<'msg, IFabMvuSwitch>(
                Switch.WidgetKey,
                Switch.IsToggledWithEvent.WithValue(MvuValueEventData.create isToggled (fun (args: ToggledEventArgs) -> onToggled args.Value))
            )

[<Extension>]
type SwitchModifiers =
    /// <summary>Link a ViewRef to access the direct Switch control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuSwitch>, value: ViewRef<Switch>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
