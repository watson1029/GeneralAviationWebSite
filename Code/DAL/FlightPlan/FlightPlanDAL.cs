using Model.EF;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ViewModel.FlightPlan;

namespace DAL.FlightPlan
{
    public class FlightPlanDAL : DBHelper<Model.EF.FlightPlan>
    {
        public override int BatchDelete(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;

            var temp = context.Set<Model.EF.FlightPlan>().Find(Guid.Parse(id));
            if (temp != null) context.Entry(temp).State = EntityState.Deleted;
            return context.SaveChanges();
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
                string sql = string.Format(@"select d.Creator,p.CompanyName,Sum(isnull(convert(int,AircraftNum),0))AircraftNum,sum(isnull(datediff(ss,ActualStartTime,ActualEndTime),0))SecondDiff
 from Company p 
left join vGetCurrentPlanNodeInstance
d  on p.CompanyID=d.creator and  d.ActorID != d.Creator and (d.State = 2 or d.State = 3) and d.ActualStartTime >= '{0}' 
and d.ActualEndTime <= '{1}'
group by p.CompanyName,d.Creator", started, ended);
                sql += string.Format(@" union select
d.Creator,p.CompanyName,Sum(isnull(convert(int,AircraftNum),0))AircraftNum,sum(isnull(datediff(ss,ActualStartTime,ActualEndTime),0))SecondDiff
 from Company p 
left join vGetCurrentPlanNodeInstance
d  on p.CompanyID=d.creator and  d.ActorID != d.Creator and (d.State = 2 or d.State = 3) and d.ActualStartTime <'{0}'  
and d.ActualEndTime <='{1}' and d.ActualEndTime >'{2}' 
group by p.CompanyName,d.Creator", started, ended, started);
                sql += string.Format(@" union select
d.Creator,p.CompanyName,Sum(isnull(convert(int,AircraftNum),0))AircraftNum,sum(isnull(datediff(ss,ActualStartTime,ActualEndTime),0))SecondDiff
 from Company p 
left join vGetCurrentPlanNodeInstance
d  on p.CompanyID=d.creator and  d.ActorID != d.Creator and (d.State = 2 or d.State = 3) and d.ActualStartTime>='{0}'  
and d.ActualEndTime>'{1}' and d.ActualStartTime<'{2}' 
group by p.CompanyName,d.Creator", started, ended, ended);
                var fps = context.Database.SqlQuery<FlightPlanStatistics>(sql);
                //int? SecondDiff = 0;
                //var fps = (from s in context.Company
                //           join p in context.vGetCurrentPlanNodeInstance on s.CompanyName equals p.CreatorName
                //            into temp
                //            from tt in temp.DefaultIfEmpty()
                //           where tt.ActorID != tt.Creator && (tt.State == 2 || tt.State == 3) && tt.ActualStartTime >= started && tt.ActualEndTime <= ended
                //           group tt by new
                //           {
                //               s.CompanyName,
                //               tt.Creator,
                //               tt.AircraftNum
                //               //p.CreatorName
                //           } into g
                //           select new
                //           {
                //               Creator = g.Key.Creator,
                //               CompanyName = g.Key.CompanyName,
                //               AircraftNum = g.Key.AircraftNum,
                //               //SecondDiff=g.Sum(EntityFunctions.DiffSeconds(p=>p.SOBT,p=>p.SIBT))
                //               SecondDiff = g.Sum(p => DbFunctions.DiffSeconds(p.ActualStartTime, p.ActualEndTime))
                //           }
                //           ).Union(
                //            from s in context.Company
                //            join p in context.vGetCurrentPlanNodeInstance on s.CompanyName equals p.CreatorName
                //              into temp
                //            from tt in temp.DefaultIfEmpty()
                //            where tt.ActorID != tt.Creator && (tt.State == 2 || tt.State == 3) && tt.ActualStartTime < started && tt.ActualEndTime <= ended && tt.ActualEndTime > started
                //            group tt by new
                //            {
                //                s.CompanyName,
                //                tt.Creator,
                //                tt.AircraftNum
                //            } into g
                //            select new
                //            {
                //                Creator = g.Key.Creator,
                //                CompanyName = g.Key.CompanyName,
                //                AircraftNum = g.Key.AircraftNum,
                //                SecondDiff = g.Sum(p => DbFunctions.DiffSeconds(started, p.ActualEndTime))
                //            }                           
                //    ).Union(
                //           from s in context.Company
                //           join p in context.vGetCurrentPlanNodeInstance on s.CompanyName equals p.CreatorName
                //             into temp
                //           from tt in temp.DefaultIfEmpty()
                //           where tt.ActorID != tt.Creator && (tt.State == 2 || tt.State == 3) && tt.ActualStartTime >= started && tt.ActualEndTime > ended && tt.ActualStartTime < ended
                //           group tt by new
                //           {
                //               s.CompanyName,
                //               tt.Creator,
                //               tt.AircraftNum
                //           } into g
                //           select new
                //           {
                //               Creator = g.Key.Creator,
                //               CompanyName = g.Key.CompanyName,
                //               AircraftNum = g.Key.AircraftNum,
                //               SecondDiff = g.Sum(p => DbFunctions.DiffSeconds(p.ActualStartTime, ended))
                //           }                           
                //    );
                //var fpsl = fps.ToList().Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                //rowCount = fps.ToList().Count;
                return fps.ToList();
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }
        /// <summary>
        /// 统计详细页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="rowCount"></param>
        /// <param name="Creator"></param>
        /// <param name="started"></param>
        /// <param name="ended"></param>
        /// <returns></returns>
        public List<vGetCurrentPlanNodeInstance> GetCurrentPlanNodeInstanceList(int page, int size, out int rowCount, int Creator, DateTime started, DateTime ended)
        {
            var cpInstance = from s in context.vGetCurrentPlanNodeInstance
                             where s.ActorID != s.Creator && (s.State == 2 || s.State == 3)
                             && s.Creator == Creator && s.ActualStartTime >= started && s.ActualEndTime <= ended
                             select s;
            rowCount = cpInstance.ToList().Count;
            List<vGetCurrentPlanNodeInstance> cplist = cpInstance.ToList().Skip((page - 1) * size).Take(size).ToList();
            return cplist;
        }

        public List<Model.EF.FlightPlan> GetList(List<string> planlist)
        {
            var linq = from t in context.FlightPlan
                       where planlist.Contains(t.RepetPlanID)
                       select t;
            return linq.ToList();
        }
    }
}
