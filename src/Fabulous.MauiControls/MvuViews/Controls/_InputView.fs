namespace Fabulous.Maui.Mvu

open Fabulous.Maui
open Microsoft.Maui.Controls

type IFabMvuInputView =
    inherit IFabMvuView
    inherit IFabInputView

module InputView =
    let TextWithEvent =
        MvuAttributes.defineBindableWithEvent<string, TextChangedEventArgs> "InputView_TextChanged" InputView.TextProperty (fun target ->
            (target :?> InputView).TextChanged)
