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
    
    public partial class BusinessIntroduction
    {
        public int ID { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Introduction { get; set; }
        public Nullable<byte> Status { get; set; }
        public Nullable<byte> Catalog { get; set; }
        public string BIContent { get; set; }
    }
}
