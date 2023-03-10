namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous

[<Extension>]
type FabElementExtensions =
    /// <summary>Apply a style modifier to a widget</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">The style modifier function</param>
    [<Extension>]
    static member inline style<'msg, 'marker when 'marker :> IFabElement>
        (
            this: WidgetBuilder<'msg, 'marker>,
            fn: WidgetBuilder<'msg, 'marker> -> WidgetBuilder<'msg, 'marker>
        ) =
        fn this
