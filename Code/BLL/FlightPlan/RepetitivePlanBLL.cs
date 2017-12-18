using DAL.FlightPlan;
using Model.EF;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


        public List<RepetitivePlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<RepetitivePlan, bool>> where)
         {
             return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.RepetPlanID, true);
         }
        public List<RepetitivePlan> GetList(Expression<Func<RepetitivePlan, bool>> where)
    {
        return dal.FindList(where, m => m.RepetPlanID, true);
    }
        public  RepetitivePlan Get(int id)
        {
            return dal.Find(u=>u.RepetPlanID==id);
        }
    }
}
