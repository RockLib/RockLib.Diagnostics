using FluentAssertions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace RockLib.Diagnostics.UnitTests.TracingTest
{

    public class ConfigureTraceMethod
    {
        private static readonly bool InitialAutoFlush = Trace.AutoFlush;
        private static readonly int InitialIndentSize = Trace.IndentSize;
        private static readonly bool InitialUseGlobalLock = Trace.UseGlobalLock;
        private static readonly IReadOnlyList<TraceListener> InitialTraceListeners = Trace.Listeners.Cast<TraceListener>().ToArray();

        static ConfigureTraceMethod()
        {
            TracingTestSettings.Initialize();
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
