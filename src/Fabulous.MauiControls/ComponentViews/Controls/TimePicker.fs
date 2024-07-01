namespace Fabulous.Maui.Components

open System
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentTimePicker =
    inherit IFabComponentView
    inherit IFabTimePicker

module TimePicker =
    let TimeWithEvent =
        ComponentAttributes.defineBindableWithEvent "TimePicker_TimeSelected" TimePicker.TimeProperty (fun target -> (target :?> FabTimePicker).TimeSelected)

[<AutoOpen>]
module TimePickerBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a TimePicker widget with a selected time and listen for the selected time changes</summary>
        /// <param name="time">The selected time</param>
        /// <param name="onTimeSelected">Message to dispatch</param>
        static member inline TimePicker<'msg>(time: TimeSpan, onTimeSelected: TimeSpan -> unit) =
            WidgetBuilder<'msg, IFabComponentTimePicker>(
                TimePicker.WidgetKey,
                TimePicker.TimeWithEvent.WithValue(ComponentValueEventData.create time (fun (args: TimeSelectedEventArgs) -> onTimeSelected args.NewTime))
            )

[<Extension>]
type TimePickerModifiers =
    /// <summary>Link a ViewRef to access the direct TimePicker control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentTimePicker>, value: ViewRef<TimePicker>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
