namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentVisualElement =
    inherit IFabVisualElement
    inherit IFabComponentNavigableElement

module VisualElementUpdaters =
    // TODO: ValueEventData is Mvu specific
    let updateVisualElementFocus oldValueOpt (newValueOpt: MvuValueEventData<bool, bool> voption) (node: IViewNode) =
        let target = node.Target :?> VisualElement

        let onEventName = "Focus_On"
        let offEventName = "Focus_Off"

        match newValueOpt with
        | ValueNone ->
            // The attribute is no longer applied, so we clean up the events
            match node.TryGetHandler(onEventName) with
            | ValueNone -> ()
            | ValueSome handler -> handler.Dispose()

            match node.TryGetHandler(offEventName) with
            | ValueNone -> ()
            | ValueSome handler -> handler.Dispose()

            // Only clear the property if a value was set before
            match oldValueOpt with
            | ValueNone -> ()
            | ValueSome _ ->
                if target.IsFocused then
                    target.Unfocus()

        | ValueSome curr ->
            // Clean up the old event handlers if any
            match node.TryGetHandler(onEventName) with
            | ValueNone -> ()
            | ValueSome handler -> handler.Dispose()

            match node.TryGetHandler(offEventName) with
            | ValueNone -> ()
            | ValueSome handler -> handler.Dispose()

            // Set the new value
            if target.IsFocused <> curr.Value then
                if curr.Value then
                    target.Focus() |> ignore
                else
                    target.Unfocus()

            // Set the new event handlers
            let onHandler =
                target.Focused.Subscribe(fun _args ->
                    let (MsgValue r) = curr.Event true
                    Dispatcher.dispatch node r)

            node.SetHandler(onEventName, onHandler)

            let offHandler =
                target.Unfocused.Subscribe(fun _args ->
                    let (MsgValue r) = curr.Event false
                    Dispatcher.dispatch node r)

            node.SetHandler(offEventName, offHandler)

module VisualElement =
    let FocusWithEvent =
        Attributes.defineSimpleScalar "VisualElement_FocusWithEvent" ScalarAttributeComparers.noCompare VisualElementUpdaters.updateVisualElementFocus

[<Extension>]
type VisualElementModifiers =
    /// <summary>Set the current focus state of the widget, and listen to focus state changes</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The focus state to apply</param>
    /// <param name="onFocusChanged">Message to dispatch when the widget's focus state changes</param>
    [<Extension>]
    static member inline focus(this: WidgetBuilder<'msg, #IFabComponentVisualElement>, value: bool, onFocusChanged: bool -> unit) =
        this.AddScalar(VisualElement.FocusWithEvent.WithValue(MvuValueEventData.create value onFocusChanged))
