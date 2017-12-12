using System.Collections.Generic;
using System.Data;
using System.Linq;
using Model.EF;
using System.Data.Entity;

namespace DAL.SystemManagement
{
    public class UserInfoDAL : DBHelper<UserInfo>
    {       
        /// <summary>
        /// 管理员判断
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsAdmin(int userID)
        {
            //SqlDbHelper dao = new SqlDbHelper();
            //var sql = "select Count(1) from UserRole a inner join  Role b on a.RoleID=b.ID where b.IsAdmin=1 and a.UserID=@UserID ";
            //SqlParameter[] parameters = {
            //        new SqlParameter("@UserID", userID)};
            //return Convert.ToInt32(dao.ExecScalar(sql, parameters)) > 0;

            //SELECT a.ID FROM dbo.UserRole a LEFT JOIN dbo.Role b ON b.ID = a.ID
            //WHERE b.IsAdmin = '1' AND a.UserID=userID
            var temp = from a in context.UserRole
                       join b in context.Role on a.RoleID equals b.ID into tempTb
                       from c in tempTb.DefaultIfEmpty()
                       where a.UserID == userID
                       where c.IsAdmin == true
                       select a.ID;           

            return temp.Count() > 0;
        }

        public Dictionary<string,int> GetGroupCount()
        {
            var result = (from a in context.RepetitivePlan
                          group a by a.CompanyCode3 into g
                          select new { name = g.Key, count = g.Count() })
                         .ToDictionary(a => a.name, a => a.count);
            return result;
        }

        public dynamic GetGroupCount1()
        {
            var result = from a in context.RepetitivePlan
                          group a by a.CompanyCode3 into g
                          select new { name = g.Key, count = g.Count() };
            return result;
        }

        public bool SetUserRole(int userID, IEnumerable<int> addUserRoleList, IEnumerable<int> removeUserRoleList)
        {
            foreach (var rmp in removeUserRoleList)
            {
                var temp = context.Set<UserRole>().Where(u => u.UserID == userID && u.RoleID == rmp).FirstOrDefault();
                if (temp != null)
                {
                    context.Entry<UserRole>(temp).State = EntityState.Deleted;
                }
            }
            foreach (var amp in addUserRoleList)
            {
                var entity = new UserRole()
                {
                    UserID = userID,
                    RoleID = amp
                };
                context.Entry<UserRole>(entity).State = EntityState.Added;
            }
            return context.SaveChanges() > 0;
        }
    }
}
