using Model.EF;
using System.Collections.Generic;
using Untity;
using System.Linq;
using System;

namespace DAL.BasicData
{
    public class AircraftDAL : DBHelper<Aircraft>
    {

        public List<TemplateClass4StatisticResult> getStatisticResult()
        {
            //TemplateClass4StatisticResult:CompanyCode CompanyName Count CreateTime
            var res = from a in context.Aircraft
                      group a by a.CompanyCode3 into b
                      join d in context.Company on b.Key equals d.CompanyCode3 into e
                      from f in e.DefaultIfEmpty()
                      select new TemplateClass4StatisticResult
                      {
                          _field1 = b.Key,
                          _field2 = f.CompanyName,
                          _field3 = b.Count(),
                          _field4 = (DateTime)f.CreateTime
                      };

            return res.ToList();
        }

    }
}
