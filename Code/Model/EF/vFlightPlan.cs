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
    
    public partial class vFlightPlan
    {
        public System.Guid FlightPlanID { get; set; }
        public string FlightType { get; set; }
        public string AircraftType { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string CompanyCode3 { get; set; }
        public string PlanState { get; set; }
        public Nullable<int> ActorID { get; set; }
        public Nullable<int> Creator { get; set; }
        public string Remark { get; set; }
        public System.DateTime SOBT { get; set; }
        public System.DateTime SIBT { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public string CreatorName { get; set; }
        public string CallSign { get; set; }
        public string CompanyName { get; set; }
        public string RepetPlanID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Code { get; set; }
        public string SsrCode { get; set; }
        public Nullable<System.DateTime> ALDT { get; set; }
        public Nullable<System.DateTime> ATOT { get; set; }
        public Nullable<System.DateTime> AOBT { get; set; }
        public string ALTN1 { get; set; }
        public string ALTN2 { get; set; }
        public string AirportText { get; set; }
        public string AirlineWorkText { get; set; }
    }
}
