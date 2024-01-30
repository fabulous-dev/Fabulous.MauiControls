namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module Component =
    let ComponentProperty =
        BindableProperty.CreateAttached("Component", typeof<Component>, typeof<BindableObject>, null)

    let get (target: obj) =
        (target :?> BindableObject).GetValue(ComponentProperty)

    let set (comp: obj) (target: obj) =
        let target = target :?> VisualElement
        target.Unloaded.Add(fun _ -> (comp :?> Component).Dispose())
        target.SetValue(ComponentProperty, comp)

[<AutoOpen>]
module ComponentBuilders =
    type Fabulous.Maui.View with

        static member inline Component<'msg, 'marker>() = ComponentBuilder<'msg>()

        static member inline Component<'msg, 'model, 'marker, 'parentMsg>(program: Program<unit, 'model, 'msg>) =
            MvuComponentBuilder<unit, 'msg, 'model, 'marker, 'parentMsg>(program, ())

        static member inline Component<'arg, 'msg, 'model, 'marker, 'parentMsg>(program: Program<'arg, 'model, 'msg>, arg: 'arg) =
            MvuComponentBuilder<'arg, 'msg, 'model, 'marker, 'parentMsg>(program, arg)
