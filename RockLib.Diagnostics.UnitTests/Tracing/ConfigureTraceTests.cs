using FluentAssertions;
using RockLib.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

public partial class TheTracing
{
    public class ConfigureTraceMethod
    {
        private static readonly bool InitialAutoFlush;
        private static readonly int InitialIndentSize;
        private static readonly bool InitialUseGlobalLock;
        private static readonly IReadOnlyList<TraceListener> InitialTraceListeners;

        static ConfigureTraceMethod()
        {
             TracingTestSettings.Initialize();

            InitialAutoFlush = Trace.AutoFlush;
            InitialIndentSize = Trace.IndentSize;
            InitialUseGlobalLock = Trace.UseGlobalLock;
            InitialTraceListeners = Trace.Listeners.Cast<TraceListener>().ToArray();
        }

        [Fact]
        public void SetsTheTraceAutoFlushProperty()
        {
            Tracing.ConfigureTrace();

            InitialAutoFlush.Should().Be(!TracingTestSettings.AutoFlush);
            Trace.AutoFlush.Should().Be(TracingTestSettings.AutoFlush);
        }

        [Fact]
        public void SetsTheTraceIndentSizeProperty()
        {
            Tracing.ConfigureTrace();

            InitialIndentSize.Should().NotBe(TracingTestSettings.IndentSize);
            Trace.IndentSize.Should().Be(TracingTestSettings.IndentSize);

        }

        [Fact]
        public void SetsTheTraceUseGlobalLockProperty()
        {
            Tracing.ConfigureTrace();

            InitialUseGlobalLock.Should().Be(!TracingTestSettings.UseGlobalLock);
            Trace.UseGlobalLock.Should().Be(TracingTestSettings.UseGlobalLock);

        }

        [Fact]
        public void SetsTheTraceListenersProperty()
        {
            Tracing.ConfigureTrace();

            InitialTraceListeners.Should().NotBeEquivalentTo(TracingTestSettings.TraceListeners);
            Trace.Listeners.Should().BeEquivalentTo(TracingTestSettings.TraceListeners);
        }
    }
}
