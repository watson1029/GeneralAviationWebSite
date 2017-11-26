using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SystemManagement
{
    public class Menu
    {
        public int ID { get; set; }
        public string MenuName { get; set; }
        public int? ParentMenuID { get; set; }
        public string LinkUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string ImageUrl { get; set; }
        public byte OrderSort { get; set; }
        public byte MenuLevel { get; set; }
        public string MenuCode { get; set; }


    }
}
