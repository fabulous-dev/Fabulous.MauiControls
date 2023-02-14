namespace Fabulous.Maui

open System
open System.Collections.Generic
open Fabulous
open Fabulous.Maui
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

type IFabNavigationStack =
    inherit IFabNavigationPage
    

    
    
type FabNavigationStack() =
    inherit NavigationPage()
    
    let stackUpdated = Event<EventHandler<EventArgs>, EventArgs>()
    let mutable stack: IEnumerable<Page> = [||]
    
    [<CLIEvent>]
    member _.StackUpdated = stackUpdated.Publish
    
    member this.Stack
        with get () = stack
        and set value =
            if stack <> value then
                stack <- value
                this.ApplyStack()
        
    member private this.ApplyStack() =
        // TODO: Diff the stack and only update the pages that changed
        stackUpdated.Trigger(this, EventArgs())
    
    
    
    
    
    
    
    
    
    
    

module NavigationStack =
    let WidgetKey = Widgets.register<FabNavigationStack>()
    
    let Stack = Attributes.defineSimpleScalarWithEquality<ArraySlice<Widget>> "NavigationStack_Stack" (fun prevOpt currOpt node ->
        ()
    )
    
[<AutoOpen>]
module NavigationStackBuilders =
    type Fabulous.Maui.View with
        static member inline NavigationStack(stack: 'path list, onStackUpdated: 'path list -> 'msg, router: 'path -> WidgetBuilder<'msg, #IFabPage>) =            
            let pages = [| for path in stack do (router path).Compile() |]
            let pages: ArraySlice<Widget> = (uint16 pages.Length, pages)
            
            WidgetBuilder<'msg, IFabNavigationStack>(
                NavigationStack.WidgetKey,
                AttributesBundle(
                    StackList.empty(),
                    ValueNone,
                    ValueSome [|
                        NavigationPage.Pages.WithValue(pages)
                    |]
                )
            )