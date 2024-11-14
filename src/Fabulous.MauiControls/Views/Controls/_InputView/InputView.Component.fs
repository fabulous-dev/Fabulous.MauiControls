namespace Fabulous.Maui

open Microsoft.Maui.Controls

module InputViewComponent =
    let TextWithEvent =
        Attributes.Component.defineBindableWithEvent<string, TextChangedEventArgs> "InputViewComponent_TextChanged" InputView.TextProperty (fun target ->
            (target :?> InputView).TextChanged)
