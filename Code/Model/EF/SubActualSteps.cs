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
    
    public partial class SubActualSteps
    {
        public System.Guid ID { get; set; }
        public string ParentStepID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public byte State { get; set; }
        public Nullable<int> ActorID { get; set; }
        public Nullable<System.DateTime> ApplyTime { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> ActorTime { get; set; }
        public string ActorName { get; set; }
        public string RoleName { get; set; }
    }
}
