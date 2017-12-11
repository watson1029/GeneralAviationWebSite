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
    
    public partial class Aircraft
    {
        public int AircraftID { get; set; }
        public string AircraftSign { get; set; }
        public string AcfType { get; set; }
        public string AcfNo { get; set; }
        public string AcfClass { get; set; }
        public string Manufacture { get; set; }
        public string WakeTurbulance { get; set; }
        public Nullable<int> FueledWeight { get; set; }
        public Nullable<int> FuelCapacity { get; set; }
        public Nullable<int> Range { get; set; }
        public System.DateTime ASdate { get; set; }
        public Nullable<double> CruiseAltd { get; set; }
        public Nullable<double> CruiseSpeed { get; set; }
        public Nullable<double> MinSpeed { get; set; }
        public Nullable<double> MaxSpeed { get; set; }
        public string CompanyCode3 { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
