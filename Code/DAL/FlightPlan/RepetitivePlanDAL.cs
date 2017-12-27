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
    }
}
