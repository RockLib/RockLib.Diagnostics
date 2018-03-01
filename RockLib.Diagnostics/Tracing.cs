using Microsoft.Extensions.Configuration;
using RockLib.Configuration;
using RockLib.Immutable;
using RockLib.Threading;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// Defines methods for configuring the <see cref="Trace"/> static class and retrieving
    /// <see cref="TraceSource"/> objects.
    /// </summary>
    public static class Tracing
    {
        /// <summary>
        /// Defines the name of the section underneath <see cref="Config.Root"/> that determine the default value
        /// of the <see cref="Settings"/> property.
        /// </summary>
        public const string DiagnosticsSectionName = "rocklib.diagnostics";

        private static readonly SoftLock _configureTraceLock = new SoftLock();

        private static readonly ConcurrentDictionary<string, TraceSource> _traceSources = new ConcurrentDictionary<string, TraceSource>();

        private static readonly Semimutable<DiagnosticsSettings> _settings =
            new Semimutable<DiagnosticsSettings>(GetDefaultDiagnosticsSettings);

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticsSettings"/> used internally by the <see cref="ConfigureTrace"/>
        /// and <see cref="GetTraceSource(string)"/> methods.
        /// </summary>
        /// <exception cref="ArgumentNullException">If set to null.</exception>
        /// <remarks>
        /// The default value for this property is created by taking the value of <see cref="Config.Root"/>,
        /// calling <see cref="IConfiguration.GetSection(string)"/> method on it with the name
        /// <see cref="DiagnosticsSectionName"/>, and applying the
        /// <see cref="ConfigurationExtensions.CreateDiagnosticsSettings(IConfiguration)"/>
        /// extension method to the sub-section.
        /// </remarks>
        public static DiagnosticsSettings Settings
        {
            get => _settings.Value;
            set => _settings.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Sets the <see cref="Trace.AutoFlush"/>, <see cref="Trace.IndentSize"/>, and
        /// <see cref="Trace.UseGlobalLock"/> properties according to the values of the
        /// <see cref="Settings"/> property. If this method has already been called, it
        /// does nothing.
        /// </summary>
        public static void ConfigureTrace()
        {
            if (!_configureTraceLock.TryAcquire()) return;

            if (Settings.Trace != null)
            {
                var traceSettings = Settings.Trace;

                Trace.AutoFlush = traceSettings.AutoFlush;
                Trace.IndentSize = traceSettings.IndentSize;
                Trace.UseGlobalLock = traceSettings.UseGlobalLock;

                if (traceSettings.Listeners.Count > 0)
                {
                    Trace.Listeners.Clear();
                    foreach (var listener in traceSettings.Listeners)
                        Trace.Listeners.Add(listener);
                }
            }
        }

        /// <summary>
        /// Gets a <see cref="TraceSource"/> object with the specified name. If the value of the
        /// <see cref="Settings"/> property defines a <see cref="TraceSource"/> in its
        /// <see cref="DiagnosticsSettings.Sources"/> property whose name matches the <paramref name="name"/>
        /// parameter, then that <see cref="TraceSource"/> is returned. Otherwise, a default instance
        /// of <see cref="TraceSource"/> with the speified name is returned.
        /// </summary>
        /// <param name="name">The name of the source to retrieve.</param>
        /// <returns>A <see cref="TraceSource"/> object with the specified name.</returns>
        /// <remarks>
        /// This method always returns the same instance of <see cref="TraceSource"/> given the same value of
        /// the <paramref name="name"/> property.
        /// </remarks>
        public static TraceSource GetTraceSource(string name)
        {
            return _traceSources.GetOrAdd(name, sourceName =>
            {
                if (Settings.Sources?.FirstOrDefault(s => s.Name == sourceName) is TraceSource traceSource)
                    return traceSource;
                return new TraceSource(name);
            });
        }

        private static DiagnosticsSettings GetDefaultDiagnosticsSettings() =>
            Config.Root.GetSection(DiagnosticsSectionName).CreateDiagnosticsSettings();
    }
}
