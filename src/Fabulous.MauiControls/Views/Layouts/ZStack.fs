namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabZStack =
    inherit IFabLayoutOfView

module ZStack =
    // Remark: ZStack control is not available in Maui, so we use a Grid control instead to simulate the ZStack behavior
    let WidgetKey = Widgets.register<Grid>()

[<AutoOpen>]
module ZStackBuilders =
    type Fabulous.Maui.View with

        static member inline ZStack() =
            CollectionBuilder<'msg, IFabZStack, IFabView>(ZStack.WidgetKey, LayoutOfView.Children)

[<Extension>]
type ZStackModifiers =
    /// <summary>Link a ViewRef to access the direct Grid control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabZStack>, value: ViewRef<Grid>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
