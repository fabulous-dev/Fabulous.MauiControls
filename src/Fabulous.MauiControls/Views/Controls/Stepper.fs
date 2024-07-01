namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabStepper =
    inherit IFabView

module StepperUpdaters =
    let updateStepperMinMax _ (newValueOpt: struct (float * float) voption) (node: IViewNode) =
        let stepper = node.Target :?> Stepper

        match newValueOpt with
        | ValueNone ->
            stepper.ClearValue(Stepper.MinimumProperty)
            stepper.ClearValue(Stepper.MaximumProperty)
        | ValueSome(min, max) ->
            let currMax = stepper.GetValue(Stepper.MaximumProperty) :?> float

            if min > currMax then
                stepper.SetValue(Stepper.MaximumProperty, max)
                stepper.SetValue(Stepper.MinimumProperty, min)
            else
                stepper.SetValue(Stepper.MinimumProperty, min)
                stepper.SetValue(Stepper.MaximumProperty, max)

module Stepper =
    let WidgetKey = Widgets.register<Stepper>()

    let Increment = Attributes.defineBindableFloat Stepper.IncrementProperty

    let MinimumMaximum =
        Attributes.defineSimpleScalarWithEquality<struct (float * float)> "Stepper_MinimumMaximum" StepperUpdaters.updateStepperMinMax

[<Extension>]
type StepperModifiers =
    /// <summary>Set the increment step</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The increment step</param>
    [<Extension>]
    static member inline increment(this: WidgetBuilder<'msg, #IFabStepper>, value: float) =
        this.AddScalar(Stepper.Increment.WithValue(value))
