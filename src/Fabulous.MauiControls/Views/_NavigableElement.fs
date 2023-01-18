namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabNavigableElement =
    inherit IFabElement

module NavigableElement =
    let Style =
        Attributes.defineBindableWithEquality<Style> NavigableElement.StyleProperty

[<Extension>]
type NavigableElementModifiers =

    [<Extension>]
    static member inline style(this: WidgetBuilder<'msg, #IFabNavigableElement>, style: Style) =
        this.AddScalar(NavigableElement.Style.WithValue(style))
