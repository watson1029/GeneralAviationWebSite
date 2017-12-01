using DAL.BasicData;
using Model.BasicData;
using Model.EF;
using System;
using System.Collections.Generic;
using Untity;
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
            return _dal.Find(m => m.pilotcardNo == id.ToString());
        }
        public List<Pilot> FindPagedList(int pageIndex, int pageSize, out int pageCount, out int rowCount, bool isAsc)
        {
            //参考
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, m => m.pilotcardNo == "1", m => m.pilotcardNo, true);
        }
    }
}