using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnomalyWeb.Models
{
    public class Response
    {
        public string timestamp { get; set; }
        public int value { get; set; }
        public bool anomaly { get; set; }
        public string expectedValue { get; set; }

        public Response(DateTime timestamp, int value, AnomalyResponse anomalyResponse, int index)
        {
            this.timestamp = timestamp.ToString("MM/dd/yyyy HH:mm:ss");
            this.value = value;
            this.anomaly = anomalyResponse.isAnomaly[index]; ;
            this.expectedValue = anomalyResponse.expectedValues[index];
        }
    }
}
