using DAL.BasicData;
using Model.EF;
using System.Collections.Generic;
namespace BLL.BasicData
{
    public class CompanyBLL
    {
        private CompanyDAL _dal = new CompanyDAL();

        public int Delete(string ids)
        {
            return _dal.BatchDelete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Company model)
        {
            return _dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Company model)
        {
            return _dal.Update(model);
        }
        public Company Get(int id)
        {
            return _dal.Find(m => m.CompanyID == id);
        }
        public List<Company> FindPagedList(int pageIndex, int pageSize, out int pageCount, out int rowCount, bool isAsc)
        {
            //参考
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, m => m.CompanyID == 1, m => m.CompanyID, true);
        }
    }
}