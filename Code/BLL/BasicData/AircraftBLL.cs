using DAL.BasicData;
using Model.EF;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace BLL.BasicData
{
    public class AircraftBLL
    {
        private AircraftDAL _dal = new AircraftDAL();
        
        public int Delete(string ids)
        {
            return _dal.BatchDelete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Aircraft model)
        {
            return _dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Aircraft model)
        {
            return _dal.Update(model);
        }
        public Aircraft Get(int id)
        {
            return _dal.Find(m => m.AircraftID == id);
        }
        public List<Aircraft> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Aircraft, bool>> where)
        {
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.AircraftID, true);
        }

        public List<Aircraft> GetList(Expression<Func<Aircraft, bool>> where)
        {
            return _dal.FindList(where, m => m.AircraftID, true);
        }

    }
}
