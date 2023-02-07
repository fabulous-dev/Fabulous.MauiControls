namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabView =
    inherit IFabVisualElement

module MauiView =
    let HorizontalOptions =
        Attributes.defineSmallBindable<LayoutOptions> View.HorizontalOptionsProperty SmallScalars.LayoutOptions.decode

    let VerticalOptions =
        Attributes.defineSmallBindable<LayoutOptions> View.VerticalOptionsProperty SmallScalars.LayoutOptions.decode

    let Margin = Attributes.defineBindableWithEquality<Thickness> View.MarginProperty

    let GestureRecognizers =
        Attributes.defineListWidgetCollection<IGestureRecognizer> "View_GestureRecognizers" (fun target -> (target :?> View).GestureRecognizers)

[<Extension>]
type ViewModifiers =
    [<Extension>]
    static member inline horizontalOptions(this: WidgetBuilder<'msg, #IFabView>, value: LayoutOptions) =
        this.AddScalar(MauiView.HorizontalOptions.WithValue(value))

    [<Extension>]
    static member inline verticalOptions(this: WidgetBuilder<'msg, #IFabView>, value: LayoutOptions) =
        this.AddScalar(MauiView.VerticalOptions.WithValue(value))

    [<Extension>]
    static member inline center(this: WidgetBuilder<'msg, #IFabView>) =
        this
            .AddScalar(MauiView.HorizontalOptions.WithValue(LayoutOptions.Center))
            .AddScalar(MauiView.VerticalOptions.WithValue(LayoutOptions.Center))

    [<Extension>]
    static member inline centerHorizontal(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.HorizontalOptions.WithValue(LayoutOptions.Center))

    [<Extension>]
    static member inline centerVertical(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.VerticalOptions.WithValue(LayoutOptions.Center))

    [<Extension>]
    static member inline alignStartHorizontal(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.HorizontalOptions.WithValue(LayoutOptions.Start))

    [<Extension>]
    static member inline alignStartVertical(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.VerticalOptions.WithValue(LayoutOptions.Start))

    [<Extension>]
    static member inline alignEndHorizontal(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.HorizontalOptions.WithValue(LayoutOptions.End))

    [<Extension>]
    static member inline alignEndVertical(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.VerticalOptions.WithValue(LayoutOptions.End))

    [<Extension>]
    static member inline fillHorizontal(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.HorizontalOptions.WithValue(LayoutOptions.Fill))

    [<Extension>]
    static member inline fillVertical(this: WidgetBuilder<'msg, #IFabView>) =
        this.AddScalar(MauiView.VerticalOptions.WithValue(LayoutOptions.Fill))

    [<Extension>]
    static member inline margin(this: WidgetBuilder<'msg, #IFabView>, value: Thickness) =
        this.AddScalar(MauiView.Margin.WithValue(value))

    [<Extension>]
    static member inline margin(this: WidgetBuilder<'msg, #IFabView>, value: float) =
        ViewModifiers.margin(this, Thickness(value))

    [<Extension>]
    static member inline margin(this: WidgetBuilder<'msg, #IFabView>, left: float, top: float, right: float, bottom: float) =
        ViewModifiers.margin(this, Thickness(left, top, right, bottom))

    [<Extension>]
    static member inline gestureRecognizers<'msg, 'marker when 'marker :> IFabView>(this: WidgetBuilder<'msg, 'marker>) =
        WidgetHelpers.buildAttributeCollection<'msg, 'marker, IFabGestureRecognizer> MauiView.GestureRecognizers this


[<Extension>]
type ViewYieldExtensions =
    [<Extension>]
    static member inline Yield
        (
            _: AttributeCollectionBuilder<'msg, #IFabView, IFabGestureRecognizer>,
            x: WidgetBuilder<'msg, #IFabGestureRecognizer>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: AttributeCollectionBuilder<'msg, #IFabView, IFabGestureRecognizer>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabGestureRecognizer>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
