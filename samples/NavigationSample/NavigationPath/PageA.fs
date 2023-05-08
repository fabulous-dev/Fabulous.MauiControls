namespace NavigationSample.NavigationPath

open Fabulous
open Fabulous.Maui

open type Fabulous.Maui.View

module PageA =
    type Model =
        { Count: int }
        
    type Msg =
        | Increment
        | Decrement
        | GoToPageB
        | GoToPageC
        | BackButtonPressed
        
    let init () = { Count = 0 }

    let update (nav: NavigationController) msg model =
        match msg with
        | Increment -> { model with Count = model.Count + 1 }, Cmd.none
        | Decrement -> { model with Count = model.Count - 1 }, Cmd.none
        | GoToPageB -> model, Navigation.navigateToPageB nav model.Count
        | GoToPageC -> model, Navigation.navigateToPageC nav "Hello from Page A!" model.Count
        | BackButtonPressed -> { model with Count = model.Count - 1 }, Cmd.none
        
    let view model =
        ContentPage(
            Grid(coldefs = [Star], rowdefs = [Auto; Star; Auto]) {
                Label("Page A")
                    .font(32.)
                    .centerTextHorizontal()
                    .margin(0., 0., 0., 30.)
                
                (VStack() {                    
                    Label($"Count: {model.Count}")
                        .centerTextHorizontal()
                        
                    Button("Increment", Increment)
                    Button("Decrement", Decrement)
                })
                    .gridRow(1)
                    
                (VStack() {
                    Button("Go to Page B", GoToPageB)
                    Button("Go to Page C", GoToPageC)
                })
                    .gridRow(2)
            }
        )