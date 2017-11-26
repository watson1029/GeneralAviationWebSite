using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SystemManagement
{
    public class MenuModel
    {
        public string menuid { get; set; }
        public string icon { get; set; }
        public string menuname { get; set; }
        public string url { get; set; }
        public MenuModel[] menus { get; set; }
    }
}
