using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Charts
{
    public class FlyTimeModel
    {
        public List<string> NameItem { get; set; }
        public List<FlyTimeData> FlyTimeData { get; set; }
        public FlyTimeModel()
        {
            this.NameItem = new List<string>();
            this.FlyTimeData = new List<FlyTimeData>();
        }
    }

    public class FlyTimeData
    {
        public List<int> data { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public FlyTimeData()
        {
            this.data = new List<int>();
            this.type = "bar";
        }
    }
}
