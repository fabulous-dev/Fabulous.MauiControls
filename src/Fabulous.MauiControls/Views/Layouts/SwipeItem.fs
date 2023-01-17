namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabSwipeItem =
    inherit IFabMenuItem

module SwipeItem =

    let WidgetKey = Widgets.register<SwipeItem>()

    let BackgroundColor =
        Attributes.defineBindableAppThemeColor SwipeItem.BackgroundColorProperty

    let IsVisible = Attributes.defineBindableBool SwipeItem.IsVisibleProperty

    let Clicked =
        Attributes.defineEvent "SwipeItem_Invoked" (fun target -> (target :?> SwipeItem).Invoked)

[<AutoOpen>]
module SwipeItemBuilders =

    type Fabulous.Maui.View with

        static member inline SwipeItem<'msg>(onInvoked: 'msg) =
            WidgetBuilder<'msg, IFabSwipeItem>(SwipeItem.WidgetKey, SwipeItem.Clicked.WithValue(fun _ -> onInvoked |> box))

[<Extension>]
type SwipeItemModifiers() =

    [<Extension>]
    static member inline backgroundColor(this: WidgetBuilder<'msg, #IFabSwipeItem>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(SwipeItem.BackgroundColor.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline isVisible(this: WidgetBuilder<'msg, #IFabSwipeItem>, value: bool) =
        this.AddScalar(SwipeItem.IsVisible.WithValue(value))

    [<Extension>]
    static member inline text(this: WidgetBuilder<'msg, #IFabSwipeItem>, value: string) =
        this.AddScalar(MenuItem.Text.WithValue(value))

    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSwipeItem>, value: ViewRef<SwipeItem>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
