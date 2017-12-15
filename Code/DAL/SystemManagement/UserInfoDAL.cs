using System.Collections.Generic;
using System.Data;
using System.Linq;
using Model.EF;
using System.Data.Entity;

namespace DAL.SystemManagement
{
    public class UserInfoDAL : DBHelper<UserInfo>
    {
        private UserRoleDAL _UserRoleDAL = new UserRoleDAL();
        /// <summary>
        /// 管理员判断
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsAdmin(int userID)
        {
            var temp = from a in context.UserRole
                       join b in context.Role on a.RoleID equals b.ID into tempTb
                       from c in tempTb.DefaultIfEmpty()
                       where a.UserID == userID
                       where c.IsAdmin == true
                       select a.ID;           

            return temp.Count() > 0;
        }

        public bool SetUserRole(int userID, IEnumerable<int> addUserRoleList, IEnumerable<int> removeUserRoleList)
        {
            //foreach (var rmp in removeUserRoleList)
            //{
            //    var temp = context.Set<UserRole>().Where(u => u.UserID == userID && u.RoleID == rmp).FirstOrDefault();
            //    if (temp != null)
            //    {
            //        context.Entry(temp).State = EntityState.Deleted;
            //    }
            //}            
            //foreach (var amp in addUserRoleList)
            //{
            //    var entity = new UserRole()
            //    {
            //        UserID = userID,
            //        RoleID = amp
            //    };
            //    context.Entry(entity).State = EntityState.Added;
            //}
            //return context.SaveChanges() > 0;

            int _DeleteCount = _UserRoleDAL.BatchDelete(a => removeUserRoleList.Contains(a.RoleID));

            List<UserRole> _UserRoleList = new List<UserRole>();
            foreach (var amp in addUserRoleList)
            {
                var entity = new UserRole()
                {
                    UserID = userID,
                    RoleID = amp
                };
                _UserRoleList.Add(entity);
            }
            int _AddCount = _UserRoleDAL.AddList(_UserRoleList);

            if ((_DeleteCount + _AddCount) > 0)
                return true;
            else
                return false;
        }
    }
}
