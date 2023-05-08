namespace NavigationSample.BasicNavigation

open Fabulous.Maui

open type Fabulous.Maui.View

module PageB =
    type Model =
        { Count: int }
        
    type Msg =
        | Increment
        | Decrement
        
    let init () = { Count = 0 }

    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + 1 }
        | Decrement -> { model with Count = model.Count - 1 }
        
    let view model =
        VStack() {
            Label("Page B")
                .font(32.)
                .centerTextHorizontal()
                .margin(0., 0., 0., 30.)
                
            Label($"Count: {model.Count}")
                .centerTextHorizontal()
                
            Button("Increment", Increment)
            Button("Decrement", Decrement)
        }