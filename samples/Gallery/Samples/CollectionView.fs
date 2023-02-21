namespace Gallery.Samples

open Fabulous.Maui
open Microsoft.Maui.Controls
open Gallery

open type Fabulous.Maui.View

module CollectionView =

    type DataType =
        { Name: string
          Species: string
          Family: string }

    type Model =
        { SampleData: DataType list
          CurrentSelection: string }

    type Msg = SelectedChanged of SelectionChangedEventArgs

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
          CurrentSelection = "" }

    let update msg model =
        match msg with
        | SelectedChanged args ->
            let selection =
                args.CurrentSelection |> Seq.tryHead
                |> Option.map (fun x -> (x :?> DataType).Name)
                |> Option.defaultValue ""
            { model with CurrentSelection = selection }

    let view model =
        VStack(){
            Label($"Current selection: {model.CurrentSelection}")
            CollectionView(model.SampleData, (fun x -> Label($"{x.Name} ({x.Species})")))
                .selectionMode(SelectionMode.Single)
                .itemSizingStrategy(ItemSizingStrategy.MeasureAllItems)
                .onSelectionChanged(SelectedChanged)
        }

    let sample =
        { Name = "CollectionView"
          Description = "A CollectionView with a selection changed event"
          Program = Helper.createProgram init update view }
