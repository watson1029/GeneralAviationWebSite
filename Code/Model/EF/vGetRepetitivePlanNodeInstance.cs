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
    
    public partial class vGetRepetitivePlanNodeInstance
    {
        public System.Guid ID { get; set; }
        public System.Guid PlanID { get; set; }
        public int StepID { get; set; }
        public int TWFID { get; set; }
        public Nullable<System.Guid> PrevID { get; set; }
        public Nullable<System.Guid> NextID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<byte> State { get; set; }
        public Nullable<int> ActorID { get; set; }
        public Nullable<System.DateTime> ApplyTime { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> ActorTime { get; set; }
        public string ActorName { get; set; }
        public string FlightType { get; set; }
        public string AircraftType { get; set; }
        public string Code { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string CompanyCode3 { get; set; }
        public string Remark { get; set; }
        public string WeekSchedule { get; set; }
        public string AirportText { get; set; }
        public string AirlineWorkText { get; set; }
        public string AttachFile { get; set; }
        public string CreatorName { get; set; }
        public Nullable<int> Creator { get; set; }
        public string CompanyName { get; set; }
        public string RoleName { get; set; }
    }
}
