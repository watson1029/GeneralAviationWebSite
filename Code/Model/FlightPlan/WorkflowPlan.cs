using System;
using System.Data.Common;

namespace Model.FlightPlan
{
    public class WorkflowPlan
    {
        public int? Actor { get; set; }
        public string ActorName { get; set; }
        public string PlanState { get; set; }
        public Guid PlanID { get; set; }

    }
}
