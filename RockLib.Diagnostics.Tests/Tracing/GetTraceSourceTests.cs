using FluentAssertions;
using Xunit;

namespace RockLib.Diagnostics.UnitTests.TracingTest
{
    public static class GetTraceSourceMethod
    {
        static GetTraceSourceMethod() => TracingTestSettings.Initialize();
    }
    public class GivenAMatchInTracingSources
    {
        [Fact]
        public void TheMatchingTraceSourceIsReturned()
        {
            var traceSource1 = Tracing.GetTraceSource(TracingTestSettings.TraceSource1Name);
            traceSource1.Should().BeSameAs(TracingTestSettings.TraceSource1);

            var traceSource2 = Tracing.GetTraceSource(TracingTestSettings.TraceSource2Name);
            traceSource2.Should().BeSameAs(TracingTestSettings.TraceSource2);
        }
    }

    public class GivenNoMatchInTracingSources
    {
        [Fact]
        public void ANewTraceSourceIsReturned()
        {
            var unknownTraceSource = Tracing.GetTraceSource("unknown_trace_source");

            TracingTestSettings.TraceSources.Should().NotContain(unknownTraceSource);
            unknownTraceSource.Name.Should().Be("unknown_trace_source");
        }

        [Fact]
        public void TheSameNonMatchIsReturnedEveryTime()
        {
            var traceSourceA = Tracing.GetTraceSource("some_trace_source");
            var traceSourceB = Tracing.GetTraceSource("some_trace_source");

            traceSourceA.Should().BeSameAs(traceSourceB);
        }
    }
}
