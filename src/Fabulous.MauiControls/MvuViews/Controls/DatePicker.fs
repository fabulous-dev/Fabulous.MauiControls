namespace Fabulous.Maui.Mvu

open System
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuDatePicker =
    inherit IFabMvuView
    inherit IFabDatePicker

module DatePicker =
    let DateWithEvent =
        let name = "DatePicker_DateSelected"
        let minProperty = DatePicker.MinimumDateProperty
        let valueProperty = DatePicker.DateProperty
        let maxProperty = DatePicker.MaximumDateProperty

        let key =
            ScalarAttributeDefinitions.SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun oldValueOpt (newValueOpt: MvuValueEventData<struct (DateTime * DateTime * DateTime), DateChangedEventArgs> voption) node ->
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
        : ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<MvuValueEventData<struct (DateTime * DateTime * DateTime), DateChangedEventArgs>>

[<AutoOpen>]
module DatePickerBuilders =
    type Fabulous.Maui.Mvu.View with

        /// <summary>Create a DatePicker widget with a date, min-max bounds and listen for the date changes</summary>
        /// <param name="min">The minimum date allowed</param>
        /// <param name="max">The maximum date allowed</param>
        /// <param name="date">The selected date</param>
        /// <param name="onDateSelected">Message to dispatch</param>
        static member inline DatePicker<'msg>(min: DateTime, max: DateTime, date: DateTime, onDateSelected: DateTime -> 'msg) =
            WidgetBuilder<'msg, IFabMvuDatePicker>(
                DatePicker.WidgetKey,
                DatePicker.DateWithEvent.WithValue(
                    MvuValueEventData.create (struct (min, max, date)) (fun (args: DateChangedEventArgs) -> onDateSelected args.NewDate)
                )
            )

[<Extension>]
type DatePickerModifiers =
    /// <summary>Link a ViewRef to access the direct DatePicker control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuDatePicker>, value: ViewRef<DatePicker>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
