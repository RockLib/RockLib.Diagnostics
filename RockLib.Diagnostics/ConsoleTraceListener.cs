using System;
using System.Diagnostics;
using System.IO;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// A <see cref="TraceListener"/> that outputs to console.
    /// </summary>
    public class ConsoleTraceListener : TraceListener
    {
        /// <summary>
        /// Defines the types of output streams used by <see cref="ConsoleTraceListener"/>.
        /// </summary>
        public enum Output
        {
            /// <summary>
            /// The <see cref="ConsoleTraceListener"/> will write to the standard output stream.
            /// </summary>
            StdOut,

            /// <summary>
            /// The <see cref="ConsoleTraceListener"/> will write to the standard error stream.
            /// </summary>
            StdErr
        }

        private readonly TextWriter _consoleWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleTraceListener"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="ConsoleTraceListener"/>.</param>
        /// <param name="output">The type of output stream to use.</param>
        public ConsoleTraceListener(string? name = null, Output output = Output.StdOut)
            : base(name)
        {
            switch (output)
            {
                case Output.StdOut:
                    _consoleWriter = Console.Out;
                    break;
                case Output.StdErr:
                    _consoleWriter = Console.Error;
                    break;
                default:
                    throw new ArgumentException($"Output stream is not defined: {output}.", nameof(output));
            }
        }

        /// <summary>
        /// Writes the specified message to console.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string? message) =>
            _consoleWriter.Write(message);

        /// <summary>
        /// Writes the specified message to console, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string? message) =>
            _consoleWriter.WriteLine(message);
    }
}
