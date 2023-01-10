# Fabulous for .NET MAUI (Microsoft.Maui.Controls)

[![build](https://img.shields.io/github/actions/workflow/status/fabulous-dev/Fabulous.MauiControls/build.yml?branch=main)](https://github.com/fabulous-dev/Fabulous.MauiControls/actions/workflows/build.yml) [![NuGet version](https://img.shields.io/nuget/v/Fabulous.MauiControls)](https://www.nuget.org/packages/Fabulous.MauiControls) [![NuGet downloads](https://img.shields.io/nuget/dt/Fabulous.MauiControls)](https://www.nuget.org/packages/Fabulous.MauiControls) [![Discord](https://img.shields.io/discord/716980335593914419?label=discord&logo=discord)](https://discord.gg/bpTJMbSSYK) [![Twitter Follow](https://img.shields.io/twitter/follow/FabulousAppDev?style=social)](https://twitter.com/FabulousAppDev)

Fabulous.MauiControls brings the great development experience of Fabulous to .NET MAUI, allowing you to take advantage of the latest cross-platform UI framework from Microsoft with a tailored declarative UI DSL and clean architecture.

Deploy to any platform supported by .NET MAUI, such as Android, iOS, macOS, Windows, Linux and more!

```fs
/// A simple Counter app

type Model =
    { Count: int }

type Msg =
    | Increment
    | Decrement

let init () =
    { Count = 0 }

let update msg model =
    match msg with
    | Increment -> { model with Count = model.Count + 1 }
    | Decrement -> { model with Count = model.Count - 1 }

let view model =
    Application(
        ContentPage(
            "Counter app",
            VStack(spacing = 16.) {
                Image(Aspect.AspectFit, "fabulous.png")

                Label($"Count is {model.Count}")

                Button("Increment", Increment)
                Button("Decrement", Decrement)
            }
        )
    )
```

To learn more about Fabulous, visit https://fabulous.dev.

## Getting Started

You can start your new Fabulous.MauiControls app in a matter of seconds using the dotnet CLI templates.  
For a starter guide see our [documentation](https://docs.fabulous.dev/v2/maui.controls/getting-started).

```sh
dotnet new install Fabulous.MauiControls.Templates
dotnet new fabulous-mauicontrols -n MyApp
```

## Documentation

Documentation can be found at https://docs.fabulous.dev/v2/maui.controls

## Sponsor Fabulous

Donating is a fantastic way to support all the efforts going into making Fabulous the best declarative UI framework for dotnet.  
We accept donations through the GitHub Sponsors program.

If you need support see Commercial Support section below.

## Commercial support

If you would like us to provide you with:

- training and workshops,
- support services,
- and consulting services.

Feel free to contact us: [support@fabulous.dev](mailto:support@fabulous.dev)
