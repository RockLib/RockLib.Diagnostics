using FluentAssertions;
using Xunit;

namespace RockLib.Diagnostics.UnitTests
{
    public class DebugTraceListenerTests
    {
        [Fact]
        public void ConstructorSetsDefaultName()
        {
            var traceListener = new DebugTraceListener();

            traceListener.Name.Should().Be("");

            traceListener.Dispose();
        }

        [Fact]
        public void ConstructorSetsSpecifiedName()
        {
            var traceListener = new DebugTraceListener("TestName");

            traceListener.Name.Should().Be("TestName");

            traceListener.Dispose();
        }
    }
}
