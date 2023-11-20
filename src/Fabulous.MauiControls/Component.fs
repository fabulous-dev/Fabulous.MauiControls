namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

[<AutoOpen>]
module ComponentBuilders =
    let ComponentProperty = BindableProperty.CreateAttached("Component", typeof<Component>, typeof<BindableObject>, null)
    
    type Fabulous.Maui.View with
        static member inline Component<'msg, 'marker>([<InlineIfLambda>] body: ComponentBodyBuilder<'msg, 'marker>) =            
            WidgetBuilder<'msg, 'marker>(
                Component.WidgetKey,
                Component.Body.WithValue(body)
            )
            
        static member inline Component<'msg, 'marker>([<InlineIfLambda>] body: ComponentBodyBuilder<'msg, 'marker>, context: ComponentContext) =
            WidgetBuilder<'msg, 'marker>(
                Component.WidgetKey,
                Component.Body.WithValue(body),
                Component.Context.WithValue(context)
            )