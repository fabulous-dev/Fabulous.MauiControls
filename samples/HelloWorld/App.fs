namespace HelloWorld

open System
open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Fabulous.Maui
open Microsoft.Maui.Controls

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

module SimpleComponent =
    let body: ComponentBodyBuilder<unit, IFabLabel> =
        view {
            Label("Hello Component")
                .centerHorizontal()
        }
        
    type Fabulous.Maui.View with
        static member inline SimpleComponent() = Component(body)

module Counter =
    let body: ComponentBodyBuilder<unit, IFabVerticalStackLayout> =
        view {
            let! count = state 0
            
            VStack() {
                Label($"Count is {count.Current}")
                    .centerHorizontal()
                
                Button'("Increment", fun () -> count.Set(count.Current + 1))
                Button'("Decrement", fun () -> count.Set(count.Current - 1))
            }
        }
        
    type Fabulous.Maui.View with
        static member inline Counter() = Component(body)
        
module ParentChild =
    let child count =
        view {
            let! multiplier = state 1
            let countMultiplied = count * multiplier.Current
            
            VStack() {
                Label($"Count * {multiplier.Current} = {countMultiplied}")
                    .centerHorizontal()
                    
                Button'("Increment Multiplier", fun () -> multiplier.Set(multiplier.Current + 1))
                Button'("Decrement Multiplier", fun () -> multiplier.Set(multiplier.Current - 1))
            }
        }
        
    let parent: ComponentBodyBuilder<unit, IFabVerticalStackLayout> =
        view {
            let! count = state 0
            
            VStack() {
                Label($"Count is {count.Current}")
                    .centerHorizontal()
                
                Button'("Increment Count", fun () -> count.Set(count.Current + 1))
                Button'("Decrement Count", fun () -> count.Set(count.Current - 1))
                    
                Component(child count.Current)
            }
        }
        
    type Fabulous.Maui.View with
        static member inline ParentChild() = Component(parent)
        
module BindingBetweenParentAndChild =
    let child (count: BindingRequest<'T>) =
        view {
            let! boundCount = count
            
            VStack() {
                Label($"Child.Count is {boundCount.Current}")
                    .centerHorizontal()
                
                Button'("Increment", fun () -> boundCount.Set(boundCount.Current + 1))
                Button'("Decrement", fun () -> boundCount.Set(boundCount.Current - 1))
            }
        }
        
    let parent =
        view {
            let! count = state 0
            
            VStack() {
                Label($"Parent.Count is {count.Current}")
                    .centerHorizontal()
                    
                Button'("Increment", fun () -> count.Set(count.Current + 1))
                Button'("Decrement", fun () -> count.Set(count.Current - 1))
                
                Component(child (ofState count))
            }
            
        }
        
    type Fabulous.Maui.View with
        static member inline BindingBetweenParentAndChild() = Component(parent)
        
module SharedContextBetweenComponents =
    let body =
        view {
            let sharedContext = ComponentContext()
            
            VStack() {
                Label("Shared context between components")
                    .centerHorizontal()
                
                Component(Counter.body, sharedContext)
                Component(Counter.body, sharedContext)
            }
        }
        
module App =
    open SimpleComponent
    open Counter
    open ParentChild
    open BindingBetweenParentAndChild
    
    open type Fabulous.Maui.View
    
    let sharedContext = ComponentContext()
    
    let view() =
        Application(
            ContentPage(
                ScrollView(
                    (VStack(spacing = 40.) {
                        // Simple component
                        VStack(spacing = 20.) {
                            Label("Simple component")
                                .centerHorizontal()
                                .font(attributes = FontAttributes.Bold)
                                
                            SimpleComponent()
                        }
                        
                        // Simple component with state
                        VStack(spacing = 20.) {
                            Label("Simple components with individual states")
                                .centerHorizontal()
                                .font(attributes = FontAttributes.Bold)
                                
                            Counter()
                            Counter()
                        }
                        
                        // Parent child component
                        VStack(spacing = 20.) {
                            Label("Parent child component")
                                .centerHorizontal()
                                .font(attributes = FontAttributes.Bold)
                                
                            ParentChild()
                        }
                        
                        // Binding between parent and child
                        VStack(spacing = 20.) {
                            Label("Binding between parent and child")
                                .centerHorizontal()
                                .font(attributes = FontAttributes.Bold)
                                
                            BindingBetweenParentAndChild()
                        }
                    })
                        .centerVertical()
                )
            )
        )

    let program = Program.stateless view
