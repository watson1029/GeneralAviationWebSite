using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FlightPlan
{
    public class WorkflowPlan
    {
        public int? Actor { get; set; }
        public string PlanState { get; set; }
        public int PlanID { get; set; }
    }
}
