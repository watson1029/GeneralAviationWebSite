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
    
    public partial class FlyPlanDemo
    {
        public string FlyPlanID { get; set; }
        public string RepetPlanID { get; set; }
        public Nullable<System.DateTime> PlanDate { get; set; }
        public Nullable<System.DateTime> PlanBeginTime { get; set; }
        public Nullable<System.DateTime> PlanEndTime { get; set; }
        public string AircraftModel { get; set; }
        public Nullable<System.DateTime> TakeOffTime { get; set; }
        public Nullable<System.DateTime> LandTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CompanyName { get; set; }
        public string Pilot { get; set; }
    }
}
