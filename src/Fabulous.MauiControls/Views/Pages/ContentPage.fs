namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Microsoft.Maui.Controls

type IFabContentPage =
    inherit IFabPage

type SizeAllocatedEventArgs = { Width: float; Height: float }

/// Set UseSafeArea to true by default because View DSL only shows `ignoreSafeArea`
type FabContentPage() as this =
    inherit ContentPage()
    do Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true)

    let sizeAllocated = Event<EventHandler<SizeAllocatedEventArgs>, _>()

    [<CLIEvent>]
    member _.SizeAllocated = sizeAllocated.Publish

    override this.OnSizeAllocated(width, height) =
        base.OnSizeAllocated(width, height)
        sizeAllocated.Trigger(this, { Width = width; Height = height })

module ContentPage =
    let WidgetKey = Widgets.register<FabContentPage>()

    let Content = Attributes.defineBindableWidget ContentPage.ContentProperty

    let SizeAllocated =
        Attributes.defineEvent<SizeAllocatedEventArgs> "ContentPage_SizeAllocated" (fun target -> (target :?> FabContentPage).SizeAllocated)

[<AutoOpen>]
module ContentPageBuilders =
    type Fabulous.Maui.View with

        static member inline ContentPage<'msg, 'marker when 'marker :> IFabView>(title: string, content: WidgetBuilder<'msg, 'marker>) =
            WidgetBuilder<'msg, IFabContentPage>(
                ContentPage.WidgetKey,
                AttributesBundle(StackList.one(Page.Title.WithValue(title)), ValueSome [| ContentPage.Content.WithValue(content.Compile()) |], ValueNone)
            )

[<Extension>]
type ContentPageModifiers =
    [<Extension>]
    static member inline onSizeAllocated(this: WidgetBuilder<'msg, #IFabContentPage>, fn: SizeAllocatedEventArgs -> 'msg) =
        this.AddScalar(ContentPage.SizeAllocated.WithValue(fn >> box))

    /// <summary>Link a ViewRef to access the direct ContentPage control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabContentPage>, value: ViewRef<ContentPage>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
