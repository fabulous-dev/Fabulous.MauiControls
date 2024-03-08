namespace NavigationSample.NavigationPath

open Fabulous
open Fabulous.Maui

open type Fabulous.Maui.View

module NavigationState =
    type Model =
        | PageAModel of PageA.Model
        | PageBModel of PageB.Model
        | PageCModel of PageC.Model
        
    type Msg =
        | PageAMsg of PageA.Msg
        | PageBMsg of PageB.Msg
        | PageCMsg of PageC.Msg
        
    let init path =
        match path with
        | NavigationPath.PageA -> PageAModel (PageA.init ())
        | NavigationPath.PageB initialCount -> PageBModel (PageB.init initialCount)
        | NavigationPath.PageC(someArgs, stepCount) -> PageCModel (PageC.init someArgs stepCount)
        
    let update nav (msg: Msg) (model: Model) =
        match msg, model with
        | PageAMsg msg, PageAModel model ->
            let m, c = PageA.update nav msg model
            PageAModel m, Cmd.map PageAMsg c
            
        | PageBMsg msg, PageBModel model ->
            let m, c = PageB.update nav msg model
            PageBModel m, Cmd.map PageBMsg c
            
        | PageCMsg msg, PageCModel model ->
            let m, c = PageC.update nav msg model
            PageCModel m, Cmd.map PageCMsg c
            
        | _ -> model, Cmd.none
        
    let view model =
        match model with
        | PageAModel model -> AnyPage(View.map PageAMsg (PageA.view model))
        | PageBModel model -> AnyPage(View.map PageBMsg (PageB.view model))
        | PageCModel model -> AnyPage(View.map PageCMsg (PageC.view model))

    let updateBackButton nav model =
        match model with
        | PageAModel model -> update nav (PageAMsg PageA.BackButtonPressed) (PageAModel model)
        | _ -> model, Cmd.none