namespace Gallery.Samples

open Fabulous
open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics
open Gallery

open type Fabulous.Maui.View

module CarouselView =

    type Onboarding =
        | First
        | Second


    type Model = { SampleData: Onboarding list }

    type Msg = Id

    let init () = { SampleData = [ First; Second ] }

    let indicatorViewRef = ViewRef<IndicatorView>()

    let update msg model =
        match msg with
        | Id -> model

    let view model =
        Grid(coldefs = [], rowdefs = [ Absolute(200.); Auto ]) {
            CarouselView(
                model.SampleData,
                fun entry ->
                    match entry with
                    | First ->
                        VStack(16.) {
                            Label("First").font(attributes = FontAttributes.Bold).centerHorizontal()
                            Image("dotnet_bot.png").size(100., 100.).centerHorizontal()
                        }

                    | Second ->
                        VStack(16.) {
                            Label("Second").font(attributes = FontAttributes.Bold).centerHorizontal()
                            Image("dotnet_bot.png").size(100., 100.).centerHorizontal()
                        }
            )
                .indicatorView(indicatorViewRef)
                .loop(false)
                .horizontalScrollBarVisibility(ScrollBarVisibility.Never)
                .gridRow(0)

            IndicatorView(indicatorViewRef)
                .indicatorsShape(IndicatorShape.Circle)
                .selectedIndicatorColor(Colors.Blue)
                .indicatorColor(Colors.Black)
                .centerHorizontal()
                .gridRow(1)
                .margin(0., 16., 0., 16.)
        }

    let sample =
        { Name = "CarouselView"
          Description = "A CarouselView is a control that allows you to cycle through a collection of items."
          Program = Helper.createProgram init update view }
