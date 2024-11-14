namespace Fabulous.Maui

open System
open System.IO
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabTitleBar =
    inherit IFabTemplatedView
    
module TitleBar =
    let WidgetKey = Widgets.register<TitleBar>()
    
    let Content = Attributes.defineBindableWidget TitleBar.ContentProperty
    
    let ForegroundColor = Attributes.defineBindableColor TitleBar.ForegroundColorProperty
    
    let Icon = Attributes.defineBindableImageSource TitleBar.IconProperty
    
    let LeadingContent = Attributes.defineBindableWidget TitleBar.LeadingContentProperty
    
    let Subtitle = Attributes.defineBindableWithEquality<string> TitleBar.SubtitleProperty
    
    let Title = Attributes.defineBindableWithEquality<string> TitleBar.TitleProperty
    
    let TrailingContent = Attributes.defineBindableWidget TitleBar.TrailingContentProperty
    
[<AutoOpen>]
module TitleBarBuilders =
    type Fabulous.Maui.View with
        static member inline TitleBar(icon: ImageSource, title: string) =
            WidgetBuilder<'msg, IFabTitleBar>(
                TitleBar.WidgetKey,
                TitleBar.Icon.WithValue(ImageSourceValue.Source icon),
                TitleBar.Title.WithValue(title)
            )
            
        static member inline TitleBar(icon: string, title: string) =
            WidgetBuilder<'msg, IFabTitleBar>(
                TitleBar.WidgetKey,
                TitleBar.Icon.WithValue(ImageSourceValue.File icon),
                TitleBar.Title.WithValue(title)
            )
        static member inline TitleBar(icon: Uri, title: string) =
            WidgetBuilder<'msg, IFabTitleBar>(
                TitleBar.WidgetKey,
                TitleBar.Icon.WithValue(ImageSourceValue.Uri icon),
                TitleBar.Title.WithValue(title)
            )
        static member inline TitleBar(icon: Stream, title: string) =
            WidgetBuilder<'msg, IFabTitleBar>(
                TitleBar.WidgetKey,
                TitleBar.Icon.WithValue(ImageSourceValue.Stream icon),
                TitleBar.Title.WithValue(title)
            )

[<Extension>]
type TitleBarModifiers =
    [<Extension>]
    static member inline content(this: WidgetBuilder<'msg, #IFabTitleBar>, widget: WidgetBuilder<'msg, #IFabView>) =
        this.AddWidget(TitleBar.Content.WithValue(widget.Compile()))
        
    [<Extension>]
    static member inline foregroundColor(this: WidgetBuilder<'msg, #IFabTitleBar>, value: Color) =
        this.AddScalar(TitleBar.ForegroundColor.WithValue(value))
        
    [<Extension>]
    static member inline leadingContent(this: WidgetBuilder<'msg, #IFabTitleBar>, widget: WidgetBuilder<'msg, #IFabView>) =
        this.AddWidget(TitleBar.LeadingContent.WithValue(widget.Compile()))
        
    [<Extension>]
    static member inline subtitle(this: WidgetBuilder<'msg, #IFabTitleBar>, value: string) =
        this.AddScalar(TitleBar.Subtitle.WithValue(value))
        
    [<Extension>]
    static member inline title(this: WidgetBuilder<'msg, #IFabTitleBar>, value: string) =
        this.AddScalar(TitleBar.Title.WithValue(value))
        
    [<Extension>]
    static member inline trailingContent(this: WidgetBuilder<'msg, #IFabTitleBar>, widget: WidgetBuilder<'msg, #IFabView>) =
        this.AddWidget(TitleBar.TrailingContent.WithValue(widget.Compile()))
