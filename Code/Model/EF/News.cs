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
    
    public partial class News
    {
        public int NewID { get; set; }
        public string NewTitle { get; set; }
        public string NewContent { get; set; }
        public byte IsTop { get; set; }
        public int Sort { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Author { get; set; }
        public bool IsDelete { get; set; }
    }
}
