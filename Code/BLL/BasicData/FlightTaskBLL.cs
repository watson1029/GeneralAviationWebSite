using DAL.BasicData;
using Model.BasicData;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BasicData
{
   public class FlightTaskBLL
    {
        private FlightTaskDAL _dal = new FlightTaskDAL();
        public int Delete(string ids)
        {
            return _dal.RemoveList(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FlightTask model)
        {
            return _dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(FlightTask model)
        {
            return _dal.Update(model);
        }
        public FlightTask Get(int id)
        {
            return _dal.Find(m => m.TaskCode == id.ToString());
        }
        public List<FlightTask> FindPagedList(int pageIndex, int pageSize, out int pageCount, out int rowCount, bool isAsc)
        {
            //参考
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, m => m.TaskCode == "1", m => m.TaskCode, true);
        }
    }
}
