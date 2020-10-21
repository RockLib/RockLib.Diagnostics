using FluentAssertions;
#if !NETCOREAPP1_1
using Moq;
using RockLib.Dynamic;
using System;
using System.IO;
#endif
using Xunit;

namespace RockLib.Diagnostics.UnitTests
{
    public class ConsoleTraceListenerTests
    {
        [Fact(DisplayName = "Constructor sets default name and output")]
        public void ConstructorHappyPath1()
        {
            var traceListener = new ConsoleTraceListener();

            traceListener.Name.Should().Be("");

#if !NETCOREAPP1_1
            TextWriter consoleWriter = traceListener.Unlock()._consoleWriter;

            consoleWriter.Should().BeSameAs(Console.Out);
#endif
        }

        [Fact(DisplayName = "Constructor sets specified name and output")]
        public void ConstructorHappyPath2()
        {
            var traceListener = new ConsoleTraceListener("TestName", ConsoleTraceListener.Output.StdErr);

            traceListener.Name.Should().Be("TestName");

#if !NETCOREAPP1_1
            TextWriter consoleWriter = traceListener.Unlock()._consoleWriter;

            consoleWriter.Should().BeSameAs(Console.Error);
#endif
        }


#if !NETCOREAPP1_1
        [Fact(DisplayName = "Constructor throws if output parameter is invalid")]
        public void ConstructorSadPath()
        {
            Action act = () => new ConsoleTraceListener(output: (ConsoleTraceListener.Output)(-1));

            act.Should().ThrowExactly<ArgumentException>().WithMessage("Output stream is not defined: -1.*output");
        }

        [Fact(DisplayName = "Write passes message to _consoleWriter.Write")]
        public void WriteHappyPath()
        {
            var traceListener = new ConsoleTraceListener();

            var mockTextWriter = new Mock<TextWriter>();

            traceListener.Unlock()._consoleWriter = mockTextWriter.Object;

            traceListener.Write("Test message");

            mockTextWriter.Verify(m => m.Write("Test message"), Times.Once());
        }

        [Fact(DisplayName = "WriteLine passes message to _consoleWriter.WriteLine")]
        public void WriteLineHappyPath()
        {
            var traceListener = new ConsoleTraceListener();

            var mockTextWriter = new Mock<TextWriter>();

            traceListener.Unlock()._consoleWriter = mockTextWriter.Object;

            traceListener.WriteLine("Test message");

            mockTextWriter.Verify(m => m.WriteLine("Test message"), Times.Once());
        }
#endif
    }
}
