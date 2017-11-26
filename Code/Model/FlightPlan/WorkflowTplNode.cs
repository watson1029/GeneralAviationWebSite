using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
   public class WorkflowTplNode
    {
        public int StepId { get; set; }
        public int TWFID { get; set; }
        public string StepName { get; set; }
        public int PrevId { get; set; }
        public int NextId { get; set; }
        public string Hide { get; set; }
        public string AuthorType { get; set; }
    }
}
