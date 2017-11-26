using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SystemManagement
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
