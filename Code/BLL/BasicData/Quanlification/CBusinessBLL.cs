using DAL.BasicData;
using Model.BasicData;
using Untity;
namespace BLL.BasicData
{
    public class CBusinessBLL
    {

        public static bool Delete(string ids)
        {
            return CBusinessDAL.Delete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(CBusiness model)
        {
            return CBusinessDAL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(CBusiness model)
        {
            return CBusinessDAL.Update(model);
        }


        public static PagedList<CBusiness> GetList(int pageSize, int pageIndex, string strWhere)
        {
            return CBusinessDAL.GetList(pageSize, pageIndex, strWhere);
        }

        public static CBusiness Get(int id)
        {
            return CBusinessDAL.Get(id);
        }
    }
}
