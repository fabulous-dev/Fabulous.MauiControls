namespace NavigationSample

open Fabulous
open Fabulous.Maui
open NavigationSample.BasicNavigation

open type Fabulous.Maui.View

module App =
    type Model =
        { BasicNavigationModel: BasicNavigation.Model }

    type Msg = BasicNavigationMsg of BasicNavigation.Msg

    let init () =
        { BasicNavigationModel = BasicNavigation.init() }

    let update msg model =
        match msg with
        | BasicNavigationMsg m -> { BasicNavigationModel = BasicNavigation.update m model.BasicNavigationModel }

    let view model =
        Application(
            View.map BasicNavigationMsg (BasicNavigation.view model.BasicNavigationModel)
        )

    let program = Program.stateful init update view
