using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlightPlan
{
    public class RepetitivePlanVM
    {
        public RepetitivePlanVM()
        {
            airportList = new List<AirportVM>();
            airlineList = new List<AirlineVM>();
            cworkList = new List<WorkVM>();
            pworkList = new List<WorkVM>();
            hworkList = new List<WorkVM>();
        }
        public System.Guid RepetPlanID { get; set; }
        public string FlightType { get; set; }
        public string Code { get; set; }
        public string AircraftType { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public string CompanyCode3 { get; set; }
        public string AttachFile { get; set; }
        public string PlanState { get; set; }
        public Nullable<int> ActorID { get; set; }
        public Nullable<int> Creator { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public string CompanyName { get; set; }
        public string DocuText { get; set; }
        public string AircraftNum { get; set; }
        public string CallSign { get; set; }
        public string WeekSchedule { get; set; }
        public string AirportText { get; set; }
        public string AirlineText { get; set; }
        public string WorkText { get; set; }
        public string CWorkText { get; set; }
        public string PWorkText { get; set; }
        public string HWorkText { get; set; }
        public string AirlineWorkText { get; set; }
        public string CreateSource { get; set; }

        public List<AirportVM> airportList { get; set; }
        public List<AirlineVM> airlineList { get; set; }
        public List<WorkVM> cworkList { get; set; }
        public List<WorkVM> pworkList { get; set; }
        public List<WorkVM> hworkList { get; set; }
        public int airLineMaxCol { get; set; }
        public int pworkMaxCol { get; set; }
        public int hworkMaxCol { get; set; }
    }
}
