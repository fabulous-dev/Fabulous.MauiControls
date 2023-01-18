namespace Fabulous.Maui

open Fabulous

type IFabLayoutOfView =
    inherit IFabLayout

module LayoutOfView =
    let Children =
        Attributes.defineListWidgetCollection "LayoutOfWidget_Children" (fun target -> (target :?> Microsoft.Maui.Controls.Layout).Children)
