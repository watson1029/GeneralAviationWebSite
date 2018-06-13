using DAL.FlightPlan;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FlightPlan
{
    public class BusyTimeBLL
    {
        private BusyTimeDAL dal = new BusyTimeDAL();
        public bool Add(BusyTime model)
        {
            return dal.Add(model) > 0;
        }
        public bool Update(BusyTime model)
        {
            return dal.Update(model) > 0;
        }
        public bool IsHash(DateTime date)
        {
            return dal.IsHash(date);
        }
        public BusyTime Get(DateTime date)
        {
            return dal.Get(date);
        }
        public List<BusyTime> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<BusyTime, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.BusyDate, false);
        }
    }
}
