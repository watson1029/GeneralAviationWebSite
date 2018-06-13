using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FlightPlan
{
    public class RepetitivePlanNewDAL : DBHelper<RepetPlanNew> 
    {
        /// <summary>
        /// 获取有效的长期计划
        /// </summary>
        /// <returns></returns>
        public List<RepetPlanNew> GetAuditRepetPlan()
        {
            DateTime time = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);
            var linq = from t in context.RepetPlanNew
                       where t.Status == 3
                       where t.StartDate <= time && t.EndDate >= time
                       select t;
            return linq.ToList();
        }
    }
}
