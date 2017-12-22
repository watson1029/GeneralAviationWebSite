using DAL.BasicData;
using Model.EF;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace BLL.BasicData
{
    public class PilotBLL
    {
        private PilotDAL _dal = new PilotDAL();
        public int Delete(string ids)
        {
            return _dal.BatchDelete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Pilot model)
        {
            return _dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Pilot model)
        {
            return _dal.Update(model);
        }
        public Pilot Get(int id)
        {
            return _dal.Find(m => m.ID == id);
        }
        public List<Pilot> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Pilot, bool>> where)
        {
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.ID, true);
        }

        public List<Pilot> GetList(Expression<Func<Pilot, bool>> where)
        {
            return _dal.FindList(where, m => m.ID, true);
        }
    }
}