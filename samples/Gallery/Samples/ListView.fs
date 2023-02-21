namespace Gallery.Samples

open Fabulous.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics
open Gallery

open type Fabulous.Maui.View

module ListView =

    type DataType =
        { Name: string
          Species: string
          Family: string }

    type Model =
        { SampleData: DataType list
          SelectedIndex: int }

    type Msg = SelectedChanged of int

    let init () =
        { SampleData =
            [ { Name = "Dog"
                Species = "Canis familiaris"
                Family = "Canidae" }
              { Name = "Cat"
                Species = "Felis catus"
                Family = "Felidae" }
              { Name = "Mouse"
                Species = "Mus musculus"
                Family = "Muridae" } ]
          SelectedIndex = -1 }

    let update msg model =
        match msg with
        | SelectedChanged index -> { model with SelectedIndex = index }

    let view model =
        ListView(model.SampleData, (fun x -> TextCell($"{x.Name} ({x.Species})")))
            .onItemSelected(SelectedChanged)

    let sample =
        { Name = "ListView"
          Description = "A list view control using a collection with a Cell"
          Program = Helper.createProgram init update view }
