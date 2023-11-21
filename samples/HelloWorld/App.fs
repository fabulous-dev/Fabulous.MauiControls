namespace HelloWorld

open System
open Fabulous
open Fabulous.ScalarAttributeDefinitions
open Fabulous.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

module ButtonExt =
    let Clicked': ScalarAttributeDefinition<unit -> unit, unit -> unit> =
        let name = "Button_Clicked'"
                    
        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun _ (newValueOpt: (unit -> unit) voption) node ->
                    let event = (node.Target :?> Button).Clicked

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

module Components =
    type Fabulous.Maui.View with
        static member inline SimpleComponent() =
            Component() {
                Label("Hello Component")
                    .centerHorizontal()
            }

        static member inline Counter() =
            Component() {
                let! count = state 0
                
                VStack() {
                    Label($"Count is {count.Current}")
                        .centerHorizontal()
                    
                    Button'("Increment", fun () -> count.Set(count.Current + 1))
                    Button'("Decrement", fun () -> count.Set(count.Current - 1))
                }
            }
            
        static member inline ParentChild_Child(count: int) =
            Component() {
                let! multiplier = state 1
                let countMultiplied = count * multiplier.Current
                
                VStack() {
                    Label($"Count * {multiplier.Current} = {countMultiplied}")
                        .centerHorizontal()
                        
                    Button'("Increment Multiplier", fun () -> multiplier.Set(multiplier.Current + 1))
                    Button'("Decrement Multiplier", fun () -> multiplier.Set(multiplier.Current - 1))
                }
            }
        
        static member inline ParentChild_Parent() =
            Component() {
                let! count = state 1
                
                VStack() {
                    Label($"Count is {count.Current}")
                        .centerHorizontal()
                    
                    Button'("Increment Count", fun () -> count.Set(count.Current + 1))
                    Button'("Decrement Count", fun () -> count.Set(count.Current - 1))
                    
                    View.ParentChild_Child(count.Current)
                }
            }
            
        static member inline Child(count: BindingRequest<'T>) =
            Component() {
                let! boundCount = count
                
                VStack() {
                    Label($"Child.Count is {boundCount.Current}")
                        .centerHorizontal()
                    
                    Button'("Increment", fun () -> boundCount.Set(boundCount.Current + 1))
                    Button'("Decrement", fun () -> boundCount.Set(boundCount.Current - 1))
                }
            }
        
        static member inline BindingBetweenParentAndChild() =
            Component() {
                let! count = state 0
                
                VStack() {
                    Label($"Parent.Count is {count.Current}")
                        .centerHorizontal()
                        
                    Button'("Increment", fun () -> count.Set(count.Current + 1))
                    Button'("Decrement", fun () -> count.Set(count.Current - 1))
                    
                    View.Child(ofState count)
                }
            }
        
        static member inline SharedContextBetweenComponents() =
            Component() {
                let sharedContext = ComponentContext()
                
                VStack() {
                    View.Counter()
                        .withContext(sharedContext)
                        
                    View.Counter()
                        .withContext(sharedContext)
                }
            }
            
        static member inline ModifiersOnComponent() =
            Component() {
                let! toggle = state false
                
                VStack() {
                    Button'("Toggle", fun () -> toggle.Set(not toggle.Current))
                    
                    View.SimpleComponent()
                        .background(SolidColorBrush(if toggle.Current then Colors.Red else Colors.Blue))
                }
            }
        
module App =
    open Components
    
    open type Fabulous.Maui.View
    
    let app () =
        Component() {
            let! appState = state 0
            
            Application(
                ContentPage(
                    ScrollView(
                        (VStack(spacing = 40.) {
                            // App state display
                            VStack(spacing = 20.) {
                                Label("App state")
                                    .centerHorizontal()
                                    .font(attributes = FontAttributes.Bold)
                                    
                                VStack(spacing = 0.) { 
                                    Label($"AppState = {appState.Current}")
                                    Button'("Increment", fun () -> appState.Set(appState.Current + 1))
                                    Button'("Decrement", fun () -> appState.Set(appState.Current - 1))
                                }
                            }
                            
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
                                    
                                ParentChild_Parent()
                            }
                            
                            // Binding between parent and child
                            VStack(spacing = 20.) {
                                Label("Binding between parent and child")
                                    .centerHorizontal()
                                    .font(attributes = FontAttributes.Bold)
                                    
                                BindingBetweenParentAndChild()
                            }
                            
                            // Shared context between components
                            VStack(spacing = 20.) {
                                Label("Shared context between components")
                                    .centerHorizontal()
                                    .font(attributes = FontAttributes.Bold)
                                    
                                SharedContextBetweenComponents()
                            }
                            
                            // Modifiers on component
                            VStack(spacing = 20.) {
                                Label("Modifiers on component")
                                    .centerHorizontal()
                                    .font(attributes = FontAttributes.Bold)
                                    
                                ModifiersOnComponent()
                            }
                        })
                            .centerVertical()
                    )
                )
            )
        }