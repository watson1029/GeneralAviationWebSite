using System;

namespace Model.BasicData
{
    public class Aircraft
    {
        public int ID { get; set; }
        public string AircraftID { get; set; } 
        public int FuelCapacity { get; set; }
        public string AcfType { get; set; }
        public int Range { get; set; }
        public string AcfNo { get; set; }
        public int ASdate { get; set; }
        public string AcfClass { get; set; }
        public  int CruiseAltd { get; set; }
        public string Manufacturer { get; set; }
        public int CruiseSpeed { get; set; }
        public string WakeTurbulance { get; set; }
        public int MaxSpeed { get; set; }
        public int FueledWeight { get; set; }
        public int MinSpeed { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
