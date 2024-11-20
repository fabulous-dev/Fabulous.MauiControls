namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

type IFabHybridWebView =
    inherit IFabView

module HybridWebView =
    let WidgetKey = Widgets.register<HybridWebView>()

    let DefaultFile =
        Attributes.defineBindableWithEquality<string> HybridWebView.DefaultFileProperty

    let HybridRoot =
        Attributes.defineBindableWithEquality<string> HybridWebView.HybridRootProperty

[<AutoOpen>]
module HybridWebViewBuilders =
    type Fabulous.Maui.View with
        static member inline HybridWebView() =
            WidgetBuilder<'msg, IFabHybridWebView>(HybridWebView.WidgetKey)

[<Extension>]
type HybridWebViewModifiers =
    [<Extension>]
    static member inline defaultFile(this: WidgetBuilder<'msg, #IFabHybridWebView>, value: string) =
        this.AddScalar(HybridWebView.DefaultFile.WithValue(value))

    [<Extension>]
    static member inline hybridRoot(this: WidgetBuilder<'msg, #IFabHybridWebView>, value: string) =
        this.AddScalar(HybridWebView.HybridRoot.WithValue(value))
