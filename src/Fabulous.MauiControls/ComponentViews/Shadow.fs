namespace Fabulous.Maui.Components

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabComponentShadow =
    inherit IFabShadow
    inherit IFabComponentElement

[<AutoOpen>]
module ShadowBuilders =
    type Fabulous.Maui.Components.View with

        /// <summary>Create a Shadow widget with a brush and an offset</summary>
        /// <param name="brush">Brush, of type Brush, represents the brush used to colorize the shadow</param>
        /// <param name="offset">OffSet, of type Point, specifies the offset for the shadow, which represents the position of the light source that creates the shadow</param>
        static member inline Shadow(brush: Brush, offset: Point) =
            WidgetBuilder<'msg, IFabComponentShadow>(Shadow.WidgetKey, Shadow.Brush.WithValue(brush), Shadow.Offset.WithValue(offset))

[<Extension>]
type ShadowModifiers =

    /// <summary>Link a ViewRef to access the direct Shadow control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentShadow>, value: ViewRef<Shadow>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
