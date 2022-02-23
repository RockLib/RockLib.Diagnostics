using FluentAssertions;
using Xunit;

namespace RockLib.Diagnostics.UnitTests
{
    public class DebugTraceListenerTests
    {
        [Fact]
        public void ConstructorSetsDefaultName()
        {
            using var traceListener = new DebugTraceListener();

            traceListener.Name.Should().Be("");
        }

        [Fact]
        public void ConstructorSetsSpecifiedName()
        {
            using var traceListener = new DebugTraceListener("TestName");

            traceListener.Name.Should().Be("TestName");
        }
    }
}
