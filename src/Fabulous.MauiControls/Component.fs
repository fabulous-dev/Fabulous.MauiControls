namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls

module Component =
    let ComponentProperty =
        BindableProperty.CreateAttached("Component", typeof<IBaseComponent>, typeof<BindableObject>, null)

    let registerComponentFunctions () =
        BaseComponent.setComponentFunctions(
            (fun view -> (view :?> BindableObject).GetValue(ComponentProperty) :?> IBaseComponent),
            (fun view comp ->
                let previousComp =
                    (view :?> BindableObject).GetValue(ComponentProperty) :?> IBaseComponent

                if previousComp <> null then
                    previousComp.Dispose()

                (view :?> BindableObject).SetValue(ComponentProperty, comp))
        )

[<AutoOpen>]
module ComponentBuilders =
    type Fabulous.Maui.View with

        static member inline Component<'msg, 'marker>() = ComponentBuilder<'msg, 'marker>()

        static member inline MvuComponent<'msg, 'marker, 'cMsg, 'cModel>(program: Program<unit, 'cModel, 'cMsg>) =
            MvuComponentBuilder<'msg, 'marker, unit, 'cMsg, 'cModel>(program, ())

        static member inline MvuComponent<'msg, 'marker, 'cArg, 'cMsg, 'cModel>(program: Program<'cArg, 'cModel, 'cMsg>, arg: 'cArg) =
            MvuComponentBuilder<'msg, 'marker, 'cArg, 'cMsg, 'cModel>(program, arg)
