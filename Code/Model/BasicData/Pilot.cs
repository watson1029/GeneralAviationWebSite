using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BasicData
{
    public class Pilot
    {
        public int ID { get; set; }
        public string Pilots { get; set; }
        public DateTime DataTime { get; set; }
        public string Password { get; set; }
        public byte Company { get; set; }
        public byte Sex { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
