namespace NavigationSample.NavigationPath

open Fabulous
open Fabulous.Maui

open type Fabulous.Maui.View

module Sample =
    let nav = NavigationController()
    
    type Model =
        { Navigation: NavigationState.Model list }
        
    type Msg =
        | NavigationPushed of NavigationPath
        | BackNavigated
        | NavigationMsg of NavigationState.Msg
        | BackButtonPressed
        
    let navSubscription (): Cmd<Msg> =
        [ fun dispatch ->
            nav.Navigated.Add(fun path -> dispatch (NavigationPushed path))
            nav.BackNavigated.Add(fun () -> dispatch BackNavigated) ]
    
    let init() =
        { Navigation = [ NavigationState.init NavigationPath.PageA ] }, navSubscription()

    let update msg model =
        match msg with
        | NavigationPushed path ->
            let newPage = NavigationState.init path
            { model with Navigation = newPage :: model.Navigation }, Cmd.none
            
        | BackNavigated ->
            { model with Navigation = model.Navigation.Tail }, Cmd.none
        
        | NavigationMsg navMsg ->
            let m, navCmd = NavigationState.update nav navMsg model.Navigation.Head
            { model with Navigation = m :: model.Navigation.Tail }, Cmd.map NavigationMsg navCmd
            
        | BackButtonPressed ->
            let m, navCmd = NavigationState.updateBackButton nav model.Navigation.Head
            { model with Navigation = m :: model.Navigation.Tail }, Cmd.map NavigationMsg navCmd
    
    let view model =
        (NavigationPage() {
            for navModel in List.rev model.Navigation do
                (View.map NavigationMsg (NavigationState.view navModel))
                    .hasBackButton(false)
                    .hasNavigationBar(false)
        })
            .onBackButtonPressed(BackButtonPressed)
