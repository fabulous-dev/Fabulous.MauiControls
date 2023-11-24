namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module Component =
    let ComponentProperty =
        BindableProperty.CreateAttached("Component", typeof<IBaseComponent>, typeof<BindableObject>, null)

    let registerComponentFunctions () =
        Component.setComponentFunctions(
            (fun view -> (view :?> BindableObject).GetValue(ComponentProperty) :?> IBaseComponent),
            (fun view comp -> (view :?> BindableObject).SetValue(ComponentProperty, comp))
        )

[<AutoOpen>]
module ComponentBuilders =
    type Fabulous.Maui.View with
        static member inline Component<'marker>() = ComponentBuilder()
        
        static member MvuComponent(program) = MvuComponentBuilder(program)
