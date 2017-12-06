using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BasicData
{
     public class CLicence
    {
        public int ID { get; set; }
        public int CompanyCode3 { get; set; }
        public int LegalPerson { get; set; }
        public int Licence { get; set; }
        public string Project { get; set; }
        public string BaseAirport { get; set; }
        public DateTime EffectiveData { get; set; }
        public  string CompanyType { get; set; }
        public DateTime LssueData { get; set;  }
        public  int Capital { get; set; }
        public int Quota { get; set; }
        public  DateTime CreateTime { get; set; }

    }
}
