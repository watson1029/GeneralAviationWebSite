using DAL.SystemManagement;
using Model.SystemManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SystemManagement
{
  public class MenuBLL
  {
      /// <summary>
      /// 递归生成树
      /// </summary>
      /// <param name="menuID"></param>
      /// <param name="roleID"></param>
      /// <param name="isAdmin"></param>
      /// <returns></returns>
      public static List<TreeNode> CreateMenuTree(int? menuID,int roleID,bool isAdmin)
      {
          var whereStr = string.Empty;
         whereStr = menuID.HasValue?string.Format("ParentMenuID={0} order by OrderSort asc",menuID.Value):"ParentMenuID is null order by OrderSort asc";
          var menulist=MenuDAL.GetList(whereStr);
          var treeList = new List<TreeNode>();
          if (menulist != null && menulist.Any())
          {
              foreach (var item in menulist)
              {
                  var node = new TreeNode();
                  node.id = item.ID;
                  node.text = item.MenuName;
                  if (isAdmin)
                  {
                      node.@checked = true;
                  }
                  else
                  {
                      if (RoleDAL.GetRoleMenuCount(roleID, item.ID) > 0)
                      {
                          node.@checked = true;
                      }
                  }
                  var _treeList = new List<TreeNode>();
                  if (MenuDAL.IsParentMenu(item.ID))
                  {
                      _treeList = CreateMenuTree(item.ID, roleID, isAdmin);
                  }
                  node.children = _treeList.ToArray();
                  treeList.Add(node);
              }
          }
          return treeList;
      }
      public static string CreateMenuJson(int userID)
      {

          List<Menu> userMenus = UserInfoBLL.GetUserMenu(userID);
          var menuList = new List<MenuModel>();
          if (userMenus != null && userMenus.Any())
          {
              var topMenus = userMenus.Where(u => u.MenuLevel == 1).OrderBy(u => u.OrderSort);
              foreach (var item in topMenus)
              {
                  var model = new MenuModel()
                  {
                      menuid = item.ID.ToString(),
                      icon = item.ImageUrl,
                      menuname = item.MenuName
                  };
                  var secMenuList = new List<MenuModel>();
                  var secMenus = userMenus.Where(u => u.ParentMenuID == item.ID).OrderBy(u => u.OrderSort).ToList();
                  if (secMenus != null && secMenus.Any())
                  {
                      secMenuList.AddRange(secMenus.Select(dto => new MenuModel
                      {
                          menuid = dto.ID.ToString(),
                          icon = dto.ImageUrl,
                          menuname = dto.MenuName,
                          url = dto.LinkUrl
                      }));
                      model.menus = secMenuList.ToArray();
                  }
                  menuList.Add(model);
              }

          }
          return JsonConvert.SerializeObject(menuList);


      }
    }
}
