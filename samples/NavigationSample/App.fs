namespace NavigationSample

open Fabulous
open Fabulous.Maui

open type Fabulous.Maui.View

module App =
    type Model =
        { BasicNavigationModel: NavigationPath.Sample.Model }

    type Msg = BasicNavigationMsg of NavigationPath.Sample.Msg

    let init () =
        let m, c = NavigationPath.Sample.init()
        { BasicNavigationModel = m }, Cmd.map BasicNavigationMsg c

    let update msg model =
        match msg with
        | BasicNavigationMsg msg ->
            let m, c = NavigationPath.Sample.update msg model.BasicNavigationModel
            { BasicNavigationModel = m }, Cmd.map BasicNavigationMsg c

    let view model =
        Application(
            View.map BasicNavigationMsg (NavigationPath.Sample.view model.BasicNavigationModel)
        )

    let program = Program.statefulWithCmd init update view
