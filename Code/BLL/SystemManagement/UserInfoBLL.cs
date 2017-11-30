using DAL.SystemManagement;
using Model.EF;
using Model.SystemManagement;
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
        public static UserInfo Get(string userName)
        {
            return UserInfoDAL.Get(userName);
        }
        /// <summary>
        /// 获取用户的menucode
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<string> GetUserPermissions(int userID)
        {
            List<string> list = new List<string>();
            List<Menu> menuList = null;
            //管理员判断
            if (UserInfoDAL.IsAdmin(userID))
            {
                menuList = MenuDAL.GetList("1=1");
                if (menuList != null & menuList.Any())
                {
                    list = menuList.Select(u => (u.MenuCode ?? "")).Distinct().ToList();

                }
            }
            else
            {
                menuList = MenuDAL.GetUserMenuList(userID);
                if (menuList != null && menuList.Any())
                {
                    list = menuList.Select(u => (u.MenuCode ?? "")).Distinct().ToList();
                }
            }
            return list;
        }
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Menu> GetUserMenu(int userID)
        {
            List<Menu> menuList = null;
            //管理员判断
            if (UserInfoDAL.IsAdmin(userID))
            {
                menuList = MenuDAL.GetList("1=1");
            }
            else
            {
                menuList = MenuDAL.GetUserMenuList(userID);
            }
            return menuList;
        }
    }
}
