//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class RepetitivePlan
    {
        public int RepetPlanID { get; set; }
        public string PlanCode { get; set; }
        public string FlightType { get; set; }
        public string AircraftType { get; set; }
        public string FlightDirHeight { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public string CompanyCode3 { get; set; }
        public string AttchFile { get; set; }
        public string PlanState { get; set; }
        public Nullable<int> ActorID { get; set; }
        public int Creator { get; set; }
        public string Remark { get; set; }
        public System.TimeSpan SOBT { get; set; }
        public System.TimeSpan SIBT { get; set; }
        public string WeekSchedule { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public string CreatorName { get; set; }
        public string CallSign { get; set; }
        public string CompanyName { get; set; }
        public Nullable<bool> IsGenFlightPlan { get; set; }
    }
}
