namespace Fabulous.Maui.Components

open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabComponentInputView =
    inherit IFabComponentView
    inherit IFabInputView

module InputView =
    let TextWithEvent =
        ComponentAttributes.defineBindableWithEvent<string, TextChangedEventArgs> "InputView_TextChanged" InputView.TextProperty (fun target ->
            (target :?> InputView).TextChanged)
