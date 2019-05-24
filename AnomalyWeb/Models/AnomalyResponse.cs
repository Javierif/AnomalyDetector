using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnomalyWeb.Models
{
    public class AnomalyResponse
    {
        public List<string> expectedValues;
        public List<bool> isAnomaly;
        public List<string> lowerMargins;
        public List<string> upperMargins;

    }
}
