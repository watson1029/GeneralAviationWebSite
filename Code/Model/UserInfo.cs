using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public byte IsGeneralAviation { get; set; }
        public string CompanyCode3 { get; set; }
        public DateTime CreateTime { get; set; }
        public byte Status { get; set; }
    }
}
