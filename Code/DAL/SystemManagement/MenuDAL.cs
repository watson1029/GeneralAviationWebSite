using Model.EF;
using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Untity.DB;

namespace DAL.SystemManagement
{
  public class MenuDAL
  {
        private ZHCC_GAPlanEntities db = new ZHCC_GAPlanEntities();

        //使用EF
        #region

        /// <summary>
        /// 【新增】:字段验证在BLL中进行
        /// </summary>
        /// <returns>true or false</returns>
        public bool Add(string MenuName, Nullable<int> ParentMenuID, string LinkUrl, string Description,
            System.DateTime CreateTime, string ImageUrl, byte OrderSort, byte MenuLevel, string MenuCode)
        {
            Model.EF.Menu menu = new Model.EF.Menu()
            {
                MenuName = MenuName,
                ParentMenuID = ParentMenuID,
                LinkUrl = LinkUrl,
                Description = Description,
                CreateTime = DateTime.Now,
                ImageUrl = ImageUrl,
                OrderSort = OrderSort,
                MenuLevel = MenuLevel,
                MenuCode = MenuCode
            };

            db.Menu.Add(menu);
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 【删除】
        /// </summary>
        /// <param name="ID">主键值</param>
        /// <returns>true or false</returns>
        public bool Delete(int? ID)
        {
            if (ID == null) return false;

            var menu = db.Menu.Find(ID);
            if (menu != null)
            {
                db.Menu.Remove(menu);
                return db.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 【修改】
        /// </summary>
        /// <returns>true or false</returns>
        public bool Update(int? ID, string MenuName, Nullable<int> ParentMenuID, string LinkUrl, string Description,
            System.DateTime CreateTime, string ImageUrl, byte OrderSort, byte MenuLevel, string MenuCode)
        {
            if (ID == null) return false;

            var menu = db.Menu.Find(ID);
            if (menu != null)
            {
                menu.MenuName = MenuName;
                menu.ParentMenuID = ParentMenuID;
                menu.LinkUrl = LinkUrl;
                menu.Description = Description;
                menu.ImageUrl = ImageUrl;
                menu.OrderSort = OrderSort;
                menu.MenuLevel = MenuLevel;
                menu.MenuCode = MenuCode;

                return db.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 【查询】：获取指定主键值的记录
        /// </summary>
        /// <param name="ID">主键值</param>
        /// <returns>Model.EF.Menu</returns>
        public Model.EF.Menu Get(int? ID)
        {
            if (ID == null) return null;
            return db.Menu.Find(ID);
        }

        /// <summary>
        /// 【判断是否为父节点】
        /// </summary>
        /// <param name="ID">主键值</param>
        /// <returns>true or false</returns>
        public bool IsParentMenu(int? ID)
        {
            if (ID == null) return false;
            var menu= db.Menu.Where(m=>m.ParentMenuID == ID).FirstOrDefault();
            if (menu != null)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 【查询】：获取全部记录，按照指定方式排序
        /// 调用方式:
        /// MenuDAL dal = new MenuDAL();
        /// List<Model.EF.Menu> result=dal.GetAllListOrderBy(m => m.ID, true);
        /// 按照主键升序排列，如需按照其它列进行排序，指定【m => m.ID】为所需列名即可
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.Menu></returns>
        public List<Model.EF.Menu> GetAllListOrderBy<TKey>(Expression<Func<Model.EF.Menu, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.Menu.OrderBy(orderBy).ToList();
            else
                return db.Menu.OrderByDescending(orderBy).ToList();
        }

        /// <summary>
        /// 【查询】：按查询条件、排序方式获取记录
        /// 调用方式:
        /// MenuDAL dal = new MenuDAL();
        /// List<Model.EF.Menu> result=dal.GetAllListWhereAndOrderBy(m => m.ID>2, m=>m.ID, true);
        /// 查询条件为ID>2，如需其它查询条件，通过修改【m => m.ID>2】即可
        /// 排序条件为ID,如需按照其它列进行排序，指定【m => m.ID】为所需列名即可
        /// 按照主键升序排列
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="planWhere">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.Menu></returns>
        public List<Model.EF.Menu> GetAllListWhereAndOrderBy<TKey>(Expression<Func<Model.EF.Menu, bool>> planWhere,
            Expression<Func<Model.EF.Menu, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.Menu.Where(planWhere).OrderBy(orderBy).ToList();
            else
                return db.Menu.Where(planWhere).OrderByDescending(orderBy).ToList();
        }

        //public static List<Menu> GetUserMenuList(int userID)
        //{
        //    SqlDbHelper dao = new SqlDbHelper();
        //    var sql = @"select d.* from UserRole a inner join Role b on a.RoleID=b.ID
        //            inner join RoleMenu c on b.ID=c.RoleID 
        //            inner join Menu d on c.MenuID=d.ID where a.ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", userID)};
        //    return dao.ExecSelectCmd(ExecReader, sql, parameters);
        //}

        public List<Model.EF.Menu> GetUserMenuList(int? userID)
        {
            if (userID == null) return null;
            //SELECT DISTINCT c.RoleID FROM dbo.UserRole c WHERE c.UserID=18
            var roleIDs = from a in db.UserRole
                                  where a.UserID == int.Parse(userID.ToString())
                                  select a.RoleID;
            var distinctRoleIDs = roleIDs.Distinct();

            //SELECT * FROM dbo.RoleMenu a LEFT JOIN dbo.Menu b ON a.MenuID=b.ID
            //WHERE a.RoleID IN (SELECT DISTINCT c.RoleID FROM dbo.UserRole c WHERE c.UserID = 18)
            var menu = from a in db.RoleMenu
                       join b in db.Menu on a.MenuID equals b.ID into tempTb
                       from c in tempTb.DefaultIfEmpty()
                       where distinctRoleIDs.Any(d => d == a.RoleID)
                       select c;
            return menu.ToList();
        }


        #endregion


        //未使用EF
        #region

        //public static List<Menu> GetList(string strWhere)
        //{
        //    SqlDbHelper dao = new SqlDbHelper();
        //    var sql = string.Format("select * from Menu where {0} ", strWhere);
        //    return dao.ExecSelectCmd(ExecReader, sql);
        //}


        //private static Menu ExecReader(SqlDataReader dr)
        //{
        //    Menu menu = new Menu();
        //    if (!dr["MenuName"].Equals(DBNull.Value))
        //        menu.MenuName = Convert.ToString(dr["MenuName"]);
        //    menu.ID = Convert.ToInt32(dr["ID"]);
        //    if (!dr["LinkUrl"].Equals(DBNull.Value))
        //        menu.LinkUrl = Convert.ToString(dr["LinkUrl"]);
        //    if (!dr["ParentMenuID"].Equals(DBNull.Value))
        //        menu.ParentMenuID = Convert.ToInt32(dr["ParentMenuID"]);
        //    if (!dr["Description"].Equals(DBNull.Value))
        //        menu.Description = Convert.ToString(dr["Description"]);
        //    if (!dr["ImageUrl"].Equals(DBNull.Value))
        //        menu.ImageUrl = Convert.ToString(dr["ImageUrl"]);
        //    menu.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
        //    menu.OrderSort = Convert.ToByte(dr["OrderSort"]);
        //    menu.MenuLevel = Convert.ToByte(dr["MenuLevel"]);
        //    if (!dr["MenuCode"].Equals(DBNull.Value))
        //        menu.MenuCode = Convert.ToString(dr["MenuCode"]);
        //    return menu;
        //}
        ///// <summary>
        ///// 是否是父菜单
        ///// </summary>
        ///// <param name="menuID"></param>
        ///// <returns></returns>
        //public static bool IsParentMenu(int menuID)
        //{
        //    SqlDbHelper dao = new SqlDbHelper();
        //    var sql = string.Format("select count(1) from Menu where ParentMenuID=@MenuID ", menuID);
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@MenuID", menuID)};

        //    return Convert.ToInt32(dao.ExecScalar(sql, parameters)) > 0;
        //}
        //public static List<Menu> GetUserMenuList(int userID)
        //{
        //    SqlDbHelper dao = new SqlDbHelper();
        //    var sql = @"select d.* from UserRole a inner join Role b on a.RoleID=b.ID
        //            inner join RoleMenu c on b.ID=c.RoleID 
        //            inner join Menu d on c.MenuID=d.ID where a.ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", userID)};
        //    return dao.ExecSelectCmd(ExecReader, sql, parameters);
        //}

        #endregion
    }
}
