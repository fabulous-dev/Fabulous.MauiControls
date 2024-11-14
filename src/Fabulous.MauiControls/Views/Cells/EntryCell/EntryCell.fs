namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabEntryCell =
    inherit IFabCell

/// Microsoft.Maui doesn't provide an event for textChanged the EntryCell, so we implement it
type FabEntryCell() =
    inherit EntryCell()

    let mutable oldText = ""

    let textChanged = Event<EventHandler<TextChangedEventArgs>, _>()

    [<CLIEvent>]
    member _.TextChanged = textChanged.Publish

    override this.OnPropertyChanged(propertyName) =
        base.OnPropertyChanged(propertyName)

        if propertyName = EntryCell.TextProperty.PropertyName then
            textChanged.Trigger(this, TextChangedEventArgs(oldText, this.Text))

    override this.OnPropertyChanging(propertyName) =
        base.OnPropertyChanging(propertyName)

        if propertyName = EntryCell.TextProperty.PropertyName then
            oldText <- this.Text

module EntryCell =
    let WidgetKey = Widgets.register<FabEntryCell>()

    let HorizontalTextAlignment =
        Attributes.defineBindableEnum<TextAlignment> EntryCell.HorizontalTextAlignmentProperty

    let Keyboard =
        Attributes.defineBindableWithEquality<Keyboard> EntryCell.KeyboardProperty

    let Label = Attributes.defineBindableWithEquality<string> EntryCell.LabelProperty

    let LabelColor = Attributes.defineBindableColor EntryCell.LabelColorProperty

    let Placeholder =
        Attributes.defineBindableWithEquality<string> EntryCell.PlaceholderProperty

    let VerticalTextAlignment =
        Attributes.defineBindableEnum<TextAlignment> EntryCell.VerticalTextAlignmentProperty

[<Extension>]
type EntryCellModifiers =
    /// <summary>Set the horizontal text alignment</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The horizontal text alignment</param>
    [<Extension>]
    static member inline horizontalTextAlignment(this: WidgetBuilder<'msg, #IFabEntryCell>, value: TextAlignment) =
        this.AddScalar(EntryCell.HorizontalTextAlignment.WithValue(value))

    /// <summary>Set the keyboard type</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The keyboard type</param>
    [<Extension>]
    static member inline keyboard(this: WidgetBuilder<'msg, #IFabEntryCell>, value: Keyboard) =
        this.AddScalar(EntryCell.Keyboard.WithValue(value))

    /// <summary>Set the color of the label</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the label</param>
    [<Extension>]
    static member inline labelColor(this: WidgetBuilder<'msg, #IFabEntryCell>, value: Color) =
        this.AddScalar(EntryCell.LabelColor.WithValue(value))

    /// <summary>Set the placeholder text</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The placeholder</param>
    [<Extension>]
    static member inline placeholder(this: WidgetBuilder<'msg, #IFabEntryCell>, value: string) =
        this.AddScalar(EntryCell.Placeholder.WithValue(value))

    /// <summary>Set the vertical text alignment</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The vertical text alignment</param>
    [<Extension>]
    static member inline verticalTextAlignment(this: WidgetBuilder<'msg, #IFabEntryCell>, value: TextAlignment) =
        this.AddScalar(EntryCell.VerticalTextAlignment.WithValue(value))

    /// <summary>Link a ViewRef to access the direct EntryCell control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabEntryCell>, value: ViewRef<EntryCell>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
