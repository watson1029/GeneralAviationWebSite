using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlightPlan
{
    public class CurrentFlightPlanVM
    {
        public int   AircraftNum{get;set;}
        public string ContractWay { get; set; }
        public string Pilot { get; set; }
        public DateTime ActualStartTime { get; set; }
        public DateTime ActualEndTime { get; set; }
    }
}
