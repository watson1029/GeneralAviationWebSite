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
        public List<TemplateClass4StatisticResult> getCollect(DateTime startTime, DateTime endTime, int type)
        {
            //TemplateClass4StatisticResult:CompanyCode CompanyName Count CreateTime            
            if (type == 1)
            {
                var res = from a in context.RepetitivePlan
                          //where a.CreateTime >= startTime
                          //where a.CreateTime <= endTime
                          group a by new { a.CreateTime.Year, a.CompanyCode3 } into b

                          join d in context.Company on b.Key.CompanyCode3 equals d.CompanyCode3 into e
                          from f in e.DefaultIfEmpty()
                          select new TemplateClass4StatisticResult
                          {
                              _field1 = b.Key.CompanyCode3,
                              _field2 = f.CompanyName,
                              _field3 = b.Count(),
                              _field4 = b.Key.Year.ToString
                          };
                return res.ToList();
            }
            else
            {
                return null;
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
