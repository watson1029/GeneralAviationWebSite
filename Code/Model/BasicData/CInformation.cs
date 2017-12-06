using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BasicData
{
    public class CInformation
    {
        public int ID { get; set; }
        public int  CompanyCode3 { get; set; }
        public int CompanyCode2 { get; set; }
        public string CompanyName { get; set; }
        public string EnglishName { get; set; }  
        public DateTime CreateTime { get; set; }

    }
}
