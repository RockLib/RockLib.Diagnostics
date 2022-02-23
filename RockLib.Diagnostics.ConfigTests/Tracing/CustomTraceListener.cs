using System;
using System.Diagnostics;

#pragma warning disable CA1050 // Declare types in namespaces
public class CustomTraceListener : TraceListener
#pragma warning restore CA1050 // Declare types in namespaces
{
        public CustomTraceListener()
        {

        }
        public string? Foo { get; set; }
        public override void Write(string? message) => Console.Write(message);
        public override void WriteLine(string? message) => Console.WriteLine(message);
    }
