using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BasicData
{
    public class FlightTask
    {
        public string TaskCode { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public string FlightType { get; set; }
        public bool DefaultMapping { get; set; }
    }
}
