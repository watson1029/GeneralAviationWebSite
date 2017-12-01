using DAL.BasicData;
using Model.BasicData;
using Model.EF;
using System.Collections.Generic;
using Untity;
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
        public List<Aircraft> FindPagedList(int pageIndex, int pageSize, out int pageCount, out int rowCount, bool isAsc)
        {
            //参考
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, m => m.AircraftID == 1, m => m.AircraftID, true);
        }
    }
}
