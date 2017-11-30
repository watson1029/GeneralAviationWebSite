using DAL.SystemManagement;
using Model.EF;
using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;

namespace BLL.SystemManagement
{
    public class RoleBLL
    {
        public static bool Delete(string ids)
        {
            return RoleDAL.Delete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(Role model)
        {
            return RoleDAL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Role model)
        {
            return RoleDAL.Update(model);
        }


        public static PagedList<Role> GetList(int pageSize, int pageIndex, string strWhere)
        {
            return RoleDAL.GetList(pageSize, pageIndex, strWhere);
        }
       /// <summary>
       /// 获取角色拥有的menuid
       /// </summary>
       /// <param name="strWhere"></param>
       /// <returns></returns>
        public static List<int> GetRoleMenuList(string strWhere)
        {
            var list= (RoleDAL.GetRoleMenuList(strWhere)??new List<RoleMenu>()).Select(t => t.MenuID).ToList();
            return list;
        }
        public static Role Get(int id)
        {
            return RoleDAL.Get(id);
        }
        public static bool SetRoleMenu(int roleID, IEnumerable<int> addRoleMenuList, IEnumerable<int> removeRoleMenuList)
        {
            return RoleDAL.SetRoleMenu(roleID, addRoleMenuList, removeRoleMenuList);
        }
    }
}
