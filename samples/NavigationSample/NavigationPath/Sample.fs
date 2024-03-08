namespace NavigationSample.NavigationPath

open Fabulous
open Fabulous.Maui

open type Fabulous.Maui.View

/// This is the root of the app
module Sample =
    /// We instantiate a single NavigationController that will be used for the lifetime of the app
    let nav = NavigationController()
    
    /// The Model needs only to store the current navigation stack
    type Model =
        { Navigation: NavigationStack }
        
    type Msg =
        | NavigationPushed of NavigationPath
        | BackNavigated
        | NavigationMsg of NavigationState.Msg
        | BackButtonPressed
        
    /// This is where we subscribe to the navigation events
    /// If a navigation forward is requested, we dispatch the NavigationPushed message
    /// If a navigation backward is requested, we dispatch the BackNavigated message
    let navSubscription (): Cmd<Msg> =
        [ fun dispatch ->
            nav.Navigated.Add(fun path -> dispatch (NavigationPushed path))
            nav.BackNavigated.Add(fun () -> dispatch BackNavigated) ]
    
    /// In the init function, we initialize the NavigationStack and subscribe to the navigation events
    let init() =
        let rootPage = NavigationState.init NavigationPath.PageA
        { Navigation = NavigationStack.Init(rootPage) }, navSubscription()

    let update msg model =
        match msg with
        | NavigationPushed path ->
            // When a new path is pushed, we create a new page and push it on the stack
            let newPage = NavigationState.init path
            { model with Navigation = model.Navigation.Push(newPage) }, Cmd.none
            
        | BackNavigated ->
            // BackNavigated handles both the back button and the programmatic back navigation
            // We simply pop the current page from the stack
            { model with Navigation = model.Navigation.Pop() }, Cmd.none
        
        | NavigationMsg navMsg ->
            let m, navCmd = NavigationState.update nav navMsg model.Navigation.CurrentPage
            { model with Navigation = model.Navigation.UpdateCurrentPage(m) }, Cmd.map NavigationMsg navCmd
            
        | BackButtonPressed ->
            let m, navCmd = NavigationState.updateBackButton nav model.Navigation.CurrentPage
            { model with Navigation = model.Navigation.UpdateCurrentPage(m) }, Cmd.map NavigationMsg navCmd
    
    /// The view function contains the NavigationPage control that will display the different pages
    /// and handle the navigation animations (push, pop) as well has displaying a back button by default
    ///
    /// Because of MVU, all the pages need to return the same Msg type but they all have their own.
    /// To be able to wrap those Msgs into the app's root Msg type, we use the View.map helper function.
    let view model =
        (NavigationPage() {
            // We inject in the NavigationPage history the back stack of our navigation
            for navModel in List.rev model.Navigation.BackStack do
                (View.map NavigationMsg (NavigationState.view navModel))
                    .hasBackButton(false)
                    
            // The page currently displayed is the one on top of the stack
            (View.map NavigationMsg (NavigationState.view model.Navigation.CurrentPage))
                .hasBackButton(false)
        })
            .onBackButtonPressed(BackButtonPressed)
