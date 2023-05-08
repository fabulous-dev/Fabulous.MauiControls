namespace NavigationSample.NavigationPath

open Fabulous
open Fabulous.Maui

open type Fabulous.Maui.View

module PageC =
    type Model =
        { Args: string
          StepCount: int
          Count: int }
        
    type Msg =
        | Increment
        | Decrement
        | GoToPageA
        | GoToPageB
        
    let init args stepCount =
        { Args = args
          StepCount = stepCount 
          Count = 0 }

    let update (nav: NavigationController) msg model =
        match msg with
        | Increment -> { model with Count = model.Count + model.StepCount }, Cmd.none
        | Decrement -> { model with Count = model.Count - model.StepCount }, Cmd.none
        | GoToPageA -> model, Navigation.navigateToPageB nav model.Count
        | GoToPageB -> model, Navigation.navigateToPageC nav "Hello from Page A!" model.Count
        
    let view model =
        ContentPage(
            Grid(coldefs = [Star], rowdefs = [Auto; Star; Auto]) {
                Label("Page C")
                    .font(32.)
                    .centerTextHorizontal()
                    .margin(0., 0., 0., 30.)
                
                (VStack() {
                    Label($"Args: {model.Args}")
                    Label($"StepCount: {model.StepCount}")
                    
                    Label($"Count: {model.Count}")
                        .centerTextHorizontal()
                        
                    Button("Increment", Increment)
                    Button("Decrement", Decrement)
                })
                    .gridRow(1)
                    
                (VStack() {
                    Button("Go to Page A", GoToPageA)
                    Button("Go to Page B", GoToPageB)
                })
                    .gridRow(2)
            }
        )