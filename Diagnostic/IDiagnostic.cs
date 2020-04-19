using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostic
{
    public interface IDiagnostic
    {
        void trace(string message);
        void traceError(string message);
    }
}
