using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
    /// <summary>
    /// 流程模版
    /// </summary>
    public class WorkflowTemplate
    {
        public enum TwfType
        {
            Other, RepetPlan
        } // 其他,长期计划
        public int TWFID { get; set; }
        public string TWFName { get; set; }
        public byte TWFType { get; set; }
        public string TWFDescription { get; set; }
        public int NextTWFID { get; set; }


    }
}
