using FluentAssertions;
using System;
using Xunit;

namespace RockLib.Diagnostics.UnitTests.TracingTest
{
    public static class SettingsProperty
    {
        static SettingsProperty() => TracingTestSettings.Initialize();
    }

    public class AfterTheGetterIsCalled
    {
        static AfterTheGetterIsCalled() => TracingTestSettings.Initialize();

        [Fact]
        public void CallingTheSetterThrows()
        {
            var dummy = Tracing.Settings;

            var ex = Assert.Throws<InvalidOperationException>(() =>
                Tracing.Settings = new DiagnosticsSettings());

            ex.Message.Should().Be("Settings have already been set.");
        }
    }
}
