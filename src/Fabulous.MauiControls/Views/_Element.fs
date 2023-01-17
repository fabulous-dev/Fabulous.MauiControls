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
    /// Sets a value that allows the automation framework to find and interact with this element.
    [<Extension>]
    static member inline automationId(this: WidgetBuilder<'msg, #IFabElement>, value: string) =
        this.AddScalar(Element.AutomationId.WithValue(value))

    [<Extension>]
    static member inline onMounted(this: WidgetBuilder<'msg, #IFabElement>, value: 'msg) =
        this.AddScalar(Lifecycle.Mounted.WithValue(value))

    [<Extension>]
    static member inline onUnmounted(this: WidgetBuilder<'msg, #IFabElement>, value: 'msg) =
        this.AddScalar(Lifecycle.Unmounted.WithValue(value))
