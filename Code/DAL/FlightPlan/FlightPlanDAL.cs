using Model.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.FlightPlan
{
    public class FlightPlanDAL:DBHelper<Model.EF.FlightPlan>
    {
        public List<Model.EF.FlightPlan> GetFlightPlan(int userID,string PlanState)
        {
            var fplist = from s in context.FlightPlan
                                               where s.Creator == userID && s.PlanState==PlanState
                                               select s;
            return fplist.ToList();
        }
    }
}
