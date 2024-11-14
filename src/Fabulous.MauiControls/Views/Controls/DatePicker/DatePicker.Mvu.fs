namespace Fabulous.Maui

open System
open Fabulous
open Microsoft.Maui.Controls

module DatePickerMvu =
    let DateWithEvent =
        let name = "DatePickerMvu_DateSelected"
        let minProperty = DatePicker.MinimumDateProperty
        let valueProperty = DatePicker.DateProperty
        let maxProperty = DatePicker.MaximumDateProperty

        let key =
            ScalarAttributeDefinitions.SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun oldValueOpt (newValueOpt: MsgValueEventData<struct (DateTime * DateTime * DateTime), DateChangedEventArgs> voption) node ->
                    let target = node.Target :?> DatePicker

                    match newValueOpt with
                    | ValueNone ->
                        // The attribute is no longer applied, so we clean up the event
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> handler.Dispose()

                        // Only clear the property if a value was set before
                        match oldValueOpt with
                        | ValueNone -> ()
                        | ValueSome _ ->
                            target.ClearValue(minProperty)
                            target.ClearValue(maxProperty)
                            target.ClearValue(valueProperty)

                    | ValueSome curr ->
                        // Clean up the old event handler if any
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> handler.Dispose()

                        // Set the new value
                        let struct (min, max, value) = curr.Value
                        target.SetValue(minProperty, min)
                        target.SetValue(maxProperty, max)
                        target.SetValue(valueProperty, value)

                        // Set the new event handler
                        let handler =
                            target.DateSelected.Subscribe(fun args ->
                                let (MsgValue r) = curr.Event args
                                Dispatcher.dispatch node r)

                        node.SetHandler(name, handler))
            )
            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }
        : ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<MsgValueEventData<struct (DateTime * DateTime * DateTime), DateChangedEventArgs>>

[<AutoOpen>]
module DatePickerMvuBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a DatePicker widget with a date, min-max bounds and listen for the date changes</summary>
        /// <param name="min">The minimum date allowed</param>
        /// <param name="max">The maximum date allowed</param>
        /// <param name="date">The selected date</param>
        /// <param name="onDateSelected">Message to dispatch</param>
        static member inline DatePicker(min: DateTime, max: DateTime, date: DateTime, onDateSelected: DateTime -> 'msg) =
            WidgetBuilder<'msg, IFabDatePicker>(
                DatePicker.WidgetKey,
                DatePickerMvu.DateWithEvent.WithValue(
                    MsgValueEventData.create (struct (min, max, date)) (fun (args: DateChangedEventArgs) -> onDateSelected args.NewDate)
                )
            )
