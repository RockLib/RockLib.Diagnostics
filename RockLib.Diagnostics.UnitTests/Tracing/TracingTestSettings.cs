using FluentAssertions;
using RockLib.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;

internal static class TracingTestSettings
{
    public const bool AutoFlush = true;
    public const int IndentSize = 2;
    public const bool UseGlobalLock = false;
    public const string TraceSource1Name = "test_source1";
    public const string TraceSource2Name = "test_source2";
    public static DefaultTraceListener TraceListener1 { get; }
    public static DefaultTraceListener TraceListener2 { get; }
    public static IReadOnlyList<TraceListener> TraceListeners { get; }
    public static TraceSettings TraceSettings { get; }
    public static TraceSource TraceSource1 { get; }
    public static TraceSource TraceSource2 { get; }
    public static IReadOnlyList<TraceSource> TraceSources { get; }
    public static DiagnosticsSettings DiagnosticsSettings { get; }

    static TracingTestSettings()
    {
        TraceListener1 = new DefaultTraceListener
        {
            Name = "test_listener1",
            LogFileName = "test_listener1.log"
        };

        TraceListener2 = new DefaultTraceListener
        {
            Name = "test_listener2",
            LogFileName = "test_listener2.log"
        };

        TraceListeners = new[] { TraceListener1, TraceListener2 };

        TraceSettings = new TraceSettings(
            AutoFlush,
            IndentSize,
            UseGlobalLock,
            TraceListeners);

        TraceSource1 = new TraceSource(TraceSource1Name);
        TraceSource2 = new TraceSource(TraceSource2Name);

        TraceSources = new TraceSource[]
        {
            TraceSource1,
            TraceSource2
        };

        DiagnosticsSettings = new DiagnosticsSettings(TraceSettings, TraceSources);

        Tracing.Settings = DiagnosticsSettings;

        // Immediately lock the Settings property by reading it.
        Tracing.Settings.Should().BeSameAs(DiagnosticsSettings);
    }

    /// <summary>Ensures that the static constructor has run.</summary>
    public static void Initialize() { }
}
