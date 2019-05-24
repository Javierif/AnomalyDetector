using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AnomalyWeb.Models
{
    public class SerieModel
    {
        public DateTime timestamp { get; set; }
        public int value { get; set; }

        public SerieModel(string timestamp, int value)
        {
            var local = timestamp.Substring(0, 10);
            IFormatProvider culture = new CultureInfo("en-US", true);
            this.timestamp = DateTime.ParseExact(local, "dd/MM/yyyy", culture); ;
            this.value = value;
        }
    }
}
