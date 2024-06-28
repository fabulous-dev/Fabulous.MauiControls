namespace Fabulous.Maui

open System
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
