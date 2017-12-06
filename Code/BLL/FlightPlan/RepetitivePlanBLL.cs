using DAL.FlightPlan;
using Model.EF;
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
         RepetitivePlanDAL dal = new RepetitivePlanDAL(); 
        public  bool Delete(string ids)
        {
            return dal.BatchDelete(ids)>0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  bool Add(RepetitivePlan model)
        {
            return dal.Add(model)>0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public  bool Update(RepetitivePlan model)
        {
            return dal.Update(model)>0;
        }


        //public  PagedList<RepetitivePlan> GetMyRepetitivePlanList(int pageSize, int pageIndex, out  strWhere)
        //{
        //    return dal.FindPagedList(pageIndex,pageSize, strWhere);
        //}

        public  RepetitivePlan Get(int id)
        {
            return dal.Find(u=>u.RepetPlanID==id);
        }
    }
}
