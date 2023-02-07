namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

/// Represents a dimension for either the row or column definition of a Grid
type Dimension =
    /// Use a size that fits the children of the row or column.
    | Auto
    /// Use a proportional size of 1
    | Star
    /// Use a proportional size defined by the associated value
    | Stars of float
    /// Use the associated value as the number of device-specific units.
    | Absolute of float

type IFabGrid =
    inherit IFabLayoutOfView

module GridUpdaters =
    let updateGridColumnDefinitions _ (newValueOpt: Dimension[] voption) (node: IViewNode) =
        let grid = node.Target :?> Grid

        match newValueOpt with
        | ValueNone -> grid.ColumnDefinitions.Clear()
        | ValueSome coll ->
            grid.ColumnDefinitions.Clear()

            for c in coll do
                let gridLength =
                    match c with
                    | Auto -> GridLength.Auto
                    | Star -> GridLength.Star
                    | Stars x -> GridLength(x, GridUnitType.Star)
                    | Absolute x -> GridLength(x, GridUnitType.Absolute)

                grid.ColumnDefinitions.Add(ColumnDefinition(Width = gridLength))

    let updateGridRowDefinitions _ (newValueOpt: Dimension[] voption) (node: IViewNode) =
        let grid = node.Target :?> Grid

        match newValueOpt with
        | ValueNone -> grid.RowDefinitions.Clear()
        | ValueSome coll ->
            grid.RowDefinitions.Clear()

            for c in coll do
                let gridLength =
                    match c with
                    | Auto -> GridLength.Auto
                    | Star -> GridLength.Star
                    | Stars x -> GridLength(x, GridUnitType.Star)
                    | Absolute x -> GridLength(x, GridUnitType.Absolute)

                grid.RowDefinitions.Add(RowDefinition(Height = gridLength))

module Grid =
    let WidgetKey = Widgets.register<Grid>()

    let ColumnDefinitions =
        Attributes.defineSimpleScalarWithEquality<Dimension array> "Grid_ColumnDefinitions" GridUpdaters.updateGridColumnDefinitions

    let RowDefinitions =
        Attributes.defineSimpleScalarWithEquality<Dimension array> "Grid_RowDefinitions" GridUpdaters.updateGridRowDefinitions

    let Column = Attributes.defineBindableInt Grid.ColumnProperty

    let Row = Attributes.defineBindableInt Grid.RowProperty

    let ColumnSpacing = Attributes.defineBindableFloat Grid.ColumnSpacingProperty

    let RowSpacing = Attributes.defineBindableFloat Grid.RowSpacingProperty

    let ColumnSpan = Attributes.defineBindableInt Grid.ColumnSpanProperty

    let RowSpan = Attributes.defineBindableInt Grid.RowSpanProperty

[<AutoOpen>]
module GridBuilders =
    type Fabulous.Maui.View with

        static member inline Grid<'msg>(coldefs: seq<Dimension>, rowdefs: seq<Dimension>) =
            CollectionBuilder<'msg, IFabGrid, IFabView>(
                Grid.WidgetKey,
                LayoutOfView.Children,
                Grid.ColumnDefinitions.WithValue(Array.ofSeq coldefs),
                Grid.RowDefinitions.WithValue(Array.ofSeq rowdefs)
            )

        static member inline Grid<'msg>() = View.Grid<'msg>([ Star ], [ Star ])

[<Extension>]
type GridModifiers =
    [<Extension>]
    static member inline columnSpacing(this: WidgetBuilder<'msg, #IFabGrid>, value: float) =
        this.AddScalar(Grid.ColumnSpacing.WithValue(value))

    [<Extension>]
    static member inline rowSpacing(this: WidgetBuilder<'msg, #IFabGrid>, value: float) =
        this.AddScalar(Grid.RowSpacing.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Grid control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabGrid>, value: ViewRef<Grid>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type GridAttachedModifiers =
    [<Extension>]
    static member inline gridColumn(this: WidgetBuilder<'msg, #IFabView>, value: int) =
        this.AddScalar(Grid.Column.WithValue(value))

    [<Extension>]
    static member inline gridRow(this: WidgetBuilder<'msg, #IFabView>, value: int) =
        this.AddScalar(Grid.Row.WithValue(value))

    [<Extension>]
    static member inline gridColumnSpan(this: WidgetBuilder<'msg, #IFabView>, value: int) =
        this.AddScalar(Grid.ColumnSpan.WithValue(value))

    [<Extension>]
    static member inline gridRowSpan(this: WidgetBuilder<'msg, #IFabView>, value: int) =
        this.AddScalar(Grid.RowSpan.WithValue(value))
