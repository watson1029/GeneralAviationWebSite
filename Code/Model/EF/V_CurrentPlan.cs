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
    
    public partial class V_CurrentPlan
    {
        public Nullable<int> CurrentFlightPlanID { get; set; }
        public int FlightPlanID { get; set; }
        public string PlanCode { get; set; }
        public string FlightType { get; set; }
        public string AircraftType { get; set; }
        public string FlightDirHeight { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public string CompanyCode3 { get; set; }
        public string PlanState { get; set; }
        public Nullable<int> ActorID { get; set; }
        public int Creator { get; set; }
        public string Remark { get; set; }
        public System.DateTime SOBT { get; set; }
        public System.DateTime SIBT { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public string RadarCode { get; set; }
        public Nullable<int> AircraftNum { get; set; }
        public string Pilot { get; set; }
        public string ContactWay { get; set; }
        public string WeatherCondition { get; set; }
        public Nullable<int> AircrewGroupNum { get; set; }
        public string CreatorName { get; set; }
        public string CallSign { get; set; }
        public Nullable<System.DateTime> ActualStartTime { get; set; }
        public Nullable<System.DateTime> ActualEndTime { get; set; }
        public string CompanyName { get; set; }
        public int RepetPlanID { get; set; }
        public byte CreateSource { get; set; }
        public string AttachFIle { get; set; }
    }
}
