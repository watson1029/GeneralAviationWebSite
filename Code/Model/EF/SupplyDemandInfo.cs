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
    
    public partial class SupplyDemandInfo
    {
        public int ID { get; set; }
        public Nullable<int> Creator { get; set; }
        public string CreateName { get; set; }
        public string CompanyCode3 { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Summary { get; set; }
        public string Catalog { get; set; }
        public Nullable<int> ActorID { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string SummaryCode { get; set; }
    }
}
