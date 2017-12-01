using Model.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.SystemManagement
{
    public class MenuDAL : DBHelper<Menu>
    {
        /// <summary>
        /// 【判断是否为父节点】
        /// </summary>
        /// <param name="ID">主键值</param>
        /// <returns>true or false</returns>
        public bool IsParentMenu(int ID)
        {
            var menu = Find(m => m.ParentMenuID == ID);
            if (menu != null)
            {
                return true;
            }
            else
                return false;
        }

        public List<Model.EF.Menu> GetUserMenuList(int userID)
        {
            //SELECT DISTINCT c.RoleID FROM dbo.UserRole c WHERE c.UserID=18
            var roleIDs = from a in context.UserRole
                                  where a.UserID == int.Parse(userID.ToString())
                                  select a.RoleID;
            var distinctRoleIDs = roleIDs.Distinct();

            //SELECT * FROM dbo.RoleMenu a LEFT JOIN dbo.Menu b ON a.MenuID=b.ID
            //WHERE a.RoleID IN (SELECT DISTINCT c.RoleID FROM dbo.UserRole c WHERE c.UserID = 18)
            var menu = from a in context.RoleMenu
                       join b in context.Menu on a.MenuID equals b.ID into tempTb
                       from c in tempTb.DefaultIfEmpty()
                       where distinctRoleIDs.Any(d => d == a.RoleID)
                       select c;
            return menu.ToList();
        } 
    }
}
