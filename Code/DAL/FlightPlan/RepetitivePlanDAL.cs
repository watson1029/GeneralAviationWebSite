using Model.EF;
using System.Collections.Generic;
using Untity;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System;

namespace DAL.FlightPlan
{
    public class RepetitivePlanDAL : DBHelper<RepetitivePlan>
    {
        public List<TemplateClass4StatisticResult> getCollect(DateTime startTime, DateTime endTime, int type, string companyCode3 = "")
        {
            //TemplateClass4StatisticResult:CompanyCode CompanyName Count CreateTime  
            if (companyCode3 == "")
            {
                if (type == 1)
                {
                    var res = from a in context.RepetitivePlan
                              where a.CreateTime >= startTime
                              where a.CreateTime <= endTime

                              group a by new { a.CreateTime.Year, a.CompanyCode3 } into b

                              join d in context.Company on b.Key.CompanyCode3 equals d.CompanyCode3 into e
                              from f in e.DefaultIfEmpty()
                              orderby b.Key.Year
                              select new TemplateClass4StatisticResult
                              {
                                  _field1 = b.Key.Year.ToString(),
                                  _field2 = f.CompanyName,
                                  _field3 = b.Count(),
                                  _field4 = f.CreateTime.Value
                              };
                    return res.ToList();
                }
                else if (type == 2)
                {
                    var res = from a in context.RepetitivePlan
                              where a.CreateTime >= startTime
                              where a.CreateTime <= endTime
                              group a by new { a.CreateTime.Year, a.CreateTime.Month, a.CompanyCode3 } into b

                              join d in context.Company on b.Key.CompanyCode3 equals d.CompanyCode3 into e
                              from f in e.DefaultIfEmpty()
                              orderby b.Key.Year, b.Key.Month
                              select new TemplateClass4StatisticResult
                              {
                                  _field1 = b.Key.Year.ToString() + "-" + b.Key.Month.ToString(),
                                  _field2 = f.CompanyName,
                                  _field3 = b.Count(),
                                  _field4 = f.CreateTime.Value
                              };
                    return res.ToList();
                }
                else if (type == 3)
                {
                    var res = from a in context.RepetitivePlan
                              where a.CreateTime >= startTime
                              where a.CreateTime <= endTime

                              group a by new { a.CreateTime.Year, a.CreateTime.Month, a.CreateTime.Day, a.CompanyCode3 } into b
                              join d in context.Company on b.Key.CompanyCode3 equals d.CompanyCode3 into e
                              from f in e.DefaultIfEmpty()
                              orderby b.Key.Day
                              select new TemplateClass4StatisticResult
                              {
                                  _field1 = b.Key.Year.ToString() + "-" + b.Key.Month.ToString() + "-" + b.Key.Day.ToString(),
                                  _field2 = f.CompanyName,
                                  _field3 = b.Count(),
                                  _field4 = f.CreateTime.Value
                              };
                    return res.ToList();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (type == 1)
                {
                    var res = from a in context.RepetitivePlan
                              where a.CreateTime >= startTime
                              where a.CreateTime <= endTime
                              where a.CompanyCode3 == companyCode3
                              group a by new { a.CreateTime.Year, a.CompanyCode3 } into b

                              join d in context.Company on b.Key.CompanyCode3 equals d.CompanyCode3 into e
                              from f in e.DefaultIfEmpty()
                              orderby b.Key.Year
                              select new TemplateClass4StatisticResult
                              {
                                  _field1 = b.Key.Year.ToString(),
                                  _field2 = f.CompanyName,
                                  _field3 = b.Count(),
                                  _field4 = f.CreateTime.Value
                              };
                    return res.ToList();
                }
                else if (type == 2)
                {
                    var res = from a in context.RepetitivePlan
                              where a.CreateTime >= startTime
                              where a.CreateTime <= endTime
                              where a.CompanyCode3 == companyCode3
                              group a by new { a.CreateTime.Year, a.CreateTime.Month, a.CompanyCode3 } into b

                              join d in context.Company on b.Key.CompanyCode3 equals d.CompanyCode3 into e
                              from f in e.DefaultIfEmpty()
                              orderby b.Key.Year, b.Key.Month
                              select new TemplateClass4StatisticResult
                              {
                                  _field1 = b.Key.Year.ToString() + "-" + b.Key.Month.ToString(),
                                  _field2 = f.CompanyName,
                                  _field3 = b.Count(),
                                  _field4 = f.CreateTime.Value
                              };
                    return res.ToList();
                }
                else if (type == 3)
                {
                    var res = from a in context.RepetitivePlan
                              where a.CreateTime >= startTime
                              where a.CreateTime <= endTime
                              where a.CompanyCode3 == companyCode3
                              group a by new { a.CreateTime.Year, a.CreateTime.Month, a.CreateTime.Day, a.CompanyCode3 } into b
                              join d in context.Company on b.Key.CompanyCode3 equals d.CompanyCode3 into e
                              from f in e.DefaultIfEmpty()
                              orderby b.Key.Day
                              select new TemplateClass4StatisticResult
                              {
                                  _field1 = b.Key.Year.ToString() + "-" + b.Key.Month.ToString() + "-" + b.Key.Day.ToString(),
                                  _field2 = f.CompanyName,
                                  _field3 = b.Count(),
                                  _field4 = f.CreateTime.Value
                              };
                    return res.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public List<TemplateClass4StatisticResult> getStatisticResult(DateTime startTime, DateTime endTime, string companyCode3 = "")
        {
            //TemplateClass4StatisticResult:CompanyCode CompanyName Count CreateTime            
            if (string.IsNullOrEmpty(companyCode3))
            {
                var res = from a in context.RepetitivePlan
                          where a.CreateTime >= startTime
                          where a.CreateTime <= endTime
                          group a by a.CompanyCode3 into b
                          join d in context.Company on b.Key equals d.CompanyCode3 into e
                          from f in e.DefaultIfEmpty()
                          select new TemplateClass4StatisticResult
                          {
                              _field1 = b.Key,
                              _field2 = f.CompanyName,
                              _field3 = b.Count(),
                              _field4 = f.CreateTime.Value
                          };
                return res.ToList();
            }
            else
            {
                var res = from a in context.RepetitivePlan
                          where a.CompanyCode3 == companyCode3
                          where a.CreateTime >= startTime
                          where a.CreateTime <= endTime
                          group a by a.CompanyCode3 into b
                          join d in context.Company on b.Key equals d.CompanyCode3 into e
                          from f in e.DefaultIfEmpty()
                          select new TemplateClass4StatisticResult
                          {
                              _field1 = b.Key,
                              _field2 = f.CompanyName,
                              _field3 = b.Count(),
                              _field4 = f.CreateTime.Value
                          };
                return res.ToList();
            }
        }
        public override int BatchDelete(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;

            var temp = context.Set<RepetitivePlan>().Find(Guid.Parse(id));
            if (temp != null) context.Entry(temp).State = EntityState.Deleted;
            return context.SaveChanges();
        }

        /// <summary>
        /// 获取长期计划未提交数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetRepetUnSubmitNum(int userId)
        {
            var linq = from t in context.RepetitivePlan
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
        public int GetRepetSubmitNum(int userId)
        {
            var linq = from t in context.RepetitivePlan
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
        public int GetRepetSubmitNum(int userId, DateTime beginTime, DateTime endTime)
        {
            var linq = from t in context.RepetitivePlan
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
        public int GetRepetUnAuditNum(int userId)
        {
            var linq = from t in context.vGetRepetitivePlanNodeInstance
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
        public int GetRepetAuditNum(int userId)
        {
            var linq = from t in context.vGetRepetitivePlanNodeInstance
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
        public int GetRepetAuditNum(int userId, DateTime beginTime, DateTime endTime)
        {
            var linq = from t in context.vGetRepetitivePlanNodeInstance
                       where t.ActorID == userId
                       where t.State == 2 || t.State == 3
                       where t.ActorTime > beginTime
                       where t.ActorTime < endTime
                       select t;
            return linq.Count();
        }

        public List<string> GetAirportName(string repetid)
        {
            var linq = from t in context.AirportInfo
                       join t1 in context.File_Airport on t.Id equals t1.AirportID
                       where t1.RepetPlanID == repetid
                       select t.Name;
            return linq.ToList();
        }


        public string GetFlightTaskName(string taskcode)
        {
            var linq = from t in context.FlightTask
                       where t.TaskCode == taskcode
                       select t.Description;
            return linq.ToString();
        }

        public List<string> GetFlyHigh(string repetID, string workTxt)
        {
            var linq = from t in context.File_Master
                       where t.RepetPlanID == repetID
                       select t;
            Dictionary<int, string> list = new Dictionary<int, string>();
            foreach (var l in linq.ToList())
            {
                list.Add(workTxt.IndexOf(l.LineDescript), l.FlyHeight);
            }
            var highList = new List<string>();
            foreach (var s in list.OrderBy(p => p.Key))
            {
                highList.Add(s.Value);
            }
            return highList;
        }
    }
}
