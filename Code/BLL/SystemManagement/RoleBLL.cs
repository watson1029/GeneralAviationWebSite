﻿using DAL.SystemManagement;
using Model.EF;
using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Untity;

namespace BLL.SystemManagement
{
    
    public class RoleBLL
    {
        ZHCC_GAPlanEntities context=new ZHCC_GAPlanEntities();
        RoleDAL roledal = new RoleDAL();
        public  bool Delete(string ids)
        {
            return roledal.BatchDelete(ids)>0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  bool Add(Role model)
        {
            return roledal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public  bool Update(Role model)
        {
            return roledal.Update(model) > 0;
        }

        public List<Role> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Role, bool>> where)
        {
            return roledal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.ID, true);
        }
       /// <summary>
       /// 获取角色拥有的menuid
       /// </summary>
       /// <param name="strWhere"></param>
       /// <returns></returns>
        public  List<int> GetRoleMenuList(string strWhere)
        {
            var list= (roledal.GetRoleMenuList(strWhere)??new List<RoleMenu>()).Select(t => t.MenuID.Value).ToList();
            return list;
        }
        public  Role Get(int id)
        {
            return roledal.Find(u=>u.ID==id);
        }
        public  bool SetRoleMenu(int roleID, IEnumerable<int> addRoleMenuList, IEnumerable<int> removeRoleMenuList)
        {
            return roledal.SetRoleMenu(roleID, addRoleMenuList, removeRoleMenuList);
        }

        public List<TreeNode> CreateRoleTree(int userID)
        {
            List<Role> rolelist = roledal.FindList(u=>u.ID,true);

            var treeList = new List<TreeNode>();
            if (rolelist != null && rolelist.Any())
            {
                foreach (var item in rolelist)
                {
                    var node = new TreeNode();
                    node.id = item.ID;
                    node.text = item.RoleName;
                    if (context.UserRole.Any(p => p.UserID == userID && p.RoleID == item.ID))
                    { node.@checked = true;}
                  
                    treeList.Add(node);
                }
            }
            return treeList;
        }
    }
}
