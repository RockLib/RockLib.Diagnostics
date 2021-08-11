# RockLib.Diagnostics Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
