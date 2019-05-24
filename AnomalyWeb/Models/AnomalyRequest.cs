using AnomalyWeb.Models.DB;
using System;
using System.Collections.Generic;

namespace AnomalyWeb.Models
{
    public class AnomalyRequest
    {
        public string granularity { get; set; }
        public List<SerieModel> series { get; set; }

        public AnomalyRequest(string granularity, List<TransferenciaEntity> data)
        {
            this.granularity = "daily";
            this.series = new List<SerieModel>();
            foreach(TransferenciaEntity serie in data)
            {
                var item = new SerieModel(serie.Time, serie.Money);
                this.series.Add(item);
            }
            series.Sort((x, y) => DateTime.Compare(x.timestamp, y.timestamp));
        }
    }
}