using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Untity.DB;
using Untity;

namespace DAL.SystemManagement
{
    
    public class UserInfoDAL
    {

       private static SqlDbHelper dao= new SqlDbHelper();


        public static bool Delete(string ids)
        {
            var sql = string.Format("delete from UserInfo WHERE  (ID IN ({0}))", ids);
            return dao.ExecNonQuery(sql) > 0;
        }
        public static bool Add(UserInfo model)
        {
            var sql =@"insert into UserInfo(UserName,Password,Mobile,Status,CreateTime,IsGeneralAviation)
                          values (@UserName,@Password,@Mobile,@Status,@CreateTime,@IsGeneralAviation)";
            SqlParameter[] parameters = {
					new SqlParameter("@UserName",  model.UserName),
					new SqlParameter("@Password", model.Password),
					new SqlParameter("@Mobile", model.Mobile),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@CreateTime", model.CreateTime),
					new SqlParameter("@IsGeneralAviation", model.IsGeneralAviation),};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }
        public static bool Update(UserInfo model)
        {
            var sql = @"update UserInfo set UserName=@UserName,Password=@Password,Mobile=@Mobile,Status=@Status,IsGeneralAviation=@IsGeneralAviation where ID=@ID";
            SqlParameter[] parameters = {
					new SqlParameter("@UserName",model.UserName),
					new SqlParameter("@Password",  model.Password),
					new SqlParameter("@Mobile",model.Mobile),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@IsGeneralAviation", model.IsGeneralAviation),
					new SqlParameter("@ID", model.ID)};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }


        public static PagedList<UserInfo> GetList(int pageSize, int pageIndex, string strWhere)
        {
           var sql = string.Format("select * from UserInfo where {0}",strWhere);
           return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<UserInfo>()).ToPagedList<UserInfo>(pageIndex, pageSize); 
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static UserInfo Get(int id)
        {
            var sql = "select  top 1 * from UserInfo where ID=@ID";
            SqlParameter[] parameters = {
					new SqlParameter("@ID",id)
			};
            return dao.ExecSelectSingleCmd<UserInfo>(ExecReader,sql, parameters);
        }
        public static UserInfo Get(string userName)
        {
            var sql = "select  top 1 * from UserInfo where UserName=@UserName";
            SqlParameter[] parameters = {
					new SqlParameter("@UserName",userName)
			};
            return dao.ExecSelectSingleCmd<UserInfo>(ExecReader, sql, parameters);
        }
        private static UserInfo ExecReader(SqlDataReader dr)
        {
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = Convert.ToString(dr["UserName"]);
            userinfo.ID = Convert.ToInt32(dr["ID"]);
            userinfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            if (!dr["Mobile"].Equals(DBNull.Value))
            userinfo.Mobile = Convert.ToString(dr["Mobile"]);
            userinfo.Password =Convert.ToString(dr["Password"]);
            userinfo.IsGeneralAviation = Convert.ToByte(dr["IsGeneralAviation"]);
            if (!dr["CompanyCode3"].Equals(DBNull.Value))
            userinfo.CompanyCode3 = Convert.ToString(dr["CompanyCode3"]);
                userinfo.Status = Convert.ToByte(dr["Status"]);
            return userinfo;
        }
        /// <summary>
        /// 管理员判断
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool IsAdmin(int userID)
        {
            var sql = "select Count(1) from UserRole a inner join  Role b on a.RoleID=b.ID where b.IsAdmin=1 and a.UserID=@UserID ";
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", userID)};

            return Convert.ToInt32(dao.ExecScalar(sql, parameters)) > 0;
        }
    }
}
