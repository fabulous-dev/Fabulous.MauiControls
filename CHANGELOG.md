# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Fixed

- Fix the crash at startup when targeting Windows by using FSharp.Maui.WinUICompat (https://github.com/fabulous-dev/Fabulous.MauiControls/pull/10)

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
