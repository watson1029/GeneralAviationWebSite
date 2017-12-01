using DAL.BasicData;
using Model.BasicData;
using System.Collections.Generic;
using Untity;
namespace BLL.BasicData
{
    public class CInformationBLL
    {
        public CInformationDAL _dal = new CInformationDAL();

        public int Delete(string ids)
        {
            return _dal.BatchDelete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CInformation model)
        {
            return _dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(CInformation model)
        {
            return _dal.Update(model);
        }
        public CInformation Get(int id)
        {
            return _dal.Find(m => m.ID == id);
        }
        public List<CInformation> FindPagedList(int pageIndex, int pageSize, out int pageCount, out int rowCount, bool isAsc)
        {
            //参考
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, m => m.ID == 1, m => m.ID, true);
        }
    }
}