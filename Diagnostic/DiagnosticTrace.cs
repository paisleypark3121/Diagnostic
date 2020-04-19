using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostic
{
    public class DiagnosticTrace : IDiagnostic
    {
        public void trace(string message)
        {
            System.Diagnostics.Trace.TraceInformation(message);
        }

        public void traceError(string message)
        {
            System.Diagnostics.Trace.TraceError(message);
        }
    }
}
