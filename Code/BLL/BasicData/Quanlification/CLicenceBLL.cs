using DAL.BasicData;
using Model.BasicData;
using Untity;
namespace BLL.BasicData
{
    public class CLicenceBLL
    {

        public static bool Delete(string ids)
        {
            return CLicenceDAL.Delete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(CLicence model)
        {
            return CLicenceDAL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(CLicence model)
        {
            return CLicenceDAL.Update(model);
        }


        public static PagedList<CLicence> GetList(int pageSize, int pageIndex, string strWhere)
        {
            return CLicenceDAL.GetList(pageSize, pageIndex, strWhere);
        }

        public static CLicence Get(int id)
        {
            return CLicenceDAL.Get(id);
        }
    }
}
