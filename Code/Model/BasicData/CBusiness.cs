using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BasicData
{
    public class CBusiness
    {
        public int ID { get; set; }
        public int CompanyCode3 { get; set; }
        public string LegalName { get; set; }
        public DateTime JoinData { get; set; }
        public int LegalCard { get; set; }
        public string JoinAddress { get; set; }
        public string LegalAddress { get; set; }
        public int Capital { get; set; }
        public int LegalPhone { get; set; }
        public DateTime EffectiveData { get; set; }
        public string LegalConsignor { get; set; }
        public string Contacts { get; set; }
        public string ConsignorAddress { get; set; }
        public string ConsignorName { get; set; }

       //法人身份证复印件，法人委托书原件，委托人身份证原件

        public int ConsignorCard { get; set; }
        public int ConsignorPhone { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
