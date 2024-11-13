namespace Fabulous.Maui

open System
open Fabulous
open Microsoft.Maui.Controls

module TimePickerMvu =
    let TimeWithEvent =
        Attributes.Mvu.defineBindableWithEvent "TimePickerMvu_TimeSelected" TimePicker.TimeProperty (fun target -> (target :?> FabTimePicker).TimeSelected)

[<AutoOpen>]
module TimePickerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a TimePicker widget with a selected time and listen for the selected time changes</summary>
        /// <param name="time">The selected time</param>
        /// <param name="onTimeSelected">Message to dispatch</param>
        static member inline TimePicker(time: TimeSpan, onTimeSelected: TimeSpan -> 'msg) =
            WidgetBuilder<'msg, IFabTimePicker>(
                TimePicker.WidgetKey,
                TimePickerMvu.TimeWithEvent.WithValue(MsgValueEventData.create time (fun (args: TimeSelectedEventArgs) -> onTimeSelected args.NewTime))
            )
