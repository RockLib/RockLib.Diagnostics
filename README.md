# RockLib.Diagnostics [![Build status](https://ci.appveyor.com/api/projects/status/k7ojdrc1mwifri96?svg=true)](https://ci.appveyor.com/project/RockLib/rocklib-diagnostics)

*Makes configuring tracing easy and standardized for .NET Core, .NET Standard, and .NET Framework.*

```powershell
PM> Install-Package RockLib.Diagnostics
```

## Overview

When it comes to tracing, there are two main concerns. The first concern is from the perespective of a library that needs to write trace messages. It needs to know that tracing has been configured *before* it starts writing trace messages. The other concern is from the perspective of an application that is using a library that writes trace messages. It needs to be able to run with tracing deactivated for most of the time. But, when the need arises, it should be able to be run with tracing activated without needing to recompile.

In old .NET Framework, configuring tracing was done for you automatically when you added a `<system.diagnostics>` section to your app.config or web.config. Since app.config and web.config no exist in .NET Core, it became clear that a standardized mechanism for configuring tracing was needed. RockLib.Diagnostics is that mechanism. For trace-producing libraries, it provides methods that ensure that tracing has been configured. And it provides mechanisms for consumers of these trace-producing libraries to configure tracing settings exactly as they need.

## RockLib.Diagnostics.Tracing

The static `RockLib.Diagnostics.Tracing` class has two methods, `ConfigureTrace()` and `GetTraceSource(string name)`, and one property, `Settings`. The behavior of each of the methods is determined by the value of the `Settings` property.

### Tracing.ConfigureTrace

All classes that write trace messages using the `System.Diagnostics.Trace` class should make a call to `RockLib.Diagnostics.Tracing.ConfigureTrace()` in their static constructor. It ensures that `Trace` has its `AutoFlush`, `IndentSize`, `UseGlobalLock`, and `Listeners` properties configured according to the `Tracing.Settings` property. Note that this method is thread-safe and can be called multiple times (only the first time calling it has any effect however).

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
Tracing.Settings = new DiagnosticsSettings(
    trace: new TraceSettings(
        autoFlush: false,    // These are
        indentSize: 4,       // the default
        useGlobalLock: true, // values.
        listeners: new System.Diagnostics.TraceListener[]
        {
            new System.Diagnostics.DefaultTraceListener
            {
                Name = "my_trace_listener",
                LogFileName = "my_trace_listener.log"
            }
        }),
    sources: new System.Diagnostics.TraceSource[]
    {
        new System.Diagnostics.TraceSource(name: "my_trace_source")
        {
            Switch = new System.Diagnostics.SourceSwitch(name: "my_trace_source_switch")
            {
                Level = System.Diagnostics.SourceLevels.Verbose
            },
            Listeners =
            {
                new System.Diagnostics.DefaultTraceListener
                {
                    Name = "my_trace_source_listener",
                    LogFileName = "my_trace_source_listener.log"
                }
            }
        }
    });
```

When not set explicitly, the `Settings` property value is automatically obtained as follows:

```c#
RockLib.Configuration.Config.Root
    .GetSection(RockLib.Diagnostics.Tracing.DiagnosticsSectionName)
    .CreateDiagnosticsSettings()
```

The `CreateDiagnosticsSettings` extension method looks like this:

```c#
var defaultTypes = new DefaultTypes
{
    { typeof(TraceListener), typeof(DefaultTraceListener) },
    { typeof(TraceFilter), typeof(EventTypeFilter) }
};
return configuration.Create<DiagnosticsSettings>(defaultTypes);
```

See the [RockLib.Configuration.ObjectFactory](https://github.com/RockLib/RockLib.Configuration/tree/develop/RockLib.Configuration.ObjectFactory) project for details on config formatting.

An `appsettings.json` file for an app with its Tracing automatically configured might look like this (note that this configuration and the programmatic example above produce the same settings):

```json
{
    "rocklib.diagnostics": {
        "trace": {
            "autoFlush": false,
            "indentSize": 4,
            "useGlobalLock": true,
            "listeners": {
                "name": "my_trace_listener",
                "logFileName": "my_trace_listener.log"
            }
        },
        "sources": {
            "name": "my_trace_source",
            "switch": {
                "name": "my_trace_source_switch",
                "level": "All"
            },
            "listeners": {
                "name": "my_trace_source_listener",
                "logFileName": "my_trace_source_listener.log"
            }
        }
    }
}

```

#### ASP.NET Core Applications

In order to enable tracing automatically from configuration, ASP.NET Core applications need be sure to call `RockLib.Configuration.Config.SetCurrent(Microsoft.Extensions.Configuration.IConfiguration)` from their `Startup` class's constructor.

```c#
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
    Config.SetRoot(Configuration);
}
```
