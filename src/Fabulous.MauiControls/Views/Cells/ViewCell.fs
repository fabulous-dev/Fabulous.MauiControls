namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabViewCell =
    inherit IFabCell

module ViewCell =
    let WidgetKey = Widgets.register<ViewCell>()

    let View =
        Attributes.definePropertyWidget "ViewCell_View" (fun target -> (target :?> ViewCell).View :> obj) (fun target value ->
            (target :?> ViewCell).View <- value)

[<AutoOpen>]
module ViewCellBuilders =
    type Fabulous.Maui.View with

        static member inline ViewCell<'msg, 'marker when 'marker :> IFabView>(view: WidgetBuilder<'msg, 'marker>) =
            WidgetHelpers.buildWidgets<'msg, IFabViewCell> ViewCell.WidgetKey [| ViewCell.View.WithValue(view.Compile()) |]

[<Extension>]
type ViewCellModifiers =
    /// <summary>Link a ViewRef to access the direct ViewCell control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabViewCell>, value: ViewRef<ViewCell>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
