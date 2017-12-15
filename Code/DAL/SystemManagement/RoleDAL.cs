using Model.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL.SystemManagement
{
    public class RoleDAL : DBHelper<Role>
    {
        private RoleMenuDAL _RoleMenuDAL = new RoleMenuDAL();
        public int GetRoleMenuCount(int roleID, int menuID)
        {
            var res = from a in context.RoleMenu
                      where a.MenuID == menuID
                      where a.RoleID == roleID
                      select a;
            return res.Count();
        }
        public bool SetRoleMenu(int roleID, IEnumerable<int> addRoleMenuList, IEnumerable<int> removeRoleMenuList)
        {
            //delete from RoleMenu where RoleID=@RoleID and MenuID=@MenuID
            int _DeleteCount = _RoleMenuDAL.BatchDelete(a => removeRoleMenuList.Contains(a.MenuID.Value) && a.RoleID.Value == roleID);

            //insert into RoleMenu(RoleID, MenuID)values(@RoleID, @MenuID)
            List<RoleMenu> _RoleMenuList = new List<RoleMenu>();
            foreach (var amp in addRoleMenuList)
            {
                var entity = new RoleMenu()
                {
                    RoleID = roleID,
                    MenuID = amp
                };
                _RoleMenuList.Add(entity);
            }
            int _AddCount = _RoleMenuDAL.AddList(_RoleMenuList);

            if ((_DeleteCount + _AddCount) > 0)
                return true;
            else
                return false;
        }
    }
}
