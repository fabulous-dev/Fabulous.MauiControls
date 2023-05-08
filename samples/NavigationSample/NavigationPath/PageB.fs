namespace NavigationSample.NavigationPath

open Fabulous
open Fabulous.Maui

open type Fabulous.Maui.View

module PageB =
    type Model =
        { InitialCount: int
          Count: int }
        
    type Msg =
        | Increment
        | Decrement
        | GoToPageA
        | GoToPageC
        
    let init initialCount =
        { InitialCount = initialCount
          Count = initialCount }

    let update (nav: NavigationController) msg model =
        match msg with
        | Increment -> { model with Count = model.Count + 1 }, Cmd.none
        | Decrement -> { model with Count = model.Count - 1 }, Cmd.none
        | GoToPageA -> model, Navigation.navigateToPageB nav model.Count
        | GoToPageC -> model, Navigation.navigateToPageC nav "Hello from Page A!" model.Count
        
    let view model =
        ContentPage(
            Grid(coldefs = [Star], rowdefs = [Auto; Star; Auto]) {
                Label("Page B")
                    .font(32.)
                    .centerTextHorizontal()
                    .margin(0., 0., 0., 30.)
                
                (VStack() {
                    Label($"Initial count: {model.InitialCount}")
                    
                    Label($"Count: {model.Count}")
                        .centerTextHorizontal()
                        
                    Button("Increment", Increment)
                    Button("Decrement", Decrement)
                })
                    .gridRow(1)
                    
                (VStack() {
                    Button("Go to Page A", GoToPageA)
                    Button("Go to Page C", GoToPageC)
                })
                    .gridRow(2)
            }
        )