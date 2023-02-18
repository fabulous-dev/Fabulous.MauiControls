namespace Gallery.Samples

open Gallery
open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Graphics
open type Fabulous.Maui.View

module Border =
    let view () =
        VStack(spacing = 15.) {
            Label("A control which decorates a child with a border and background")

            (VStack(16.) {
                Border(SolidColorBrush(Colors.BlueViolet), Label("Border"))
                    .background(SolidColorBrush(Colors.ForestGreen))
                    .strokeThickness(2.)
                    .padding(16.)

            })
                .margin(0., 16., 0., 0.)
                .centerHorizontal()

            Border(SolidColorBrush(Colors.BlueViolet), Label("Border and Background"))
                .background(SolidColorBrush(Colors.ForestGreen))
                .strokeThickness(4.)
                .padding(16.)
                .centerHorizontal()

            Border(SolidColorBrush(Colors.BlueViolet), Label("Rounded Corners"))
                .strokeShape(RoundRectangle(CornerRadius(8.)))
                .strokeThickness(8.)
                .padding(16.)
                .centerHorizontal()


            Border(SolidColorBrush(Colors.Magenta), Label("Rounded Corners"))
                .background(SolidColorBrush(Colors.Green))
                .strokeShape(RoundRectangle(CornerRadius(8.)))
                .padding(16.)
                .centerHorizontal()


            Border(SolidColorBrush(Colors.Green), Image("dotnet_bot.png"))
                .width(100.)
                .height(100.)
                .strokeThickness(0.)
                .background(SolidColorBrush(Colors.Green))
                .strokeShape(RoundRectangle(CornerRadius(100.)))
        }

    let sampleProgram = Helper.createStatelessProgram view

    let sample =
        { Name = "Border"
          Description = "A control which decorates a child with a border and background"
          Program = sampleProgram }
