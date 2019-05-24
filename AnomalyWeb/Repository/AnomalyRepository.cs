using AnomalyWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyWeb.Service
{
    public class AnomalyRepository
    {
        const string subscriptionKey = "ANOMALY DETECTOR API KEY";
        const string endpoint = "https://westeurope.api.cognitive.microsoft.com";
        
        const string batchDetectionUrl = "/anomalydetector/v1.0/timeseries/entire/detect";

        public AnomalyResponse DetectAnomaliesBatch(string requestData)
        {
            System.Console.WriteLine("Detecting anomalies as a batch");
            AnomalyResponse response = new AnomalyResponse();
            //construct the request
            var result = Request(
                endpoint,
                batchDetectionUrl,
                subscriptionKey,
                requestData).Result;

            //deserialize the JSON object, and display it
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            System.Console.WriteLine(jsonObj);

            if (jsonObj["code"] != null)
            {
                System.Console.WriteLine($"Detection failed. ErrorCode:{jsonObj["code"]}, ErrorMessage:{jsonObj["message"]}");
            }
            else
            {                
                //Find and display the positions of anomalies in the data set
                response.isAnomaly = jsonObj["isAnomaly"].ToObject<List<bool>>();
                response.expectedValues = jsonObj["expectedValues"].ToObject<List<string>>();

                System.Console.WriteLine("\nAnomalies detected in the following data positions:");
                for (var i = 0; i < response.isAnomaly.Count; i++)
                {
                    if (response.isAnomaly[i])
                    {
                        System.Console.Write(i + ", ");
                    }
                }
                return response;
            }
            return response;
        }

        /// <summary>
        /// Sends a request to the Anomaly Detection API to detect anomaly points
        /// </summary>
        /// <param name="apiAddress">Address of the API.</param>
        /// <param name="endpoint">The endpoint of the API</param>
        /// <param name="subscriptionKey">The subscription key applied  </param>
        /// <param name="requestData">The JSON string for requet data points</param>
        /// <returns>The JSON string for anomaly points and expected values.</returns>
        public async Task<string> Request(string apiAddress, string endpoint, string subscriptionKey, string requestData)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(apiAddress) })
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                var content = new StringContent(requestData, Encoding.UTF8, "application/json");
                var res = await client.PostAsync(endpoint, content);
                return await res.Content.ReadAsStringAsync();
            }
        }
    }
}
