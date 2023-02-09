namespace Gallery

open Microsoft.FSharp.Core
open Fabulous.Maui

open type Fabulous.Maui.View

module App =
    type Path =
        | Overview
        | Sample of sampleIndex: int * sampleModel: obj

    type Model = { Paths: Path list }

    type Msg =
        | SampleMsg of obj
        | GoToSample of int
        | GoBack

    let init () = { Paths = [ Overview ] }

    let update msg model =
        match msg with
        | SampleMsg sMsg ->
            match List.tryHead model.Paths with
            | Some(Sample(index, sampleModel)) ->
                let newSampleModel =
                    RegisteredSamples.samples[index].Program.update sMsg sampleModel

                { model with
                    Paths = Sample(index, newSampleModel) :: List.tail model.Paths }
            | _ -> model

        | GoToSample index ->
            let sampleModel = RegisteredSamples.samples[index].Program.init()
            let paths = Sample(index, sampleModel) :: model.Paths
            { model with Paths = paths }

        | GoBack ->
            { model with
                Paths = List.tail model.Paths }

    let view model =
        Application(
            ContentPage(
                match List.head model.Paths with
                | Overview -> AnyView(Overview.view GoToSample)
                | Sample(index, sampleModel) -> AnyView(SamplePage.view GoBack SampleMsg index sampleModel)
            )
        )

    let program = Program.stateful init update view |> Program.withThemeAwareness
