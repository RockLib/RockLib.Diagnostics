# RockLib.Diagnostics Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## 5.0.0 - 2025-02-12

### Changed
- Finalizing 5.0.0 release

## 5.0.0-alpha.1 - 2025-02-06

### Changed
- RockLib.Configuration.4.0.3 -> RockLib.Configuration.5.0.0
- RockLib.Configuration.ObjectFactory.3.0.0 -> RockLib.Configuration.4.0.0

## 4.0.0 - 2025-01-24

### Changed
- Removed .NET 6 as a target framework

## 3.0.2 - 2024-10-17

### Changed
- RockLib.Configuration.4.0.1 -> RockLib.Configuration.4.0.3 to fix vulnerability

## 3.0.1 - 2024-07-15

### Changed
- RockLib.Configuration.4.0.0 -> RockLib.Configuration.4.0.1 to fix vulnerability

## 3.0.0 - 2024-02-21

#### Changed
- Finalizing 3.0.0 release.

## 3.0.0-alpha.1 - 2024-02-21

#### Changed
- Removed netcoreapp3.1 and added net8.0 for TFMs.

## 2.0.1 - 2023-02-02

#### Added
- Added updated dependency for RockLib.Configuration.ObjectFactory

## 2.0.0 - 2022-02-23

#### Added
- Added `.editorconfig` and `Directory.Build.props` files to ensure consistency.

#### Changed
- Supported targets: net6.0, netcoreapp3.1, and net48.
- As the package now uses nullable reference types, some method parameters now specify if they can accept nullable values.

## 1.0.9 - 2021-08-11

#### Changed

- Changes "Quicken Loans" to "Rocket Mortgage".
- Updates RockLib.Configuration to latest version, [2.5.3](https://github.com/RockLib/RockLib.Configuration/blob/main/RockLib.Configuration/CHANGELOG.md#253---2021-08-11).
- Updates RockLib.Configuration.ObjectFactory to latest version, [1.6.9](https://github.com/RockLib/RockLib.Configuration/blob/main/RockLib.Configuration.ObjectFactory/CHANGELOG.md#169---2021-08-11).

## 1.0.8 - 2021-05-06

#### Added

- Adds SourceLink to nuget package.

#### Fixed

- Fixes bug in DebugTraceListener where nothing was ever actually written to debug.

#### Changed

- Updates RockLib.Configuration and RockLib.Configuration.ObjectFactory packages to latest versions, which include SourceLink.

----

**Note:** Release notes in the above format are not available for earlier versions of
RockLib.Diagnostics. What follows below are the original release notes.

----

## 1.0.6

Adds net5.0 target and updates dependencies.

## 1.0.5

Updates RockLib.Configuration.ObjectFactory for a bug fix.

## 1.0.4

Adds icon to project and nuget package.

## 1.0.3

- Updates to align with nuget conventions.
- Updates dependency packages.

## 1.0.2

- Adds support for rocklib_diagnostics config section.
- Adds ConfigSection attribute for the Rockifier tool.

## 1.0.1

Adds a RockLib.Diagnostics.DefaultTraceListener class for .NET Standard 1.1 only because the System.Diagnostics.DefaultTraceListener class from the System.Diagnostics.TraceSource package that targets .NET Standard 1.1 doesn't actually do anything - it's just a placeholder with no behavior. The RockLib.Diagnostics.DefaultTraceListener class behaves just like System.Diagnostics.DefaultTraceListener from .NET Framework and .NET Core 2.0. and is used as the default type of System.Diagnostics.TraceListener in the CreateDiagnosticsSettings extension method.

## 1.0.0

Initial release.
