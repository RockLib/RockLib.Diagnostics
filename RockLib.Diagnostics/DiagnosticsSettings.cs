using System.Collections.Generic;
using System.Diagnostics;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// Defines settings for diagnostics.
    /// </summary>
    public class DiagnosticsSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsSettings"/> class.
        /// </summary>
        /// <param name="trace">Settings for configuring the <see cref="Trace"/> static class.</param>
        /// <param name="sources">A collection of <see cref="TraceSource"/> objects.</param>
        public DiagnosticsSettings(TraceSettings trace = null, IReadOnlyList<TraceSource> sources = null)
        {
            Trace = trace;
            Sources = sources;
        }

        /// <summary>
        /// Gets settings for configuring the <see cref="Trace"/> class.
        /// </summary>
        public TraceSettings Trace { get; }

        /// <summary>
        /// Get a collection of <see cref="TraceSource"/> objects.
        /// </summary>
        public IReadOnlyList<TraceSource> Sources { get; }
    }
}
