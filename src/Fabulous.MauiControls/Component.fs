namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module Component =
    let ComponentProperty =
        BindableProperty.CreateAttached("Component", typeof<Component>, typeof<BindableObject>, null)

    let get (target: obj) =
        (target :?> BindableObject).GetValue(ComponentProperty)

    let set (comp: obj) (target: obj) =
        (target :?> BindableObject).SetValue(ComponentProperty, comp)

[<AutoOpen>]
module ComponentBuilders =
    type Fabulous.Maui.View with

        static member inline Component<'msg, 'marker>() = ComponentBuilder()

        static member inline Component(program: Program<unit, 'model, 'msg>) = MvuComponentBuilder(program, ())

        static member inline Component(program: Program<'arg, 'model, 'msg>, arg: 'arg) = MvuComponentBuilder(program, arg)
