namespace Gallery

open Fabulous.Maui
open Fabulous.Maui.Mvu

open type Fabulous.Maui.View
open type Fabulous.Maui.Mvu.View

module Overview =
    let view (push: int -> 'msg) =
        ScrollView(
            (VStack(spacing = 16.) {
                Label("Fabulous Gallery").style(Styles.title)

                Label(".NET MAUI").style(Styles.subtitle)

                VStack(spacing = 10.) {
                    Label("A collection of code samples for Fabulous.Maui")
                    Label("Available on iOS & Android")

                    for i = 0 to RegisteredSamples.samples.Length - 1 do
                        let sample = RegisteredSamples.samples[i]
                        Button(sample.Name, push i)
                }
            })
                .margin(20.)
        )
