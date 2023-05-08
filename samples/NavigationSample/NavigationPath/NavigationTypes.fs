namespace NavigationSample.NavigationPath

open Fabulous

[<RequireQualifiedAccess>]
type NavigationPath =
    | PageA
    | PageB of initialCount: int
    | PageC of someArgs: string * stepCount: int

type NavigationController() =
    let navigated = Event<NavigationPath>()
    let backNavigated = Event<unit>()
    
    member this.Navigated = navigated.Publish
    member this.BackNavigated = backNavigated.Publish
    
    member this.NavigateTo(path: NavigationPath) =
        navigated.Trigger(path)
        
    member this.NavigateBack() =
        backNavigated.Trigger()
        
module Navigation =
    let private navigateTo (nav: NavigationController) path: Cmd<'msg> =
        [ fun _ -> nav.NavigateTo(path) ]

    let private backNavigated (nav: NavigationController): Cmd<'msg> =
        [ fun _ -> nav.NavigateBack() ]
        
    let navigateToPageA nav = navigateTo nav NavigationPath.PageA
    
    let navigateToPageB nav initialCount = navigateTo nav (NavigationPath.PageB initialCount)
    
    let navigateToPageC nav someArgs stepCount = navigateTo nav (NavigationPath.PageC(someArgs, stepCount))