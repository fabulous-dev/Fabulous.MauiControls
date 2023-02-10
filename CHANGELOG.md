# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

_No unreleased changes_

## [2.3.2] - 2023-02-10

### Fixed
- Make sure Info.plist is not ignored during build by @TimLariviere (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/22)

## [2.3.1] - 2023-02-09

### Added
- Add missing RoundRectangle widget
- Add alternative `.size(uniformSize)` modifier to all widgets

### Fixed
- Fix wrong attribute definitions for some FabColor modifiers
- Fix crash in NavigationPage when pushing a new page

### Removed
- Remove unneeded default Border stroke shape
- Remove alternative Color/FabColor for Brush modifiers

## [2.3.0] - 2023-02-09

### Added
- Add attributes for both `Microsoft.Maui.Graphics.Color` and `Fabulous.Maui.FabColor` for all Color and Brush modifiers
- Add alternative Brush modifiers to accept either `Microsoft.Maui.Graphics.Brush` or a Brush widget
- Add XML documentation for all widgets and modifiers
- Add new `GraphicsView` widget
- Add ThemeAware support by @TimLariviere (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/19)

### Fixed
- Fix the crash at startup when targeting Windows by using FSharp.Maui.WinUICompat by @TimLariviere (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/10)
- Fix an issue where template could enumerate the whole disk to add under the Android target folder by @Smaug123 (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/9)
- Write a custom NavigationPage to use synchronous push/pop and call Mounted/Unmounted events on pages by @TimLariviere (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/13)
- Fix widgets and modifiers where `Microsoft.Maui.IView` was requested instead of `Fabulous.Maui.IFabView`

### Removed
- Remove obsolete widgets and modifiers
- Remove AppTheme modifiers in favor of ThemeAware
- Remove Color modifiers when Brush modifiers are available (eg. BackgroundColor -> Background)
- Remove Frame widget in favor of Border widget

## [2.2.0] - 2023-01-24

### Changed
- Rename all marker interfaces to IFab* to avoid naming conflicts with Maui interfaces (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/5)
- Fix an issue where FlexLayout was not usable (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/6)
- Upgrade to Fabulous 2.2.0 (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/8)

## [2.1.3] - 2023-01-14

### Changed
- Apply fix to templates to ensure correct version of Fabulous and Fabulous.MauiControls (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/4) 

## [2.1.2] - 2023-01-09

### Changed
- Remove generic types from WidgetItems and GroupedWidgetItems (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/2)

## [2.1.1] - 2023-01-05

### Changed
- Fabulous.MauiControls has moved from the Fabulous repository to its own repository: [https://github.com/fabulous-dev/Fabulous.MauiControls](https://github.com/fabulous-dev/Fabulous.MauiControls)

[unreleased]: https://github.com/fabulous-dev/Fabulous.MauiControls/compare/2.1.3...HEAD
[2.2.0]: https://github.com/fabulous-dev/Fabulous.MauiControls/releases/tag/2.2.0
[2.1.3]: https://github.com/fabulous-dev/Fabulous.MauiControls/releases/tag/2.1.3
[2.1.2]: https://github.com/fabulous-dev/Fabulous.MauiControls/releases/tag/2.1.2
[2.1.1]: https://github.com/fabulous-dev/Fabulous.MauiControls/releases/tag/2.1.1
