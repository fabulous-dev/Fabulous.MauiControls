namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module StepperMvu =
    let ValueWithEvent =
        Attributes.Mvu.defineBindableWithEvent "StepperMvu_ValueChanged" Stepper.ValueProperty (fun target -> (target :?> Stepper).ValueChanged)

[<AutoOpen>]
module StepperMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a Stepper widget with min and max values, a current value, and listen for value changes</summary>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Stepper(min: float, max: float, value: float, onValueChanged: float -> 'msg) =
            WidgetBuilder<'msg, IFabStepper>(
                Stepper.WidgetKey,
                Stepper.MinimumMaximum.WithValue(struct (min, max)),
                StepperMvu.ValueWithEvent.WithValue(MsgValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )
            