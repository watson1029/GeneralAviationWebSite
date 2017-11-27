using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity.DB;

namespace DAL.SystemManagement
{
  public class MenuDAL
  {

      public static List<Menu> GetList(string strWhere)
      {
          SqlDbHelper dao = new SqlDbHelper();
          var sql = string.Format("select * from Menu where {0} ", strWhere);
          return dao.ExecSelectCmd(ExecReader, sql);
      }
      private static Menu ExecReader(SqlDataReader dr)
      {
          Menu menu = new Menu();
          if (!dr["MenuName"].Equals(DBNull.Value))
              menu.MenuName = Convert.ToString(dr["MenuName"]);
          menu.ID = Convert.ToInt32(dr["ID"]);
          if (!dr["LinkUrl"].Equals(DBNull.Value))
              menu.LinkUrl = Convert.ToString(dr["LinkUrl"]);
          if (!dr["ParentMenuID"].Equals(DBNull.Value))
              menu.ParentMenuID = Convert.ToInt32(dr["ParentMenuID"]);
          if (!dr["Description"].Equals(DBNull.Value))
              menu.Description = Convert.ToString(dr["Description"]);
          if (!dr["ImageUrl"].Equals(DBNull.Value))
              menu.ImageUrl = Convert.ToString(dr["ImageUrl"]);
          menu.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
              menu.OrderSort = Convert.ToByte(dr["OrderSort"]);
              menu.MenuLevel = Convert.ToByte(dr["MenuLevel"]);
              if (!dr["MenuCode"].Equals(DBNull.Value))
                  menu.MenuCode = Convert.ToString(dr["MenuCode"]);
          return menu;
      }
      /// <summary>
      /// 是否是父菜单
      /// </summary>
      /// <param name="menuID"></param>
      /// <returns></returns>
      public static bool IsParentMenu( int menuID)
      {
          SqlDbHelper dao = new SqlDbHelper();
          var sql = string.Format("select count(1) from Menu where ParentMenuID=@MenuID ", menuID);
          SqlParameter[] parameters = {
					new SqlParameter("@MenuID", menuID)};

          return Convert.ToInt32(dao.ExecScalar(sql, parameters))>0;
      }
      public static List<Menu> GetUserMenuList(int userID)
      {
          SqlDbHelper dao = new SqlDbHelper();
          var sql = @"select d.* from UserRole a inner join Role b on a.RoleID=b.ID
                    inner join RoleMenu c on b.ID=c.RoleID 
                    inner join Menu d on c.MenuID=d.ID where a.ID=@ID";
          SqlParameter[] parameters = {
					new SqlParameter("@ID", userID)};
          return dao.ExecSelectCmd(ExecReader, sql, parameters);
      }
    }
}
