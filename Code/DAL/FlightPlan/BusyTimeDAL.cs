using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FlightPlan
{
    public class BusyTimeDAL : DBHelper<BusyTime>
    {
        public bool IsHash(DateTime date)
        {
            var linq = from t in context.BusyTime
                       where t.BusyDate == date
                       select t;
            return linq.Count() > 0 ? true : false;
        }

        public BusyTime Get(DateTime date)
        {
            var linq = from t in context.BusyTime
                       where t.BusyDate == date
                       select t;
            return linq.FirstOrDefault();
        }

        public List<BusyTime> GetList()
        {
            var linq = from t in context.BusyTime
                       orderby t.BusyDate descending
                       select t;
            return linq.ToList();
        }
    }
}
