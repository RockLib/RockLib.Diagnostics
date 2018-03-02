using FluentAssertions;
using RockLib.Diagnostics;
using System.Diagnostics;
using Xunit;

public partial class TheTracing
{
    public class SettingsProperty
    {
        public class IfNotSetExplicitly
        {
            [Fact]
            public void HasAValueCreatedFromConfigRoot()
            {
                Tracing.Settings.Should().NotBeNull();
                Tracing.Settings.Trace.AutoFlush.Should().Be(true);
                Tracing.Settings.Trace.IndentSize.Should().Be(2);
                Tracing.Settings.Trace.UseGlobalLock.Should().Be(false);

                Tracing.Settings.Trace.Listeners.Count.Should().Be(2);

                Tracing.Settings.Trace.Listeners[0].Should().BeOfType<DefaultTraceListener>();
                Tracing.Settings.Trace.Listeners[1].Should().BeOfType<CustomTraceListener>();

                var listener1 = (DefaultTraceListener)Tracing.Settings.Trace.Listeners[0];
                var listener2 = (CustomTraceListener)Tracing.Settings.Trace.Listeners[1];

                listener1.Name.Should().Be("test_listener1");
                listener1.LogFileName.Should().Be("test_listener1.log");

                listener2.Name.Should().Be("test_listener2");
                listener2.Foo.Should().Be("bar");

                Tracing.Settings.Sources.Count.Should().Be(2);

                Tracing.Settings.Sources[0].Name.Should().Be("test_source1");
                Tracing.Settings.Sources[1].Name.Should().Be("test_source2");

                Tracing.Settings.Sources[0].Switch.DisplayName.Should().Be("test_source1_switch");
                Tracing.Settings.Sources[0].Switch.Level.Should().Be(SourceLevels.All);

                Tracing.Settings.Sources[1].Switch.DisplayName.Should().Be("test_source2_switch");
                Tracing.Settings.Sources[1].Switch.Level.Should().Be(SourceLevels.Warning);

                Tracing.Settings.Sources[0].Listeners.Count.Should().Be(2);
                Tracing.Settings.Sources[1].Listeners.Count.Should().Be(2);

                Tracing.Settings.Sources[0].Listeners[0].Should().BeOfType<DefaultTraceListener>();
                Tracing.Settings.Sources[0].Listeners[1].Should().BeOfType<CustomTraceListener>();

                Tracing.Settings.Sources[1].Listeners[0].Should().BeOfType<DefaultTraceListener>();
                Tracing.Settings.Sources[1].Listeners[1].Should().BeOfType<CustomTraceListener>();

                var listener3 = (DefaultTraceListener)Tracing.Settings.Sources[0].Listeners[0];
                var listener4 = (CustomTraceListener)Tracing.Settings.Sources[0].Listeners[1];

                var listener5 = (DefaultTraceListener)Tracing.Settings.Sources[1].Listeners[0];
                var listener6 = (CustomTraceListener)Tracing.Settings.Sources[1].Listeners[1];

                listener3.Name.Should().Be("test_listener3");
                listener3.LogFileName.Should().Be("test_listener3.log");

                listener4.Name.Should().Be("test_listener4");
                listener4.Foo.Should().Be("baz");

                listener5.Name.Should().Be("test_listener5");
                listener5.LogFileName.Should().Be("test_listener5.log");

                listener6.Name.Should().Be("test_listener6");
                listener6.Foo.Should().Be("qux");
            }
        }
    }
}
