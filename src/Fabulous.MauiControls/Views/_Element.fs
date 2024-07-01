namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabElement =
    interface
    end

module Element =
    let AutomationId =
        Attributes.defineBindableWithEquality<string> Element.AutomationIdProperty

[<Extension>]
type ElementModifiers =
    /// <summary>Sets a value that allows the automation framework to find and interact with this element</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">A value that the automation framework can use to find and interact with this element.</param>
    [<Extension>]
    static member inline automationId(this: WidgetBuilder<'msg, #IFabElement>, value: string) =
        this.AddScalar(Element.AutomationId.WithValue(value))
