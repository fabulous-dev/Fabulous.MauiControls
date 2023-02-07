namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous

[<Extension>]
type FabElementExtensions =
    [<Extension>]
    static member inline style(this: WidgetBuilder<'msg, #IFabElement>, fn: WidgetBuilder<'msg, #IFabElement> -> WidgetBuilder<'msg, #IFabElement>) =
        fn this
    