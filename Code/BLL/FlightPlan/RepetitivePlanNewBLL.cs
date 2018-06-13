using DAL.FlightPlan;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FlightPlan
{
    public class RepetitivePlanNewBLL
    {
        RepetitivePlanNewDAL dal = new RepetitivePlanNewDAL();
        public bool Delete(string ids)
        {
            return dal.BatchDelete(ids) > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(RepetPlanNew model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(RepetPlanNew model)
        {
            return dal.Update(model) > 0;
        }


        public List<RepetPlanNew> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<RepetPlanNew, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.RepetPlanID, true);
        }
        public RepetPlanNew Get(int id)
        {
            return dal.Find(u => u.RepetPlanID == id);
        }
    }
}
