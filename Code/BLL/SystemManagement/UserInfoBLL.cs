using DAL.SystemManagement;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Untity;
namespace BLL.SystemManagement
{
    public class UserInfoBLL
    {

        public static bool Delete(string ids)
        {
            return UserInfoDAL.Delete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  static bool Add(UserInfo model)
        {
            return UserInfoDAL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(UserInfo model)
        {
            return UserInfoDAL.Update(model);
        }


        public static PagedList<UserInfo> GetList(int pageSize, int pageIndex, string strWhere)
        {
            return UserInfoDAL.GetList(pageSize, pageIndex, strWhere);
        }

        public static UserInfo Get(int id)
        {
            return UserInfoDAL.Get(id);
        }
    }
}
