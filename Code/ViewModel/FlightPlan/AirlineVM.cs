using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlightPlan
{
    public class AirlineVM
    {
        public AirlineVM()
        {
            pointList = new List<PointVM>();
        }
        public string masterID { get; set; }
        public List<PointVM> pointList { get; set; }
        public string FlyHeight { get; set; }
    }
    public class PointVM
    {
        public string Name { get; set; }
        public string LatLong { get; set; }
    }
}
