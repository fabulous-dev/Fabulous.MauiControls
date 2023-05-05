namespace Playground

open Android.App
open Android.Content.PM
open Android.Views
open Microsoft.Maui

type KeyCodeReceivedService() =
    let keyCodeReceived = Event<KeyCodeResult>()

    static let _instance = KeyCodeReceivedService()

    static member Current = _instance

    member this.OnKeyCodeReceivedReceived(code: KeyCodeResult) = keyCodeReceived.Trigger(code)

    interface IKeyCodeReceivedService with
        [<CLIEvent>]
        member this.KeyCodeReceived = keyCodeReceived.Publish

[<Activity(Theme = "@style/Maui.SplashTheme",
           MainLauncher = true,
           ConfigurationChanges =
               (ConfigChanges.ScreenSize
                ||| ConfigChanges.Orientation
                ||| ConfigChanges.UiMode
                ||| ConfigChanges.ScreenLayout
                ||| ConfigChanges.SmallestScreenSize
                ||| ConfigChanges.Density))>]
type MainActivity() =
    inherit MauiAppCompatActivity()

    override this.OnKeyDown(keyCode: Keycode, eventArgs) =
        let keyCodeRes =
            match keyCode with
            | Keycode.VolumeUp -> KeyCodeResult.VolumeUp
            | Keycode.VolumeDown -> KeyCodeResult.VolumeDown
            | Keycode.Back -> KeyCodeResult.Back
            | _ -> KeyCodeResult.Unknown

        KeyCodeReceivedService.Current.OnKeyCodeReceivedReceived(keyCodeRes)
        base.OnKeyDown(keyCode, eventArgs)
