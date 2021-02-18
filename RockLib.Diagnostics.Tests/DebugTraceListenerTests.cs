using FluentAssertions;
using Xunit;

namespace RockLib.Diagnostics.UnitTests
{
    public class DebugTraceListenerTests
    {
        [Fact(DisplayName = "Constructor sets default name")]
        public void ConstructorHappyPath1()
        {
            var traceListener = new DebugTraceListener();

            traceListener.Name.Should().Be("");
        }

        [Fact(DisplayName = "Constructor sets specified name")]
        public void ConstructorHappyPath2()
        {
            var traceListener = new DebugTraceListener("TestName");

            traceListener.Name.Should().Be("TestName");
        }
    }
}
