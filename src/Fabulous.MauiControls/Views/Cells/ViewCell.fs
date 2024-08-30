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

        /// <summary>Create a ViewCell with a content widget</summary>
        /// <param name="view">The content widget</param>
        static member inline ViewCell(view: WidgetBuilder<'msg, #IFabView>) =
            WidgetHelpers.buildWidgets<'msg, IFabViewCell> ViewCell.WidgetKey [| ViewCell.View.WithValue(view.Compile()) |]

[<Extension>]
type ViewCellModifiers =
    /// <summary>Link a ViewRef to access the direct MenuItem control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabViewCell>, value: ViewRef<ViewCell>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
