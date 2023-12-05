namespace Playground

open Fabulous.Maui

open type Fabulous.Maui.View

module App =
    type Path =
        | Home
        | Details
        | Subdetails of int
        
    type Model =
        { Stack: Path list }
        
    type Msg =
        | StackUpdated of Path list
        | GoTo of Path
        | GoBack
        | GoToRoot
        
    let init() =
        { Stack = [ Home ] }
        
    let update msg model =
        match msg with
        | StackUpdated stack ->
            { model with Stack = stack }
        | GoTo path ->
            { model with Stack = path :: model.Stack }
        | GoBack ->
            { model with Stack = model.Stack |> List.tail }
        | GoToRoot ->
            { model with Stack = [model.Stack |> List.last] }

    let view model =
        Application(
            NavigationStack(List.rev model.Stack, StackUpdated, fun path ->
                match path with
                | Home ->
                    ContentPage(
                        VStack(spacing = 15.) {
                            Label("Home")
                            Button("Go to Details", GoTo Details)
                        }
                    )
                        .title("Home")
                | Details ->
                    ContentPage(
                        VStack(spacing = 15.) {
                            Label("Details")
                            for i in 1..3 do
                                Button($"Go to Subdetails {i}", GoTo (Subdetails i))
                            Button("Go back", GoBack)
                        }
                    )
                        .title("Details")
                | Subdetails i ->
                    ContentPage(
                        VStack(spacing = 15.) {
                            Label($"Subdetails {i}")
                            Button("Go back", GoBack)
                            Button("Go to root", GoToRoot)
                        }
                    )
                        .title($"Subdetails {i}")
            )
        )

    let program = Program.stateful init update view
