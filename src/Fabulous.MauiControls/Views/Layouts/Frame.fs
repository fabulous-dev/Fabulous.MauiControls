namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabFrame =
    inherit IFabContentView

module Frame =
    let WidgetKey = Widgets.register<Frame>()

    let BorderColor = Attributes.defineBindableWithEquality Frame.BorderColorProperty
    
    let BorderFabColor = Attributes.defineBindableWithEquality Frame.BorderColorProperty

    let CornerRadius = Attributes.defineBindableFloat Frame.CornerRadiusProperty

    let HasShadow = Attributes.defineBindableBool Frame.HasShadowProperty

[<AutoOpen>]
module FrameBuilders =
    type Fabulous.Maui.View with
        /// <summary>Create a Frame widget</summary>
        static member inline Frame<'msg>() =
            WidgetBuilder<'msg, IFabFrame>(
                Frame.WidgetKey,
                AttributesBundle(
                    StackList.empty(),
                    ValueNone,
                    ValueNone
                )
            )
            
        /// <summary>Create a Frame widget with a content</summary>
        /// <param name="content">The content widget</param>
        static member inline Frame(content: WidgetBuilder<'msg, #IFabView>) =
            WidgetHelpers.buildWidgets<'msg, IFabFrame> Frame.WidgetKey [| ContentView.Content.WithValue(content.Compile()) |]

[<Extension>]
type FrameModifiers =
    /// <summary>Set the color of the frame border</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the frame border</param>
    [<Extension>]
    static member inline borderColor(this: WidgetBuilder<'msg, #IFabFrame>, value: Color) =
        this.AddScalar(Frame.BorderColor.WithValue(value))
        
    /// <summary>Set the color of the frame border</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The color of the frame border</param>
    [<Extension>]
    static member inline borderColor(this: WidgetBuilder<'msg, #IFabFrame>, value: FabColor) =
        this.AddScalar(Frame.BorderFabColor.WithValue(value))

    /// <summary>Set the corner radius of the frame</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The corner radius of the frame</param>
    [<Extension>]
    static member inline cornerRadius(this: WidgetBuilder<'msg, #IFabFrame>, value: float) =
        this.AddScalar(Frame.CornerRadius.WithValue(value))

    /// <summary>Set whether the frame has a shadow</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The value indicating whether the frame has a shadow</param>
    [<Extension>]
    static member inline hasShadow(this: WidgetBuilder<'msg, #IFabFrame>, value: bool) =
        this.AddScalar(Frame.HasShadow.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Frame control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabFrame>, value: ViewRef<Frame>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
