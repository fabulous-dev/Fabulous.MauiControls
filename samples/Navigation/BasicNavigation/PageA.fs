namespace NavigationSample

open Fabulous.Maui
open Fabulous.Maui.Mvu

open type Fabulous.Maui.View

module PageA =
    type Model = { Count: int }

    type Msg =
        | Increment
        | Decrement

    let init () = { Count = 0 }

    let update msg model =
        match msg with
        | Increment -> { Count = model.Count + 1 }
        | Decrement -> { Count = model.Count - 1 }

    let view model =
        VStack() {
            Label("Page A").font(32.).centerTextHorizontal().margin(0., 0., 0., 30.)

            Label($"Count: {model.Count}").centerTextHorizontal()

            Button("Increment", Increment)
            Button("Decrement", Decrement)
        }
