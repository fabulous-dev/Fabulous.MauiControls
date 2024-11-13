namespace Fabulous.Maui

open System
open Fabulous
open Microsoft.Maui.Controls

module TimePickerComponent =
    let TimeWithEvent =
        Attributes.Component.defineBindableWithEvent "TimePickerComponent_TimeSelected" TimePicker.TimeProperty (fun target -> (target :?> FabTimePicker).TimeSelected)

[<AutoOpen>]
module TimePickerComponentBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a TimePicker widget with a selected time and listen for the selected time changes</summary>
        /// <param name="time">The selected time</param>
        /// <param name="onTimeSelected">Message to dispatch</param>
        static member inline TimePicker(time: TimeSpan, onTimeSelected: TimeSpan -> unit) =
            WidgetBuilder<unit, IFabTimePicker>(
                TimePicker.WidgetKey,
                TimePickerComponent.TimeWithEvent.WithValue(ValueEventData.create time (fun (args: TimeSelectedEventArgs) -> onTimeSelected args.NewTime))
            )
