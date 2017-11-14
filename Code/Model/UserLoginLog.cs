using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserLoginLog
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LoginOutTime { get; set; }
        public string IPAddress { get; set; }
    }
}
