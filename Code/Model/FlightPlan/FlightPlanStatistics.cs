using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
    public class FlightPlanStatistics
    {
        public string Creator { get; set; }
        public string CompanyName { get; set; }
        public int AircraftNum { get; set; }
        public int SecondDiff { get; set;}
    }
    public class FlightPlanStatisticsAll: FlightPlanStatistics
    {
        public string TimeDiff { get; set; }
    }
}
