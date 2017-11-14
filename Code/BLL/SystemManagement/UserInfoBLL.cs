using DAL.SystemManagement;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SystemManagement
{
    public class UserInfoBLL
    {
    //    /// <summary>
    //    /// 获取指定名称的用户
    //    /// </summary>
    //    /// <param name="userName">用户名称</param>
    //    /// <returns></returns>
    //    public static UserInfo GetUserByName(string userName)
    //    {
    //        return UserInfoDAL.GetUserByName(userName);
    //    }
        public static bool Delete(int id)
        {
            return UserInfoDAL.Delete(id);
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


        public static DataTable GetList(string tableName, string getFields, string orderName, int pageSize, int pageIndex, bool isGetCount, bool orderType, string strWhere)
        {
            return UserInfoDAL.GetList(tableName,getFields,orderName, pageSize, pageIndex, isGetCount, orderType,strWhere);
        }

        public static DataTable Get(int id)
        {
            return UserInfoDAL.Get(id);
        }
    }
}
