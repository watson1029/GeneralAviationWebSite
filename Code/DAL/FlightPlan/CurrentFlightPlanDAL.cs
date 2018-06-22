using Model.EF;
using System;
using System.Data.Entity;
using System.Linq;
namespace DAL.FlightPlan
{
    public class CurrentFlightPlanDAL : DBHelper<CurrentFlightPlan>
    {
        public override int BatchDelete(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;

            var temp = context.Set<CurrentFlightPlan>().Find(Guid.Parse(id));
            if (temp != null) context.Entry(temp).State = EntityState.Deleted;
            return context.SaveChanges();
        }

        /// <summary>
        /// 获取当日动态未提交数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCurrentUnSubmitNum(int userId)
        {
            var linq = from t in context.CurrentFlightPlan
                       where t.Creator == userId
                       where t.PlanState == "0"
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取当日动态提交数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCurrentSubmitNum(int userId)
        {
            var linq = from t in context.CurrentFlightPlan
                       where t.Creator == userId
                       where t.PlanState != "0"
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取当日动态提交数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCurrentSubmitNum(int userId, DateTime beginTime, DateTime endTime)
        {
            var linq = from t in context.CurrentFlightPlan
                       where t.Creator == userId
                       where t.PlanState != "0"
                       where t.CreateTime > beginTime
                       where t.CreateTime < endTime
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取当日动态待审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCurrentUnAuditNum(int userId)
        {
            var linq = from t in context.vGetCurrentPlanNodeInstance
                       where t.ActorID == userId
                       where t.State == 1
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取当日动态审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCurrentAuditNum(int userId)
        {
            var linq = from t in context.vGetCurrentPlanNodeInstance
                       where t.ActorID == userId
                       where t.State == 2 || t.State == 3
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取当日动态审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCurrentAuditNum(int userId, DateTime beginTime, DateTime endTime)
        {
            var linq = from t in context.vGetCurrentPlanNodeInstance
                       where t.ActorID == userId
                       where t.State == 2 || t.State == 3
                       where t.ActorTime > beginTime
                       where t.ActorTime < endTime
                       select t;
            return linq.Count();
        }
    }
}
