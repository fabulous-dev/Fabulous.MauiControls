namespace Fabulous.Maui

open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Microsoft.Maui.Controls
open System

[<Struct>]
type MvuValueEventData<'data, 'eventArgs> =
    { Value: 'data
      Event: 'eventArgs -> MsgValue }

module MvuValueEventData =
    let create (value: 'data) (event: 'eventArgs -> 'msg) =
        { Value = value
          Event = event >> box >> MsgValue }

module MvuAttributes =

    /// Update both a property and its related event.
    /// This definition makes sure that the event is only raised when the property is changed by the user,
    /// and not when the property is set by the code
    let defineBindableWithEvent<'data, 'args>
        name
        (bindableProperty: BindableProperty)
        (getEvent: obj -> IEvent<EventHandler<'args>, 'args>)
        : SimpleScalarAttributeDefinition<MvuValueEventData<'data, 'args>> =

        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun oldValueOpt (newValueOpt: MvuValueEventData<'data, 'args> voption) node ->
                    let target = node.Target :?> BindableObject

                    match newValueOpt with
                    | ValueNone ->
                        // The attribute is no longer applied, so we clean up the event
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> handler.Dispose()

                        // Only clear the property if a value was set before
                        match oldValueOpt with
                        | ValueNone -> ()
                        | ValueSome _ -> target.ClearValue(bindableProperty)

                    | ValueSome curr ->
                        // Clean up the old event handler if any
                        match node.TryGetHandler(name) with
                        | ValueNone -> ()
                        | ValueSome handler -> handler.Dispose()

                        // Set the new value
                        target.SetValue(bindableProperty, curr.Value)

                        // Set the new event handler
                        let event = getEvent target

                        let handler =
                            event.Subscribe(fun args ->
                                let (MsgValue r) = curr.Event args
                                Dispatcher.dispatch node r)

                        node.SetHandler(name, handler))
            )
            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }
