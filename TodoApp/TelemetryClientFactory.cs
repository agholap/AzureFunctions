using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp
{
    public class TelemetryClientFactory : ITelemetryClientFactory
    {
        public virtual TelemetryClient GetClient()
        {
            string key = TelemetryConfiguration.Active.InstrumentationKey = System.Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY", EnvironmentVariableTarget.Process);
            TelemetryClient client = new TelemetryClient()
            {
                InstrumentationKey = key
            };

            return client;
        }
    }

    public interface ITelemetryClientFactory
    {
        TelemetryClient GetClient();
    }
}
