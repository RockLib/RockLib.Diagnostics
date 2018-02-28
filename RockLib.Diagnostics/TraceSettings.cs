using System.Collections.Generic;
using System.Diagnostics;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// Defines settings for the <see cref="Trace"/> static class.
    /// </summary>
    public class TraceSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceSettings"/> class.
        /// </summary>
        /// <param name="autoFlush">The value for the <see cref="Trace"/> static class's <see cref="Trace.AutoFlush"/> property.</param>
        /// <param name="indentSize">The value for the <see cref="Trace"/> static class's <see cref="Trace.IndentSize"/> property.</param>
        /// <param name="useGlobalLock">The value for the <see cref="Trace"/> static class's <see cref="Trace.UseGlobalLock"/> property.</param>
        /// <param name="listeners">A collection of <see cref="TraceListener"/> objects for the <see cref="Trace"/> static class's <see cref="Trace.Listeners"/> property.</param>
        public TraceSettings(bool autoFlush = false, int indentSize = 4, bool useGlobalLock = true, IReadOnlyList<TraceListener> listeners = null)
        {
            AutoFlush = autoFlush;
            IndentSize = indentSize;
            UseGlobalLock = useGlobalLock;
            Listeners = listeners ?? new TraceListener[0];
        }

        /// <summary>
        /// Gets the value for the <see cref="Trace"/> static class's <see cref="Trace.AutoFlush"/> property.
        /// </summary>
        public bool AutoFlush { get; }

        /// <summary>
        /// Gets the value for the <see cref="Trace"/> static class's <see cref="Trace.IndentSize"/> property.
        /// </summary>
        public int IndentSize { get; }

        /// <summary>
        /// Gets the value for the <see cref="Trace"/> static class's <see cref="Trace.UseGlobalLock"/> property.
        /// </summary>
        public bool UseGlobalLock { get; }

        /// <summary>
        /// Gets the collection of <see cref="TraceListener"/> objects for the <see cref="Trace"/> static class's <see cref="Trace.Listeners"/> property.
        /// </summary>
        public IReadOnlyList<TraceListener> Listeners { get; }
    }
}
