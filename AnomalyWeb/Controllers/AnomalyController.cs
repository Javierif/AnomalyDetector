using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnomalyWeb.Models;
using AnomalyWeb.Service;
using AnomalyWeb.Models.DB;
using Newtonsoft.Json;

namespace AnomalyWeb.Controllers
{
    [Route("api/[controller]")]
    public class AnomalyController : Controller
    {
        private AnomalyRepository api;
        private TableRepository table;
        public AnomalyController()
        {
            api = new AnomalyRepository();
            table = new TableRepository();
        }


        [HttpGet("detectAnomalies")]
        public async Task<List<Response>> DetectAnomalies()
        {
            List<TransferenciaEntity> transferencias = await table.RetrieveAllTransferenciasAsync();

            AnomalyRequest request = new AnomalyRequest("daily", transferencias);

            var json = JsonConvert.SerializeObject(request);

            AnomalyResponse anomalies = api.DetectAnomaliesBatch(json);
            List<Response> responses = new List<Response>();

            for(int i = 0; i< request.series.Count;++i)
            {
                var serie = request.series[i];
                responses.Add(new Response(serie.timestamp, serie.value, anomalies, i));
            }

            return responses;
        }
    }
}
