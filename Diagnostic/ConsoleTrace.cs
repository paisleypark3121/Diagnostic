using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostic
{
    public class ConsoleTrace : IDiagnostic
    {
        internal void consoleTrace(string message)
        {
            Console.WriteLine(message);
        }

        public void trace(string message)
        {
            consoleTrace(message);
        }

        public void traceError(string message)
        {
            consoleTrace(message);
        }
    }
}
