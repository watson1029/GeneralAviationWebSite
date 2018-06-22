using Model.EF;
using System;
using Model.FlightPlan;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.FlightPlan
{
    public class FlightPlanDAL : DBHelper<Model.EF.FlightPlan>
    {
        public List<Model.EF.FlightPlan> GetFlightPlan(int userID, string PlanState)
        {
            var fplist = from s in context.FlightPlan
                         where s.Creator == userID && s.PlanState == PlanState
                         select s;
            return fplist.ToList();
        }

        /// <summary>
        /// 飞行时间都在范围内
        /// </summary>
        /// <param name="started"></param>
        /// <param name="ended"></param>
        /// <returns></returns>
        public object GetFullTimeFlightStatistics(DateTime started, DateTime ended)
        {
            //List<FlightPlanStatistics> fpslist = new List<FlightPlanStatistics>();
            try
            {
                int? SecondDiff = 0;
                var fps = (from p in context.vGetCurrentPlanNodeInstance
                           where p.ActorID != p.Creator && (p.State == 2 || p.State == 3) && p.SOBT >= started && p.SIBT <= ended
                           group p by new
                           {
                               p.Creator,
                               p.CompanyName
                           } into g
                           select new
                           {
                               Creator = g.Key.Creator,
                               CompanyName = g.Key.CompanyName,
                               AircraftNum = g.Sum(p => p.AircraftNum),
                               //SecondDiff=g.Sum(EntityFunctions.DiffSeconds(p=>p.SOBT,p=>p.SIBT))
                               SecondDiff = g.Sum(p => DbFunctions.DiffSeconds(p.SOBT, p.SIBT))
                           }).Union(
                          from p in context.vGetCurrentPlanNodeInstance
                          where p.ActorID != p.Creator && (p.State == 2 || p.State == 3)
                          group p by new
                          {
                              p.Creator,
                              p.CompanyName
                          } into g
                          select new
                          {
                              Creator = g.Key.Creator,
                              CompanyName = g.Key.CompanyName,
                              AircraftNum = g.Sum(p => p.AircraftNum == 0 ? SecondDiff : SecondDiff),
                              SecondDiff = SecondDiff
                          }).Union(
                            from p in context.vGetCurrentPlanNodeInstance
                            where p.ActorID != p.Creator && (p.State == 2 || p.State == 3) && p.SOBT < started && p.SIBT <= ended && p.SIBT > started
                            group p by new
                            {
                                p.Creator,
                                p.CompanyName
                            } into g
                            select new
                            {
                                Creator = g.Key.Creator,
                                CompanyName = g.Key.CompanyName,
                                AircraftNum = g.Sum(p => p.AircraftNum),
                                SecondDiff = g.Sum(p => DbFunctions.DiffSeconds(started, p.SIBT))
                            }
                    ).Union(
                            from p in context.vGetCurrentPlanNodeInstance
                            where p.ActorID != p.Creator && (p.State == 2 || p.State == 3) && p.SOBT >= started && p.SIBT > ended && p.SOBT < ended
                            group p by new
                            {
                                p.Creator,
                                p.CompanyName
                            } into g
                            select new
                            {
                                Creator = g.Key.Creator,
                                CompanyName = g.Key.CompanyName,
                                AircraftNum = g.Sum(p => p.AircraftNum),
                                SecondDiff = g.Sum(p => DbFunctions.DiffSeconds(p.SOBT, ended))
                            }
                    );
                //var fpsl = fps.ToList().Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                //rowCount = fps.ToList().Count;
                return fps.ToList();
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        public List<vGetCurrentPlanNodeInstance> GetCurrentPlanNodeInstanceList(int page,int size, out int rowCount,int Creator,DateTime started,DateTime ended)
        {
            var cpInstance=from s in context.vGetCurrentPlanNodeInstance
                                                       where s.ActorID != s.Creator && (s.State == 2 || s.State == 3)
                                                       && s.Creator == Creator && s.SOBT >= started && s.SIBT <= ended
                                                       select s;
            rowCount = cpInstance.ToList().Count;
            List<vGetCurrentPlanNodeInstance> cplist = cpInstance.ToList().Skip((page-1)*size).Take(size).ToList();
            return cplist;
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
