namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module Component =
    let ComponentProperty = BindableProperty.CreateAttached("Component", typeof<Component>, typeof<BindableObject>, null)
    
    let registerComponentFunctions() =
        Component.SetComponentFunctions(
            (fun view -> (view :?> BindableObject).GetValue(ComponentProperty) :?> Component),
            (fun view comp -> (view :?> BindableObject).SetValue(ComponentProperty, comp))
        )

[<AutoOpen>]
module ComponentBuilders =    
    type Fabulous.Maui.View with
        static member inline Component<'msg, 'marker>() =            
            ComponentBuilder()