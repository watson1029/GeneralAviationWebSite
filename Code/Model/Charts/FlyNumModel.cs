using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Charts
{
    public class FlyNumModel
    {
        public List<string> NameItem { get; set; }
        public List<FlyNumData> FlyNumData { get; set; }
        public FlyNumModel()
        {
            this.NameItem = new List<string>();
            this.FlyNumData = new List<FlyNumData>();
        }
    }

    public class FlyNumData
    {
        public int value { get; set; }
        public string name { get; set; }
    }
}
