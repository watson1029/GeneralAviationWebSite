using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlightPlan
{
   public class AirportFillTotal
    {
        public List<AirportFillText> airportArray { get; set; }
    }
    public class AirportFillText
    {
        public string AirportName { get; set; }
        public string LatLong { get; set; }
    }
    public class AirlineFillTotal
    {
        public List<AirlineFillText> airlineArray { get; set; }
    }
    public class AirlineFillText
    {
        public string FlyHeight { get; set; }
        public string Radius { get; set; }
        public List<AirlinePointFillText> airlinePointList { get; set; }
    }
    public class AirlinePointFillText
    {
        public string PointName { get; set; }
        public string LatLong { get; set; }
    }
}
