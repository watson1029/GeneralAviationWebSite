using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
    public class RepetitivePlan
    {
        public int RepetPlanID { get; set; }
        public string PlanCode { get; set; }
        public string FlightType { get; set; }
        public string AircraftType { get; set; }
        public string FlightDirHeight { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string CompanyCode3 { get; set; }
        public string AttchFile { get; set; }
        public string Remark { get; set; }
        public byte PlanState { get; set; }
        public int? ActorID { get; set; }
        public int Creator { get; set; }
        public string ADES { get; set; }
        public string ADEP { get; set; }
        public string WeekSchedule { get; set; }
        public DateTime SIBT { get; set; }
        public DateTime SOBT { get; set; }
    }
}
