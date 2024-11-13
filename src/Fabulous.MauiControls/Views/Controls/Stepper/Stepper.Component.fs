namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module StepperComponent =
    let ValueWithEvent =
        Attributes.Component.defineBindableWithEvent "StepperComponent_ValueChanged" Stepper.ValueProperty (fun target -> (target :?> Stepper).ValueChanged)

[<AutoOpen>]
module StepperComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Stepper widget with min and max values, a current value, and listen for value changes</summary>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Stepper(min: float, max: float, value: float, onValueChanged: float -> unit) =
            WidgetBuilder<unit, IFabStepper>(
                Stepper.WidgetKey,
                Stepper.MinimumMaximum.WithValue(struct (min, max)),
                StepperComponent.ValueWithEvent.WithValue(ValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )
