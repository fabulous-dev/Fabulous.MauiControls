namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

[<AutoOpen>]
module ComponentBuilders =
    let ComponentProperty = BindableProperty.CreateAttached("Component", typeof<Component>, typeof<BindableObject>, null)
    
    type Fabulous.Maui.View with
        static member inline Component<'msg, 'marker>() =            
            ViewBuilder()