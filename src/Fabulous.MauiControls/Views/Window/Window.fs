namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Microsoft.Maui
open Microsoft.Maui.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Maui

type IFabWindow =
    inherit IFabNavigableElement

module Window =
    let WidgetKey = Widgets.register<Window>()

    let Page = Attributes.defineBindableWidget Window.PageProperty

    let FlowDirection =
        Attributes.defineBindableWithEquality Window.FlowDirectionProperty

    let Height = Attributes.defineBindableWithEquality Window.HeightProperty

    let MaximumHeight =
        Attributes.defineBindableWithEquality Window.MaximumHeightProperty

    let MaximumWidth = Attributes.defineBindableWithEquality Window.MaximumWidthProperty

    let MinimumHeight =
        Attributes.defineBindableWithEquality Window.MinimumHeightProperty

    let MinimumWidth = Attributes.defineBindableWithEquality Window.MinimumWidthProperty

    let Title = Attributes.defineBindableWithEquality Window.TitleProperty

    let Width = Attributes.defineBindableWithEquality Window.WidthProperty

    let X = Attributes.defineBindableWithEquality Window.XProperty

    let Y = Attributes.defineBindableWithEquality Window.YProperty

[<AutoOpen>]
module WindowBuilders =
    type Fabulous.Maui.View with

        static member inline Window(content: WidgetBuilder<'msg, #IFabPage>) =
            WidgetBuilder<'msg, IFabWindow>(Window.WidgetKey, Window.Page.WithValue(content.Compile()))

[<Extension>]
type WindowModifiers =
    [<Extension>]
    static member inline flowDirection(this: WidgetBuilder<'msg, #IFabWindow>, value: FlowDirection) =
        this.AddScalar(Window.FlowDirection.WithValue(value))

    [<Extension>]
    static member inline height(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.Height.WithValue(value))

    [<Extension>]
    static member inline maximumHeight(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MaximumHeight.WithValue(value))

    [<Extension>]
    static member inline maximumWidth(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MaximumWidth.WithValue(value))

    [<Extension>]
    static member inline minimumHeight(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MinimumHeight.WithValue(value))

    [<Extension>]
    static member inline minimumWidth(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.MinimumWidth.WithValue(value))

    [<Extension>]
    static member inline title(this: WidgetBuilder<'msg, #IFabWindow>, value: string) =
        this.AddScalar(Window.Title.WithValue(value))

    [<Extension>]
    static member inline width(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.Width.WithValue(value))

    [<Extension>]
    static member inline x(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.X.WithValue(value))

    [<Extension>]
    static member inline y(this: WidgetBuilder<'msg, #IFabWindow>, value: double) =
        this.AddScalar(Window.Y.WithValue(value))
