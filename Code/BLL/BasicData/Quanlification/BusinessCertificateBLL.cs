using DAL.BasicData;
using Model.EF;
using System.Collections.Generic;
namespace BLL.BasicData
{
    public class BusinessCertificateBLL
    {
        private BusinessCertificateDAL _dal = new BusinessCertificateDAL();

        public int Delete(string ids)
        {
            return _dal.BatchDelete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BusinessCertificate model)
        {
            return _dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(BusinessCertificate model)
        {
            return _dal.Update(model);
        }
        public BusinessCertificate Get(int id)
        {
            return _dal.Find(m => m.ID == id);
        }
        public List<BusinessCertificate> FindPagedList(int pageIndex, int pageSize, out int pageCount, out int rowCount, bool isAsc)
        {
            //参考
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, m => m.ID == 1, m => m.ID, true);
        }
    }
}
