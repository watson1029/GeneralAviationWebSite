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
    public class RepetitivePlanBLL
    {


        public static bool Delete(string ids)
        {
            return RepetitivePlanDAL.Delete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(RepetitivePlan model)
        {
            return RepetitivePlanDAL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(RepetitivePlan model)
        {
            return RepetitivePlanDAL.Update(model);
        }


        public static PagedList<RepetitivePlan> GetMyRepetitivePlanList(int pageSize, int pageIndex, string strWhere)
        {
            return RepetitivePlanDAL.GetMyRepetitivePlanList(pageSize, pageIndex, strWhere);
        }

        public static RepetitivePlan Get(int id)
        {
            return RepetitivePlanDAL.Get(id);
        }
    }
}
