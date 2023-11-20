namespace HelloWorld

open System
open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Fabulous.Maui

module ButtonExt =
    let Clicked': ScalarAttributeDefinition<(unit -> unit), (unit -> unit)> =
        let name = "Button_Clicked'"
                    
        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun _ (newValueOpt: (unit -> unit) voption) node ->
                    let event = (node.Target :?> Microsoft.Maui.Controls.Button).Clicked

                    match node.TryGetHandler(name) with
                    | ValueNone -> ()
                    | ValueSome handler -> event.RemoveHandler handler

                    match newValueOpt with
                    | ValueNone -> node.SetHandler(name, ValueNone)

                    | ValueSome(fn) ->
                        let handler = EventHandler(fun _ _ -> fn())

                        event.AddHandler handler
                        node.SetHandler(name, ValueSome handler))
            )

            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }

[<AutoOpen>]
module ButtonBuildersExt =
    type Fabulous.Maui.View with
        static member inline Button'<'msg>(text: string, onClick: unit -> unit) =
            WidgetBuilder<'msg, IFabButton>(
                Button.WidgetKey,
                Button.Text.WithValue(text),
                ButtonExt.Clicked'.WithValue(onClick)
            )
        
open type Fabulous.Maui.View

module Counter =
    let body =
        view {
            let! count = state 0
            
            VStack() {
                Label($"Count is {count.Current}")
                    .centerHorizontal()
                
                Button'("Increment", fun () -> count.Set(count.Current + 1))
                Button'("Decrement", fun () -> count.Set(count.Current - 1))
            }
        }
        
module ParentChild =
    let child count =
        view {
            let count10 = count * 10
            
            Label($"Count * 10 = {count10}")
                .centerHorizontal()
        }
        
    let parent =
        view {
            let! count = state 0
            
            VStack() {
                Label($"Count is {count.Current}")
                    .centerHorizontal()
                    
                Component(child count.Current)
                
                Button'("Increment", fun () -> count.Set(count.Current + 1))
                Button'("Decrement", fun () -> count.Set(count.Current - 1))
            }
        }

module App =
    let view() =
        Application(
            ContentPage(
                (VStack(spacing = 40.) {
                    VStack(spacing = 10.) {
                        Label("Component 1")
                            .centerHorizontal()
                            
                        Component(Counter.body)
                    }
                    
                    VStack(spacing = 10.) {
                        Label("Component 2")
                            .centerHorizontal()
                            
                        Component(Counter.body)
                    }
                    
                    VStack(spacing = 10.) {
                        Label("Parent Child")
                            .centerHorizontal()
                            
                        Component(ParentChild.parent)
                    }
                })
                    .centerVertical()
            )
        )

    let program = Program.stateless view
