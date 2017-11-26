using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SystemManagement
{
    public class TreeNode
    {
        public TreeNode() {
            @checked = false;
            state = "open";
        }
        public int id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public TreeNode[] children { get; set; }
       public bool @checked{ get; set; }
    }
}
