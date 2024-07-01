namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentStepper =
    inherit IFabComponentView
    inherit IFabStepper

module Stepper =
    let ValueWithEvent =
        ComponentAttributes.defineBindableWithEvent "Stepper_ValueChanged" Stepper.ValueProperty (fun target -> (target :?> Stepper).ValueChanged)

[<AutoOpen>]
module StepperBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a Stepper widget with min and max values, a current value, and listen for value changes</summary>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <param name="value">The current value</param>
        /// <param name="onValueChanged">Message to dispatch</param>
        static member inline Stepper<'msg>(min: float, max: float, value: float, onValueChanged: float -> unit) =
            WidgetBuilder<'msg, IFabComponentStepper>(
                Stepper.WidgetKey,
                Stepper.MinimumMaximum.WithValue(struct (min, max)),
                Stepper.ValueWithEvent.WithValue(ComponentValueEventData.create value (fun (args: ValueChangedEventArgs) -> onValueChanged args.NewValue))
            )

[<Extension>]
type StepperModifiers =
    /// <summary>Link a ViewRef to access the direct Stepper control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentStepper>, value: ViewRef<Stepper>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
