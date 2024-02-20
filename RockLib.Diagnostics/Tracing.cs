using Microsoft.Extensions.Configuration;
using RockLib.Configuration;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// Defines methods for configuring the <see cref="Trace"/> static class and retrieving
    /// <see cref="TraceSource"/> objects.
    /// </summary>
#pragma warning disable CA1724
    public static class Tracing
#pragma warning disable CA1724
    {
        // Since we took out RockLib.Threading, we don't have access to SoftLock,
        // but we really don't need it either. Essentially this is inlining what we need.
        private const int _lockNotAcquired = 0;
        private const int _lockAcquired = 1;
        private static int _lock;

        /// <summary>
        /// Defines the name of the section underneath <see cref="Config.Root"/> that determine the default value
        /// of the <see cref="Settings"/> property.
        /// </summary>
        public const string DiagnosticsSectionName = "rocklib.diagnostics";

        /// <summary>
        /// Defines the underscore based name of the section underneath <see cref="Config.Root"/> that determine the default value
        /// of the <see cref="Settings"/> property.
        /// </summary>
        public const string DiagnosticsUnderscoreSectionName = "rocklib_diagnostics";

        private static readonly ConcurrentDictionary<string, TraceSource> _traceSources = new ConcurrentDictionary<string, TraceSource>();

        // We no longer have Semimutable...but we don't need it either,
        // this should be sufficient.
        private static bool _settingsSet;
        private static DiagnosticsSettings? _settings;


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
        public static DiagnosticsSettings? Settings
        {
            get
            {
                if (!_settingsSet)
                {
                    _settings = GetDefaultDiagnosticsSettings();
                    _settingsSet = true;
                }
                return _settings;
            }
            set
            {
                if (_settingsSet)
                {
                    throw new InvalidOperationException("Settings have already been set.");
                }

                _settings = value;
                _settingsSet = true;
            }
        }

        /// <summary>
        /// Sets the <see cref="Trace.AutoFlush"/>, <see cref="Trace.IndentSize"/>, and
        /// <see cref="Trace.UseGlobalLock"/> properties according to the values of the
        /// <see cref="Settings"/> property. If this method has already been called, it
        /// does nothing.
        /// </summary>
        public static void ConfigureTrace()
        {
            if (!(Interlocked.Exchange(ref _lock, _lockAcquired) == _lockNotAcquired)) return;

            if (Settings?.Trace is not null)
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
                if (Settings?.Sources?.FirstOrDefault(s => s.Name == sourceName) is TraceSource traceSource)
                    return traceSource;
                return new TraceSource(name);
            });
        }

        private static DiagnosticsSettings? GetDefaultDiagnosticsSettings() =>
            Config.Root!.GetCompositeSection(DiagnosticsUnderscoreSectionName, DiagnosticsSectionName).CreateDiagnosticsSettings();
    }
}
