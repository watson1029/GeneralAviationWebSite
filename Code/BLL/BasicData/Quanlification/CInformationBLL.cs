using DAL.BasicData;
using Model.BasicData;
using Untity;
namespace BLL.BasicData
{
    public class CInformationBLL
    {

        public static bool Delete(string ids)
        {
            return CInformationBLL.Delete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(CInformation model)
        {
            return CInformationBLL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(CInformation model)
        {
            return CInformationBLL.Update(model);
        }


        public static PagedList<CInformation> GetList(int pageSize, int pageIndex, string strWhere)
        {
            return CInformationDAL.GetList(pageSize, pageIndex, strWhere);
        }

        public static CInformation Get(int id)
        {
            return CInformationDAL.Get(id);
        }
    }
}