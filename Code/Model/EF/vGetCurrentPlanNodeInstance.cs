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
    
    public partial class vGetCurrentPlanNodeInstance
    {
        public System.Guid ID { get; set; }
        public System.Guid PlanID { get; set; }
        public int StepID { get; set; }
        public int TWFID { get; set; }
        public Nullable<System.Guid> PrevID { get; set; }
        public Nullable<System.Guid> NextID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public byte State { get; set; }
        public Nullable<int> ActorID { get; set; }
        public Nullable<System.DateTime> ApplyTime { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> ActorTime { get; set; }
        public string ActorName { get; set; }
        public string RepetPlanID { get; set; }
        public string Code { get; set; }
        public int Creator { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> ActualStartTime { get; set; }
        public Nullable<System.DateTime> ActualEndTime { get; set; }
        public string PlanState { get; set; }
        public bool DeletedFlag { get; set; }
        public System.DateTime Expr2 { get; set; }
    }
}
