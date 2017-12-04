using DAL.BasicData;
using Model.EF;
using System.Collections.Generic;
using System;

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
        public List<Pilot> FindPagedList(int pageIndex, int pageSize, out int pageCount, out int rowCount, bool isAsc)
        {
            //参考
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, m => m.ID == 1, m => m.ID, true);
        }

        public object GetList(int size, int page, string strWhere)
        {
            throw new NotImplementedException();
        }
    }
}