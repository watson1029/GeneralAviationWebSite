using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Untity.DB;
using Model.EF;
using System.Data.Entity;

namespace DAL.SystemManagement
{
    public class UserInfoDAL : DBHelper<UserInfo>
    {

        //public  bool Delete(string ids)
        //{
        //    SqlDbHelper dao = new SqlDbHelper();
        //    var sql = string.Format("delete from UserInfo WHERE  (ID IN ({0}))", ids);
        //    return dao.ExecNonQuery(sql) > 0;
        //}
        //        public  bool Add(UserInfo model)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = @"insert into UserInfo(UserName,Password,Mobile,Status,CreateTime,IsGeneralAviation)
        //                          values (@UserName,@Password,@Mobile,@Status,@CreateTime,@IsGeneralAviation)";
        //            SqlParameter[] parameters = {
        //                    new SqlParameter("@UserName",  model.UserName),
        //                    new SqlParameter("@Password", model.Password),
        //                    new SqlParameter("@Mobile", model.Mobile),
        //                    new SqlParameter("@Status", model.Status),
        //                    new SqlParameter("@CreateTime", model.CreateTime),
        //                    new SqlParameter("@IsGeneralAviation", model.IsGeneralAviation),};
        //            return dao.ExecNonQuery(sql, parameters) > 0;

        //        }
        //public  bool Update(UserInfo model)
        //{
        //    var sql = @"update UserInfo set UserName=@UserName,Password=@Password,Mobile=@Mobile,Status=@Status,IsGeneralAviation=@IsGeneralAviation where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@UserName",model.UserName),
        //            new SqlParameter("@Password",  model.Password),
        //            new SqlParameter("@Mobile",model.Mobile),
        //            new SqlParameter("@Status", model.Status),
        //            new SqlParameter("@IsGeneralAviation", model.IsGeneralAviation),
        //            new SqlParameter("@ID", model.ID)};
        //    return dao.ExecNonQuery(sql, parameters) > 0;

        //}


        //public static PagedList<UserInfo> GetList(int pageSize, int pageIndex, string strWhere)
        //{
        //    var sql = string.Format("select * from UserInfo where {0}", strWhere);
        //    return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<UserInfo>()).ToPagedList<UserInfo>(pageIndex, pageSize);
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public static UserInfo Get(int id)
        //{
        //    var sql = "select  top 1 * from UserInfo where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID",id)
        //    };
        //    return dao.ExecSelectSingleCmd<UserInfo>(ExecReader, sql, parameters);
        //}
        //public static UserInfo Get(string userName)
        //{
        //    var sql = "select  top 1 * from UserInfo where UserName=@UserName";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@UserName",userName)
        //    };
        //    return dao.ExecSelectSingleCmd<UserInfo>(ExecReader, sql, parameters);
        //}
        //private static UserInfo ExecReader(SqlDataReader dr)
        //{
        //    UserInfo userinfo = new UserInfo();
        //    userinfo.UserName = Convert.ToString(dr["UserName"]);
        //    userinfo.ID = Convert.ToInt32(dr["ID"]);
        //    userinfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
        //    if (!dr["Mobile"].Equals(DBNull.Value))
        //        userinfo.Mobile = Convert.ToString(dr["Mobile"]);
        //    userinfo.Password = Convert.ToString(dr["Password"]);
        //    userinfo.IsGeneralAviation = Convert.ToByte(dr["IsGeneralAviation"]);
        //    if (!dr["CompanyCode3"].Equals(DBNull.Value))
        //        userinfo.CompanyCode3 = Convert.ToString(dr["CompanyCode3"]);
        //    userinfo.Status = Convert.ToByte(dr["Status"]);
        //    return userinfo;
        //}
        /// <summary>
        /// 管理员判断
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsAdmin(int userID)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = "select Count(1) from UserRole a inner join  Role b on a.RoleID=b.ID where b.IsAdmin=1 and a.UserID=@UserID ";
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", userID)};

            return Convert.ToInt32(dao.ExecScalar(sql, parameters)) > 0;
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
                context.Entry<UserRole>(entity).State = System.Data.Entity.EntityState.Added;
            }
            return context.SaveChanges()>0;

        }
    }
}
