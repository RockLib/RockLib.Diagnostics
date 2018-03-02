using FluentAssertions;
using RockLib.Diagnostics;
using System;
using Xunit;

public partial class TheTracing
{
    public class SettingsProperty
    {
        static SettingsProperty() => TracingTestSettings.Initialize();

        public class AfterTheGetterIsCalled
        {
            static AfterTheGetterIsCalled() => TracingTestSettings.Initialize();

            [Fact]
            public void CallingTheSetterThrows()
            {
                var dummy = Tracing.Settings;

                var ex = Assert.Throws<InvalidOperationException>(() =>
                    Tracing.Settings = new DiagnosticsSettings());

                ex.Message.Should().Be(
                    "Setting the value of a Semimutable object is not permitted after it has been locked.");
            }
        }
    }
}
