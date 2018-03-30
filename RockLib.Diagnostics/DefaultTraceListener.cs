#if NETSTANDARD1_6
using System;
using System.Diagnostics;
using System.IO;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// Provides the default output methods and behavior for tracing.
    /// </summary>
    /// <remarks>
    /// This class is defined for .NET Standard 1.6 only because its
    /// <see cref="System.Diagnostics.DefaultTraceListener"/> implementation doesn't actually
    /// do anything - it is just an empty stub. Other targets define a fully functional implementation
    /// of <see cref="System.Diagnostics.DefaultTraceListener"/>
    /// </remarks>
    public class DefaultTraceListener : TraceListener
    {
        /// <summary>
        /// Gets or sets the name of a log file to write trace or debug messages to.
        /// </summary>
        public string LogFileName { get; set; }

        /// <inheritdoc />
        public override void Write(string message)
        {
            Write(message, false);
        }

        /// <inheritdoc />
        public override void WriteLine(string message)
        {
            Write(message, true);
            NeedIndent = true;
        }

        private void Write(string message, bool useWriteLine)
        {
            if (NeedIndent)
                WriteIndent();

            if (!string.IsNullOrWhiteSpace(LogFileName))
            {
                try
                {
                    using (var stream = new FileInfo(LogFileName).Open(FileMode.OpenOrCreate))
                    using (var streamWriter = new StreamWriter(stream))
                    {
                        stream.Position = stream.Length;
                        if (useWriteLine)
                            streamWriter.WriteLine(message);
                        else
                            streamWriter.Write(message);
                    }
                }
                catch
                {
                }
            }
        }
    }
}
#endif
