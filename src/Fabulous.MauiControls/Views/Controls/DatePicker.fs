namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.PlatformConfiguration
open Microsoft.Maui.Graphics

type IFabDatePicker =
    inherit IFabView

module DatePicker =
    let WidgetKey = Widgets.register<DatePicker>()

    let CharacterSpacing =
        Attributes.defineBindableFloat DatePicker.CharacterSpacingProperty

    let DateWithEvent =
        Attributes.defineBindableWithEvent "DatePicker_DateSelected" DatePicker.DateProperty (fun target -> (target :?> DatePicker).DateSelected)

    let FontAutoScalingEnabled =
        Attributes.defineBindableBool DatePicker.FontAutoScalingEnabledProperty

    let FontAttributes =
        Attributes.defineBindableWithEquality<FontAttributes> DatePicker.FontAttributesProperty

    let FontFamily =
        Attributes.defineBindableWithEquality<string> DatePicker.FontFamilyProperty

    let FontSize = Attributes.defineBindableFloat DatePicker.FontSizeProperty

    let Format = Attributes.defineBindableWithEquality<string> DatePicker.FormatProperty

    let MaximumDate =
        Attributes.defineBindableWithEquality<DateTime> DatePicker.MaximumDateProperty

    let MinimumDate =
        Attributes.defineBindableWithEquality<DateTime> DatePicker.MinimumDateProperty

    let TextColor = Attributes.defineBindableWithEquality DatePicker.TextColorProperty

    let TextFabColor = Attributes.defineBindableColor DatePicker.TextColorProperty

    let UpdateMode =
        Attributes.defineSimpleScalarWithEquality<iOSSpecific.UpdateMode> "DatePicker_UpdateMode" (fun _ newValueOpt node ->
            let datePicker = node.Target :?> DatePicker

            let value =
                match newValueOpt with
                | ValueNone -> iOSSpecific.UpdateMode.Immediately
                | ValueSome v -> v

            iOSSpecific.DatePicker.SetUpdateMode(datePicker, value))

[<AutoOpen>]
module DatePickerBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a DatePicker widget with a date and listen for the date changes</summary>
        /// <param name="date">The selected date</param>
        /// <param name="onDateSelected">Message to dispatch</param>
        static member inline DatePicker<'msg>(date: DateTime, onDateSelected: DateTime -> 'msg) =
            WidgetBuilder<'msg, IFabDatePicker>(
                DatePicker.WidgetKey,
                DatePicker.DateWithEvent.WithValue(ValueEventData.create date (fun args -> onDateSelected args.NewDate |> box))
            )

[<Extension>]
type DatePickerModifiers =
    /// <summary>Set the character spacing</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The character spacing</param>
    [<Extension>]
    static member inline characterSpacing(this: WidgetBuilder<'msg, #IFabDatePicker>, value: float) =
        this.AddScalar(DatePicker.CharacterSpacing.WithValue(value))

    /// <summary>Set the font</summary>
    /// <param name="this">Current widget</param>
    /// <param name="size">The font size</param>
    /// <param name="attributes">The font attributes</param>
    /// <param name="fontFamily">The font family</param>
    /// <param name="autoScalingEnabled">The value indicating whether auto-scaling is enabled</param>
    [<Extension>]
    static member inline font
        (
            this: WidgetBuilder<'msg, #IFabDatePicker>,
            ?size: float,
            ?attributes: FontAttributes,
            ?fontFamily: string,
            ?autoScalingEnabled: bool
        ) =

        let mutable res = this

        match size with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontSize.WithValue(v))

        match attributes with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontAttributes.WithValue(v))

        match fontFamily with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontFamily.WithValue(v))

        match autoScalingEnabled with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontAutoScalingEnabled.WithValue(v))

        res

    /// <summary>Set the display format of the selected date</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The display format</param>
    [<Extension>]
    static member inline format(this: WidgetBuilder<'msg, #IFabDatePicker>, value: string) =
        this.AddScalar(DatePicker.Format.WithValue(value))

    /// <summary>Set the minimum date selectable</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The minimum date</param>
    [<Extension>]
    static member inline minimumDate(this: WidgetBuilder<'msg, #IFabDatePicker>, value: DateTime) =
        this.AddScalar(DatePicker.MinimumDate.WithValue(value))

    /// <summary>Set the maximum date selectable</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The maximum date</param>
    [<Extension>]
    static member inline maximumDate(this: WidgetBuilder<'msg, #IFabDatePicker>, value: DateTime) =
        this.AddScalar(DatePicker.MaximumDate.WithValue(value))

    /// <summary>Set the color of the text</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the text</param>
    [<Extension>]
    static member inline textColor(this: WidgetBuilder<'msg, #IFabDatePicker>, value: Color) =
        this.AddScalar(DatePicker.TextColor.WithValue(value))

    /// <summary>Set the color of the text</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the text</param>
    [<Extension>]
    static member inline textColor(this: WidgetBuilder<'msg, #IFabDatePicker>, value: FabColor) =
        this.AddScalar(DatePicker.TextFabColor.WithValue(value))

    /// <summary>Link a ViewRef to access the direct DatePicker control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabDatePicker>, value: ViewRef<DatePicker>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type DatePickerPlatformModifiers =
    /// <summary>iOS platform specific. Set a value that controls whether elements in the date picker are continuously updated while scrolling or updated once after scrolling has completed</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value that controls whether elements in the date picker are continuously updated while scrolling or updated once after scrolling has completed.</param>
    [<Extension>]
    static member inline updateMode(this: WidgetBuilder<'msg, #IFabDatePicker>, value: iOSSpecific.UpdateMode) =
        this.AddScalar(DatePicker.UpdateMode.WithValue(value))
