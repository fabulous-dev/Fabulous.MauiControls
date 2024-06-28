namespace Fabulous.Maui.Mvu

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

type IFabMvuContentPage =
    inherit IFabContentPage
    inherit IFabMvuPage

module ContentPage =
    let SizeAllocated =
        Attributes.defineEvent<SizeAllocatedEventArgs> "ContentPage_SizeAllocated" (fun target -> (target :?> FabContentPage).SizeAllocated)

[<AutoOpen>]
module ContentPageBuilders =
    type Fabulous.Maui.View with

        /// <summary>Create a ContentPage with a content widget</summary>
        /// <param name="content">The content widget</param>
        static member inline ContentPage<'msg, 'marker when 'marker :> IFabView>(content: WidgetBuilder<'msg, 'marker>) =
            WidgetBuilder<'msg, IFabMvuContentPage>(
                ContentPage.WidgetKey,
                AttributesBundle(StackList.empty(), ValueSome [| ContentPage.Content.WithValue(content.Compile()) |], ValueNone)
            )

        static member inline ContentPage<'msg, 'childMarker>() =
            SingleChildBuilder<'msg, IFabMvuContentPage, 'childMarker>(ContentPage.WidgetKey, ContentPage.Content)
            
[<Extension>]
type ContentPageModifiers =
    /// <summary>Listen for SizeAllocated event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Message to dispatch</param>
    [<Extension>]
    static member inline onSizeAllocated(this: WidgetBuilder<'msg, #IFabMvuContentPage>, fn: SizeAllocatedEventArgs -> 'msg) =
        this.AddScalar(ContentPage.SizeAllocated.WithValue(fn))
        
    /// <summary>Link a ViewRef to access the direct ContentPage control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMvuContentPage>, value: ViewRef<ContentPage>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))