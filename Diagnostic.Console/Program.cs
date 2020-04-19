using Diagnostic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosticConsole
{
    class Program
    {
        public static void mainConsole()
        {
            IDiagnostic trace = new ConsoleTrace();
            trace.trace("Hello World");

        }

        public static void mainApplicationInsights()
        {
            //_ = TelemetryConfiguration.Active;
            //TelemetryConfiguration configuration = TelemetryConfiguration.Active;
            //TelemetryClient telemetryClient = new TelemetryClient(configuration);
            //telemetryClient.TrackTrace("Good evening with wait");
            //telemetryClient.Flush(); // force empty of the buffer: the SDK send data each 30 sec. or when the buffer is full

            ApplicationInsightsTrace trace = new ApplicationInsightsTrace();
            trace.trace("try again....");

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                trace.trace("try again...." + i);
                System.Threading.Thread.Sleep(500);
            }

            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("END");
        }

        static void Main(string[] args)
        {
            //mainApplicationInsights();
            mainConsole();
        }
    }
}
