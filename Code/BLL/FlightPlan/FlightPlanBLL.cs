using DAL.FlightPlan;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;

namespace BLL.FlightPlan
{
   public class FlightPlanBLL
    {
        public static bool Delete(string ids)
        {
            return FlightPlanDAL.Delete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(vFlightPlan model)
        {
            return FlightPlanDAL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(vFlightPlan model)
        {
            return FlightPlanDAL.Update(model);
        }


        public static PagedList<vFlightPlan> GetMyFlightPlanList(int pageSize, int pageIndex, string strWhere)
        {
            return FlightPlanDAL.GetMyFlightPlanList(pageSize, pageIndex, strWhere);
        }

        public static vFlightPlan Get(int id)
        {
            return FlightPlanDAL.Get(id);
        }
    }
}
