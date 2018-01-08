using DAL.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Model.EF;
namespace BLL.FlightPlan
{
    public class FlightPlanBLL
    {
        FlightPlanDAL dal = new FlightPlanDAL();
        public bool Delete(string ids)
        {
            return dal.BatchDelete(ids) > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.EF.FlightPlan model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.EF.FlightPlan model)
        {
            return dal.Update(model) > 0;
        }


        public List<Model.EF.FlightPlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Model.EF.FlightPlan, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.FlightPlanID, true);
        }
        public List<Model.EF.FlightPlan> GetList(Expression<Func<Model.EF.FlightPlan, bool>> where)
        {
            return dal.FindList(where, m => m.FlightPlanID, true);
        }
        public Model.EF.FlightPlan Get(int id)
        {
            return dal.Find(u => u.FlightPlanID == id);
        }
        public Model.EF.vFlightPlan GetvFlightPlan(int id)
        {
            var context = new ZHCC_GAPlanEntities();
            return context.Set<vFlightPlan>().Where(u => u.FlightPlanID == id).FirstOrDefault();
        }

        public List<vGetFlightPlanNodeInstance> GetNodeInstanceList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<vGetFlightPlanNodeInstance, bool>> where)
        {
            var insdal = new DBHelper<vGetFlightPlanNodeInstance>();
            return insdal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.PlanID, true);
        }
    }
}
