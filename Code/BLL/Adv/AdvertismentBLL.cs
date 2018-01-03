using DAL.Adv;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Adv
{
   public  class AdvertismentBLL
    {
       private AdvertismentDAL _dal = new AdvertismentDAL();
        public bool Delete(string ids)
        {
            return _dal.BatchDelete(ids) > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Advertisment model)
        {
            return _dal.Add(model) > 0;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Advertisment model)
        {
            return _dal.Update(model) > 0;
        }
        public Advertisment Get(int id)
        {
            return _dal.Find(m => m.AdvWebSiteID == id&&m.IsUsed==1);
        }

        public List<Advertisment> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Advertisment, bool>> where)
        {
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.AdvWebSiteID, true);
        }

        public List<Advertisment> GetList(Expression<Func<Advertisment, bool>> where)
        {
            return _dal.FindList(where, m => m.AdvWebSiteID, true);
        }

    }
}
