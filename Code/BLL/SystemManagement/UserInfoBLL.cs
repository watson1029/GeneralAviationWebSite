using DAL.SystemManagement;
using Model.EF;
using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Untity;
namespace BLL.SystemManagement
{
    public class UserInfoBLL
    {
        private MenuDAL menudal = new MenuDAL();
        private UserInfoDAL userinfodal = new UserInfoDAL();
        private ZHCC_GAPlanEntities context = new ZHCC_GAPlanEntities();

        public  bool Delete(string ids)
        public bool Delete(string ids)
        {
            return userinfodal.BatchDelete(ids)>0;
            return userinfodal.BatchDelete(ids) > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  bool Add(UserInfo model)
        public bool Add(UserInfo model)
        {
            return userinfodal.Add(model)>0;
            return userinfodal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UserInfo model)
        {
            return userinfodal.Update(model)>0;
            return userinfodal.Update(model) > 0;
        }


        public List<UserInfo> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<UserInfo, bool>> where)
        {
            return userinfodal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.ID, true);
        }

        public  UserInfo Get(int id)
        public UserInfo Get(int id)
        {
            return userinfodal.Find(u=>u.ID==id);
            return userinfodal.Find(u => u.ID == id);
        }
        public  UserInfo Get(string userName)
        public UserInfo Get(string userName)
        {
            return userinfodal.Find(u => u.UserName == userName);
        }
        /// <summary>
        /// 获取用户的menucode
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> GetUserPermissions(int userID)
        {
            List<string> list = new List<string>();
            List<Menu> menuList = null;
            //管理员判断
            if (userinfodal.IsAdmin(userID))
            {
                //menuList = menudal.GetList("1=1");
                menuList = menudal.FindList().ToList();
                menuList = menudal.FindList(u=>u.ID,false).ToList();
                if (menuList != null & menuList.Any())
                {
                    list = menuList.Select(u => (u.MenuCode ?? "")).Distinct().ToList();

                }
            }
            else
            {
                //menuList = MenuDAL.GetUserMenuList(userID);
                menuList = menudal.GetUserMenuList(userID);
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
        public List<Menu> GetUserMenu(int userID)
        {
            List<Menu> menuList = null;
            //管理员判断
            if (userinfodal.IsAdmin(userID))
            {
                menuList = menudal.FindList().ToList();
                menuList = menudal.FindList(u=>u.ID,true).ToList();
            }
            else
            {
                menuList = menudal.GetUserMenuList(userID);
            }
            return menuList;
        }


        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<int> GetUserRoleList(int userID)
        {
            var list = context.UserRole.Where(p => p.UserID == userID).Select(t => t.RoleID).ToList();
            return list;
        }
        public bool SetUserRole(int userID, IEnumerable<int> addUserRoleList, IEnumerable<int> removeUserRoleList)
        {
            return userinfodal.SetUserRole(userID, addUserRoleList, removeUserRoleList);
        }
    }
}
