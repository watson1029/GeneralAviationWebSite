using DAL.BasicData;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace BLL.BasicData
{
    public class NewBLL
    {
        private NewDAL _dal = new NewDAL();
        public bool Delete(string ids)
        {
            return _dal.BatchDelete(ids) > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(News model)
        {
            return _dal.Add(model) > 0;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(News model)
        {
            return _dal.Update(model) > 0;
        }
        public News Get(int id)
        {
            return _dal.Find(m => m.NewID == id);
        }

        public List<News> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<News, bool>> where)
        {
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.CreateTime, false);
        }

        public List<News> GetList(Expression<Func<News, bool>> where)
        {
            return _dal.FindList(where, m => m.CreateTime, false);
        }
        public List<News> GetTopList(int top,Expression<Func<News, bool>> where)
        {
            ZHCC_GAPlanEntities context = new ZHCC_GAPlanEntities();
            return context.Set<News>().Where(where).OrderByDescending(m => m.IsTop).ThenByDescending(m => m.CreateTime).AsNoTracking().Take(top).ToList();  
        }
    }
}
