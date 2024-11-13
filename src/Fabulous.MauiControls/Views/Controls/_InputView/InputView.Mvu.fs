namespace Fabulous.Maui

open Microsoft.Maui.Controls

module InputViewMvu =
    let TextWithEvent =
        Attributes.Mvu.defineBindableWithEvent<string, TextChangedEventArgs> "InputViewMvu_TextChanged" InputView.TextProperty (fun target ->
            (target :?> InputView).TextChanged)
