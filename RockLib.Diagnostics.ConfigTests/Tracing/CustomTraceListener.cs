using System;
using System.Diagnostics;
public class CustomTraceListener : TraceListener
{
    public CustomTraceListener()
    {

    }
    public string Foo { get; set; }
    public override void Write(string message) => Console.Write(message);
    public override void WriteLine(string message) => Console.WriteLine(message);
}
