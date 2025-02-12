using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace RockLib.Diagnostics.UnitTests
{
    public class ConsoleTraceListenerTests
    {
        [Fact]
        public void ConstructorSetsDefaultNameAndOutput()
        {
            var traceListener = new ConsoleTraceListener();

            traceListener.Name.Should().Be("");

            var consoleWriter = (TextWriter)typeof(ConsoleTraceListener)
                .GetField("_consoleWriter", BindingFlags.Instance | BindingFlags.NonPublic)!
                .GetValue(traceListener)!;

            consoleWriter.Should().BeSameAs(Console.Out);

            traceListener.Dispose();
        }

        [Fact]
        public void ConstructorSetsSpecifiedNameAndOutput()
        {
            var traceListener = new ConsoleTraceListener("TestName", ConsoleTraceListener.Output.StdErr);

            traceListener.Name.Should().Be("TestName");

            var consoleWriter = (TextWriter)typeof(ConsoleTraceListener)
                .GetField("_consoleWriter", BindingFlags.Instance | BindingFlags.NonPublic)!
                .GetValue(traceListener)!;

            consoleWriter.Should().BeSameAs(Console.Error);

            traceListener.Dispose();
        }

        [Fact]
        public void ConstructorTrowsIfOutputParameterIsInvalid()
        {
#pragma warning disable CA1806 // Do not ignore method results
            Action act = () => new ConsoleTraceListener(output: (ConsoleTraceListener.Output)(-1));
#pragma warning restore CA1806 // Do not ignore method results

            act.Should().ThrowExactly<ArgumentException>().WithMessage("Output stream is not defined: -1.*output*");
        }

        [Fact]
        public void WritePassesMessage()
        {
            var traceListener = new ConsoleTraceListener();

            var mockTextWriter = new Mock<TextWriter>();

            var consoleWriter = typeof(ConsoleTraceListener)
                .GetField("_consoleWriter", BindingFlags.Instance | BindingFlags.NonPublic)!;
            consoleWriter.SetValue(traceListener, mockTextWriter.Object);

            traceListener.Write("Test message");

            mockTextWriter.Verify(m => m.Write("Test message"), Times.Once());

            traceListener.Dispose();
        }

        [Fact]
        public void WriteLinePassesMessage()
        {
            var traceListener = new ConsoleTraceListener();

            var mockTextWriter = new Mock<TextWriter>();

            var consoleWriter = typeof(ConsoleTraceListener)
                .GetField("_consoleWriter", BindingFlags.Instance | BindingFlags.NonPublic)!;
            consoleWriter.SetValue(traceListener, mockTextWriter.Object);

            traceListener.WriteLine("Test message");

            mockTextWriter.Verify(m => m.WriteLine("Test message"), Times.Once());

            traceListener.Dispose();
        }
    }
}
