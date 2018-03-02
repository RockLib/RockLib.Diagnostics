using FluentAssertions;
using Microsoft.Extensions.Configuration;
using RockLib.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

public class TheCreateDiagnosticsSettingsExtensionMethod
{
    [Fact]
    public void UsesRockLibConfigurationObjectFactory()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "trace:autoFlush", "true" },
                { "trace:listeners:type", "System.Diagnostics.DefaultTraceListener, System.Diagnostics.TraceSource" },
                { "trace:listeners:value:name", "listener1" },
                { "trace:listeners:value:logFileName", "listener1.log" },
                { "trace:listeners:value:filter:type", "System.Diagnostics.EventTypeFilter, System.Diagnostics.TraceSource" },
                { "trace:listeners:value:filter:value:level", "Warning" },
                { "sources:name", "source1" },
                { "sources:switch:name", "sourceSwitch1" },
                { "sources:switch:level", "Error" },
                { "sources:listeners:type", "System.Diagnostics.DefaultTraceListener, System.Diagnostics.TraceSource" },
                { "sources:listeners:value:name", "listener2" },
                { "sources:listeners:value:logFileName", "listener2.log" },
                { "sources:listeners:value:filter:type", "System.Diagnostics.EventTypeFilter, System.Diagnostics.TraceSource" },
                { "sources:listeners:value:filter:value:level", "Critical" },
            })
            .Build();

        var diagnosticsSettings = config.CreateDiagnosticsSettings();

        var trace = diagnosticsSettings.Trace;
        trace.AutoFlush.Should().BeTrue();
        trace.Listeners.Count.Should().Be(1);
        trace.Listeners[0].Should().BeOfType<DefaultTraceListener>();
        var traceListener = (DefaultTraceListener)trace.Listeners[0];
        traceListener.Name.Should().Be("listener1");
        traceListener.LogFileName.Should().Be("listener1.log");
        traceListener.Filter.Should().BeOfType<EventTypeFilter>();
        var filter = (EventTypeFilter)traceListener.Filter;
        filter.EventType.Should().Be(SourceLevels.Warning);

        diagnosticsSettings.Sources.Count.Should().Be(1);
        var source = diagnosticsSettings.Sources[0];
        source.Name.Should().Be("source1");
        source.Switch.DisplayName.Should().Be("sourceSwitch1");
        source.Switch.Level.Should().Be(SourceLevels.Error);
        source.Listeners.Count.Should().Be(1);
        source.Listeners[0].Should().BeOfType<DefaultTraceListener>();
        traceListener = (DefaultTraceListener)source.Listeners[0];
        traceListener.Name.Should().Be("listener2");
        traceListener.LogFileName.Should().Be("listener2.log");
        traceListener.Filter.Should().BeOfType<EventTypeFilter>();
        filter = (EventTypeFilter)traceListener.Filter;
        filter.EventType.Should().Be(SourceLevels.Critical);
    }

    [Fact]
    public void UsesDefaultTraceListenerAsTheDefaultTypeOfTraceListener()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "trace:autoFlush", "true" },
                { "trace:listeners:type", "System.Diagnostics.DefaultTraceListener, System.Diagnostics.TraceSource" },
                { "trace:listeners:name", "listener1" },
                { "trace:listeners:logFileName", "listener1.log" },
                { "trace:listeners:filter:type", "System.Diagnostics.EventTypeFilter, System.Diagnostics.TraceSource" },
                { "trace:listeners:filter:value:level", "Warning" },
                { "sources:name", "source1" },
                { "sources:switch:name", "sourceSwitch1" },
                { "sources:switch:level", "Error" },
                { "sources:listeners:name", "listener2" },
                { "sources:listeners:logFileName", "listener2.log" },
                { "sources:listeners:filter:type", "System.Diagnostics.EventTypeFilter, System.Diagnostics.TraceSource" },
                { "sources:listeners:filter:value:level", "Critical" },
            })
            .Build();

        var diagnosticsSettings = config.CreateDiagnosticsSettings();

        var trace = diagnosticsSettings.Trace;
        trace.AutoFlush.Should().BeTrue();
        trace.Listeners.Count.Should().Be(1);
        trace.Listeners[0].Should().BeOfType<DefaultTraceListener>();
        var traceListener = (DefaultTraceListener)trace.Listeners[0];
        traceListener.Name.Should().Be("listener1");
        traceListener.LogFileName.Should().Be("listener1.log");
        traceListener.Filter.Should().BeOfType<EventTypeFilter>();
        var filter = (EventTypeFilter)traceListener.Filter;
        filter.EventType.Should().Be(SourceLevels.Warning);

        diagnosticsSettings.Sources.Count.Should().Be(1);
        var source = diagnosticsSettings.Sources[0];
        source.Name.Should().Be("source1");
        source.Switch.DisplayName.Should().Be("sourceSwitch1");
        source.Switch.Level.Should().Be(SourceLevels.Error);
        source.Listeners.Count.Should().Be(1);
        source.Listeners[0].Should().BeOfType<DefaultTraceListener>();
        traceListener = (DefaultTraceListener)source.Listeners[0];
        traceListener.Name.Should().Be("listener2");
        traceListener.LogFileName.Should().Be("listener2.log");
        traceListener.Filter.Should().BeOfType<EventTypeFilter>();
        filter = (EventTypeFilter)traceListener.Filter;
        filter.EventType.Should().Be(SourceLevels.Critical);
    }

    [Fact]
    public void UsesEventTypeFilterAsTheDefaultTypeOfTraceFilter()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "trace:autoFlush", "true" },
                { "trace:listeners:type", "System.Diagnostics.DefaultTraceListener, System.Diagnostics.TraceSource" },
                { "trace:listeners:value:name", "listener1" },
                { "trace:listeners:value:logFileName", "listener1.log" },
                { "trace:listeners:value:filter:level", "Warning" },
                { "sources:name", "source1" },
                { "sources:switch:name", "sourceSwitch1" },
                { "sources:switch:level", "Error" },
                { "sources:listeners:type", "System.Diagnostics.DefaultTraceListener, System.Diagnostics.TraceSource" },
                { "sources:listeners:value:name", "listener2" },
                { "sources:listeners:value:logFileName", "listener2.log" },
                { "sources:listeners:value:filter:level", "Critical" },
            })
            .Build();

        var diagnosticsSettings = config.CreateDiagnosticsSettings();

        var trace = diagnosticsSettings.Trace;
        trace.AutoFlush.Should().BeTrue();
        trace.Listeners.Count.Should().Be(1);
        trace.Listeners[0].Should().BeOfType<DefaultTraceListener>();
        var traceListener = (DefaultTraceListener)trace.Listeners[0];
        traceListener.Name.Should().Be("listener1");
        traceListener.LogFileName.Should().Be("listener1.log");
        traceListener.Filter.Should().BeOfType<EventTypeFilter>();
        var filter = (EventTypeFilter)traceListener.Filter;
        filter.EventType.Should().Be(SourceLevels.Warning);

        diagnosticsSettings.Sources.Count.Should().Be(1);
        var source = diagnosticsSettings.Sources[0];
        source.Name.Should().Be("source1");
        source.Switch.DisplayName.Should().Be("sourceSwitch1");
        source.Switch.Level.Should().Be(SourceLevels.Error);
        source.Listeners.Count.Should().Be(1);
        source.Listeners[0].Should().BeOfType<DefaultTraceListener>();
        traceListener = (DefaultTraceListener)source.Listeners[0];
        traceListener.Name.Should().Be("listener2");
        traceListener.LogFileName.Should().Be("listener2.log");
        traceListener.Filter.Should().BeOfType<EventTypeFilter>();
        filter = (EventTypeFilter)traceListener.Filter;
        filter.EventType.Should().Be(SourceLevels.Critical);
    }
}