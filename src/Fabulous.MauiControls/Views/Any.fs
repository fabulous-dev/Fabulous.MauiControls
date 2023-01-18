namespace Fabulous.Maui

open Fabulous
open Fabulous.Maui

[<AutoOpen>]
module AnyBuilders =
    type Fabulous.Maui.View with

        /// Downcast to IView to allow to return different types of views in a single expression (e.g. if/else, match with pattern, etc.)
        static member AnyView<'msg, 'marker when 'marker :> IFabView>(widget: WidgetBuilder<'msg, 'marker>) =
            WidgetBuilder<'msg, IFabView>(widget.Key, widget.Attributes)

        /// Downcast to IPage to allow to return different types of pages in a single expression (e.g. if/else, match with pattern, etc.)
        static member AnyPage<'msg, 'marker when 'marker :> IFabPage>(widget: WidgetBuilder<'msg, 'marker>) =
            WidgetBuilder<'msg, IFabPage>(widget.Key, widget.Attributes)

        /// Downcast to ICell to allow to return different types of cells in a single expression (e.g. if/else, match with pattern, etc.)
        static member AnyCell<'msg, 'marker when 'marker :> IFabCell>(widget: WidgetBuilder<'msg, 'marker>) =
            WidgetBuilder<'msg, IFabCell>(widget.Key, widget.Attributes)
