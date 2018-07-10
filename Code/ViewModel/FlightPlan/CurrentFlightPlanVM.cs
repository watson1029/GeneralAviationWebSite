using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlightPlan
{
    public class CurrentFlightPlanVM
    {

        public CurrentFlightPlanVM()
        {
            airlineList = new List<AirlineVM>();
            cworkList = new List<WorkVM>();
            pworkList = new List<WorkVM>();
            hworkList = new List<WorkVM>();
            airlineworkList = new List<string>();
        }
        public System.Guid FlightPlanID { get; set; }
        public string Code { get; set; }
        public string FlightType { get; set; }
        public string AircraftType { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public string PlanState { get; set; }
        public Nullable<int> ActorID { get; set; }
        public Nullable<int> Creator { get; set; }
        public string Remark { get; set; }
        public System.DateTime ActualStartTime { get; set; }
        public System.DateTime ActualEndTime { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public string SsrCode { get; set; }
        public int AircraftNum { get; set; }
        public string CreatorName { get; set; }
        public string CallSign { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode3 { get; set; }
        public string AirportText { get; set; }
        public string AirlineWorkText { get; set; }
        public string MasterIDs { get; set; }
        public List<AirlineVM> airlineList { get; set; }
        public List<WorkVM> cworkList { get; set; }
        public List<WorkVM> pworkList { get; set; }
        public List<WorkVM> hworkList { get; set; }
        public List<string> airlineworkList { get; set; }
        public int airLineMaxCol { get; set; }
        public int pworkMaxCol { get; set; }
        public int hworkMaxCol { get; set; }
    }
}
