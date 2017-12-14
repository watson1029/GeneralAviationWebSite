using Model.EF;
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
   public class RoleDAL : DBHelper<Role>
    {
       public  int GetRoleMenuCount(int roleID, int menuID)
       {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = string.Format("select count(1) from RoleMenu where MenuID=@MenuID and RoleID=@RoleID ", menuID, roleID);
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleID",roleID),
                    new SqlParameter("@MenuID", menuID)};
            return Convert.ToInt32(dao.ExecScalar(sql, parameters));


            //return 0;
       }
       public  List<RoleMenu> GetRoleMenuList(string strWhere)
       {
           SqlDbHelper dao = new SqlDbHelper();
           var sql = string.Format("select * from RoleMenu where {0} ", strWhere);
           return dao.ExecSelectCmd(rmExecReader, sql);
       }
       private  RoleMenu rmExecReader(SqlDataReader dr)
       {
           RoleMenu roleMenu = new RoleMenu();
           roleMenu.RoleID = Convert.ToInt32(dr["RoleID"]);
           roleMenu.MenuID = Convert.ToInt32(dr["MenuID"]);
           return roleMenu;
       }
       public  bool SetRoleMenu(int roleID, IEnumerable<int> addRoleMenuList, IEnumerable<int> removeRoleMenuList)
       {
           SqlDbHelper dao = new SqlDbHelper();
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
