using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.FlightPlan
{
    public class FlightPlanDAL:DBHelper<Model.EF.FlightPlan>
    {
        public List<Model.EF.FlightPlan> GetFlightPlan(int userID, string PlanState)
        {
            var fplist = from s in context.FlightPlan
                         where s.Creator == userID && s.PlanState == PlanState
                         select s;
            return fplist.ToList();
        }

        /// <summary>
        /// 获取长期计划待提交数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetFlyUnSubmitNum(int userId)
        {
            var linq = from t in context.FlightPlan
                       where t.Creator == userId
                       where t.PlanState == "0"
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取长期计划提交数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetFlySubmitNum(int userId)
        {
            var linq = from t in context.FlightPlan
                       where t.Creator == userId
                       where t.PlanState != "0"
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取长期计划提交数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetFlySubmitNum(int userId, DateTime beginTime, DateTime endTime)
        {
            var linq = from t in context.FlightPlan
                       where t.Creator == userId
                       where t.PlanState != "0"
                       where t.CreateTime > beginTime
                       where t.CreateTime < endTime
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取长期计划待审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetFlyUnAuditNum(int userId)
        {
            var linq = from t in context.vGetFlightPlanNodeInstance
                       where t.ActorID == userId
                       where t.State == 1
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取长期计划审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetFlyAuditNum(int userId)
        {
            var linq = from t in context.vGetFlightPlanNodeInstance
                       where t.ActorID == userId
                       where t.State == 2 || t.State == 3
                       select t;
            return linq.Count();
        }

        /// <summary>
        /// 获取长期计划审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetFlyAuditNum(int userId, DateTime beginTime, DateTime endTime)
        {
            var linq = from t in context.vGetFlightPlanNodeInstance
                       where t.ActorID == userId
                       where t.State == 2 || t.State == 3
                       where t.ActorTime > beginTime
                       where t.ActorTime < endTime
                       select t;
            return linq.Count();
        }
    }
}
