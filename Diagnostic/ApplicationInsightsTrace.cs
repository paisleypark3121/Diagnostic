using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostic
{
    public class ApplicationInsightsTrace : ITrace
    {
        TelemetryClient client;

        public ApplicationInsightsTrace()
        {
            try
            {
                if (client == null)
                {
                    TelemetryConfiguration configuration = TelemetryConfiguration.Active;
                    client = new TelemetryClient(configuration);
                }
            }
            catch (Exception) { }
        }

        public void trace(string message)
        {
            #region preconditions
            if (client == null)
                throw new Exception("Missing telemetry client");
            if (string.IsNullOrEmpty(message))
                return;
            #endregion

            if (message.StartsWith("{") && message.EndsWith("}"))
            {
                // json package: header property will be placed in "message"
                try
                {
                    JObject _jobject = JsonConvert.DeserializeObject<JObject>(message);
                    var telemetry = new TraceTelemetry();
                    string header = null;
                    foreach (var entry in _jobject)
                    {
                        if (entry.Key == "header")
                            header = entry.Value.ToString();
                        else
                            telemetry.Properties.Add(entry.Key, entry.Value.ToString());
                    }
                    telemetry.Message = header;
                    if (string.IsNullOrEmpty(header))
                        header = "Missing Header";
                    client.TrackTrace(telemetry);
                }
                catch (Exception ex)
                {
                    string error = "Error: " + ex.Message + " - original message: " + message;
                    System.Diagnostics.Trace.TraceInformation(error);
                    client.TrackTrace(error);
                }
            }
            else
                client.TrackTrace(message);
        }

        public void traceError(string message)
        {
            string error = "TraceError: " + message;
            System.Diagnostics.Trace.TraceInformation(error);
            client.TrackTrace(error);
        }
    }
}
