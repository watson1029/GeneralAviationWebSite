using Model.EF;
using System.Collections.Generic;
using System.Linq;
namespace DAL.FlightPlan
{
    public class RepetitivePlanDAL:DBHelper<RepetitivePlan>
    {
        public Dictionary<string, int> GetGroupCount()
        {
            var result = (from a in context.RepetitivePlan
                          group a by a.CompanyCode3 into g
                          select new { name = g.Key, count = g.Count() })
                         .ToDictionary(a => a.name, a => a.count);
            return result;
        }
        
    }
}
