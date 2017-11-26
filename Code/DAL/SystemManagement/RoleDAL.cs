using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;
using Untity.DB;

namespace DAL.SystemManagement
{
   public class RoleDAL
    {
        private static SqlDbHelper dao = new SqlDbHelper();


        public static bool Delete(string ids)
        {
            var sql = string.Format("delete from Role WHERE (ID IN ({0}))", ids);

            return dao.ExecNonQuery(sql) > 0;
        }
        public static bool Add(Role model)
        {
            var sql = @"insert into Role(RoleName,Description,IsAdmin,CreateTime)
                          values (@RoleName,@Description,@IsAdmin,@CreateTime)";
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName",  model.RoleName),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@IsAdmin", model.IsAdmin),
					new SqlParameter("@CreateTime", model.CreateTime)};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }
        public static bool Update(Role model)
        {
            var sql = @"update Role set RoleName=@RoleName,Description=@Description,IsAdmin=@IsAdmin where ID=@ID";
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName",model.RoleName),
					new SqlParameter("@Description",  model.Description),
					new SqlParameter("@IsAdmin",model.IsAdmin),
					new SqlParameter("@ID", model.ID)};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }


        public static PagedList<Role> GetList(int pageSize, int pageIndex, string strWhere)
        {
            var sql = string.Format("select * from Role where {0}", strWhere);
            return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<Role>()).ToPagedList<Role>(pageIndex, pageSize);

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Role Get(int id)
        {
            var sql = "select  top 1 * from Role where ID=@ID";
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            return dao.ExecSelectSingleCmd<Role>(ExecReader, sql, parameters);
        }

        private static Role ExecReader(SqlDataReader dr)
        {
            Role role = new Role();
            if (!dr["RoleName"].Equals(DBNull.Value))
                role.RoleName = Convert.ToString(dr["RoleName"]);
            if (!dr["Description"].Equals(DBNull.Value))
                role.Description = Convert.ToString(dr["Description"]);
            role.ID = Convert.ToInt32(dr["ID"]);
            role.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            role.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);
            return role;
        }
     

        public static int GetRoleMenuCount(int roleID,int menuID)
        {
            var sql = string.Format("select count(1) from RoleMenu where MenuID=@MenuID and RoleID=@RoleID ", menuID, roleID);
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID",roleID),
					new SqlParameter("@MenuID", menuID)};

            return Convert.ToInt32(dao.ExecScalar(sql, parameters));
        }
        public static List<RoleMenu> GetRoleMenuList(string strWhere)
        {
            var sql = string.Format("select * from RoleMenu where {0} ", strWhere);
            return dao.ExecSelectCmd(rmExecReader, sql);
        }
   private static RoleMenu rmExecReader(SqlDataReader dr)
        {
            RoleMenu roleMenu = new RoleMenu();
            roleMenu.RoleID = Convert.ToInt32(dr["RoleID"]);
            roleMenu.MenuID = Convert.ToInt32(dr["MenuID"]);
            return roleMenu;
        }
   public static bool SetRoleMenu(int roleID, IEnumerable<int> addRoleMenuList, IEnumerable<int> removeRoleMenuList)
        {
            try
            {
                dao.BeginTran();
                foreach (var rmp in removeRoleMenuList)
                {
                    //删除
                    var sql = @"delete from RoleMenu where RoleID=@RoleID and MenuID=@MenuID";
                    SqlParameter[] parameters = {
					new SqlParameter("@RoleID",roleID),
					new SqlParameter("@MenuID", rmp)};
                     dao.ExecNonQuery(sql, parameters);
                }
                foreach (var amp in addRoleMenuList)
                {
                    var sql = @"insert into RoleMenu
                        (RoleID,MenuID)
                        values (@RoleID,@MenuID)";
                    SqlParameter[] parameters = {
					new SqlParameter("@RoleID",roleID),
					new SqlParameter("@MenuID", amp)};
                    dao.ExecNonQuery(sql, parameters);
                }
                dao.CommitTran();
                return true;

            }
            catch (Exception e)
            {
                dao.RollBackTran();
                throw (e);

            }
        }
        

    }
}
