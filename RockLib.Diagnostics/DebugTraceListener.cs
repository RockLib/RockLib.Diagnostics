#define DEBUG
using System.Diagnostics;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// A <see cref="TraceListener"/> that outputs to debug.
    /// </summary>
    public class DebugTraceListener : TraceListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugTraceListener"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="DebugTraceListener"/>.</param>
        public DebugTraceListener(string? name = null)
            : base(name)
        {
        }

        /// <summary>
        /// Writes the specified message to debug.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string? message) =>
            Debug.Write(message);

        /// <summary>
        /// Writes the specified message to debug, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string? message) =>
            Debug.WriteLine(message);
    }
}
