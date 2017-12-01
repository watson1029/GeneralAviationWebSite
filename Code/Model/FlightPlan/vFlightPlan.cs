using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
    public class vFlightPlan
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
        public string PlanState { get; set; }
        public int? ActorID { get; set; }
        public int Creator { get; set; }
        public string CreatorName { get; set; }
        public string ADES { get; set; }
        public string ADEP { get; set; }
        public string WeekSchedule { get; set; }
        public DateTime SIBT { get; set; }
        public DateTime SOBT { get; set; }
        [Description("空勤组人数")]
        public int AircrewGroupNum { get; set; }
        [Description("飞行气候条件")]
        public string WeatherCondition { get; set; }
        [Description("二次雷达应答机代码")]
        public string RadarCode { get; set; }
        [Description("飞行员姓名")]
        public string Pilot { get; set; }
        [Description("航空器架数")]
        public int AircraftNum { get; set; }
        [Description("通信联络方法")]
        public string ContactWay { get; set; }
        [Description("代号呼号")]
        public string CallSign { get; set; }
        [Description("实际飞行开始时间")]
        public DateTime? ActualStartTime { get; set; }
        [Description("实际飞行结束时间")]
        public DateTime? ActualEndTime { get; set; }
    }
}
