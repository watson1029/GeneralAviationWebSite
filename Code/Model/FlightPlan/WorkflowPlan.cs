using System.Data.Common;

namespace Model.FlightPlan
{
    public class WorkflowPlan
    {
        public int? Actor { get; set; }
        public string PlanState { get; set; }
        public int PlanID { get; set; }

    }
}
