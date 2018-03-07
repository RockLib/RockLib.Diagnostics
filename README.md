# RockLib.Diagnostics [![Build status](https://ci.appveyor.com/api/projects/status/9gra3aiyh2bb8rob?svg=true)](https://ci.appveyor.com/project/bfriesen/rocklib-diagnostics)

_Makes configuring tracing easy and standardized for .NET Core, .NET Standard, and .NET Framework._

```powershell
PM> Install-Package RockLib.Diagnostics
```

## Overview

When it comes to tracing, there are two main concerns. The first concern is from the perespective of a library that needs to write trace messages. It needs to know that tracing has been configured *before* it starts writing trace messages. The other concern is from the perspective of an application that is using a library that writes trace messages. It needs to be able to run with tracing deactivated for most of the time. But, when the need arises, it should be able to be run with tracing activated without needing to recompile.

In old .NET Framework, configuring tracing was done for you automatically when you added a `<system.diagnostics>` section to your app.config or web.config. Since app.config and web.config no exist in .NET Core, it became clear that a standardized mechanism for configuring tracing was needed. RockLib.Diagnostics is that mechanism. For trace-producing libraries, it provides methods that ensure that tracing has been configured. And it provides mechanisms for consumers of these trace-producing libraries to configure tracing settings exactly as they need.

## RockLib.Diagnostics.Tracing

The static `RockLib.Diagnostics.Tracing` class has two methods, `ConfigureTrace()` and `GetTraceSource(string name)`, and one property, `Settings`. The behavior of each of the methods is determined by the value of the `Settings` property.

### Tracing.ConfigureTrace

All classes that write trace messages using the `System.Diagnostics.Trace` class should make a call to `RockLib.Diagnostics.Tracing.ConfigureTrace()` in their static constructor. It ensures that `Trace` has its `AutoFlush`, `IndentSize`, `UseGlobalLock`, and `Listeners` properties configured according to the `Tracing.Settings` property. Note that this method is thread-safe and can be called multiple timmes (only the first time calling it has any effect however).

```c#
using RockLib.Diagnostics;
using System.Diagnostics;

public class YourClass
{
    static YourClass()
    {
        Tracing.ConfigureTrace();
    }

    public YourClass()
    {
        Trace.TraceInformation("Created instance of YourClass.");
    }
}
```

### Tracing.GetTraceSource

Any classes that would create an instance of the `System.Diagnostics.TraceSource` class, should do so using the `RockLib.Diagnostics.Tracing.GetTraceSource(string name)` method. This method will return the first `TraceSource` object from the `Tracing.Settings.Sources` property that has a `TraceSource.Name` property matching the `name` parameter of the method. Otherwise, it returns a default `TraceSource` object with the given name.

### Tracing.Settings

The type of the `Tracing.Settings` property, `DiagnosticsSettings`, has two properties: `Trace` and `Sources`. The `Trace` property contains information used by the `ConfigureTrace` method, while the `Sources` property is used by the `GetTraceSource` method.

This property may be explicitly set at the beginning of an application. However, it cannot be set once already in use.

```c#
static void Main(string[] args)
{
    Tracing.Settings = new DiagnosticsSettings(
        trace: new TraceSettings(
            autoFlush:false,    // These are
            indentSize:4,       // the default
            useGlobalLock:true, // values.
            listeners: new System.Diagnostics.TraceListener[]
            {
                // TODO: instantiate TraceListener objects
            }),
        sources: new System.Diagnostics.TraceSource[]
        {
            // TODO: instantiate TraceSource objects
        });
}
```

When not set explicitly, the `Settings` property value is automatically obtained as follows:

```c#
RockLib.Configuration.Config.Root
    .GetSection(RockLib.Diagnostics.Tracing.DiagnosticsSectionName)
    .CreateDiagnosticsSettings() // Extension method from RockLib.Diagnostics.ConfigurationExtensions
```

The `appsettings.json` file for an app with its Tracing automatically configured looks like this:

```json
{
    "rocklib.diagnostics": {
        "trace": {
            "autoFlush": false,
            "indentSize": 4,
            "useGlobalLock": true,
            "listeners": [
                // TODO: configure TraceListener objects
            ]
        },
        "sources": [
            // TODO: configure TraceSource objects
        ]
    }
}

```