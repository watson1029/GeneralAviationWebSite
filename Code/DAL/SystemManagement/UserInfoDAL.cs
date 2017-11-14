using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Untity.DB;

namespace DAL.SystemManagement
{
    
    public class UserInfoDAL
    {

       private static SqlDbHelper dao= new SqlDbHelper();


        public static bool Delete(int id)
        {
            var sql = "delete from UserInfo WHERE ID=@ID";
            SqlParameter[] parameters = {
					new SqlParameter("@ID", id)
			};
            return dao.ExecNonQuery(sql, parameters) > 0;
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

        /// <summary>
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="getFields">需要返回的列</param>
        /// <param name="orderName">排序的字段名</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="isGetCount">返回记录总数,非 0 值则返回</param>
        /// <param name="orderType">设置排序类型,0表示升序非0降序</param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static DataTable GetList(string tableName, string getFields, string orderName, int pageSize, int pageIndex, bool isGetCount, bool orderType, string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                  new SqlParameter("@PageSize", SqlDbType.Int),
               new SqlParameter("@PageIndex", SqlDbType.Int),
                new SqlParameter("@doCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar, 1500)            
                                     };
            parameters[0].Value = tableName;
            parameters[1].Value = getFields;
            parameters[2].Value = orderName;
            parameters[3].Value = pageSize;
            parameters[4].Value = pageIndex;
            parameters[5].Value = isGetCount ? 1 : 0;
            parameters[6].Value = orderType ? 1 : 0;
            parameters[7].Value = strWhere;
            return dao.ExecProcedureSelect("pro_pageList", parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DataTable Get(int id)
        {
            var sql = "select  top 1 * from UserInfo where ID=@ID";
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return dao.ExecSelectCmd(sql, parameters);
        }

        //private static UserInfo ExecReader(SqlDataReader dr)
        //{
        //    UserInfo userinfo = new UserInfo();
        //    userinfo.UserName = Convert.ToString(dr["UserName"]);
        //    userinfo.ID = Convert.ToInt32(dr["ID"]);
        //    userinfo.Mobile = Convert.ToString(dr["Mobile"]);
        //    userinfo.IsGeneralAviation = Convert.ToBoolean(dr["IsGeneralAviation"]);
        //    userinfo.CompanyCode3 = Convert.ToString(dr["CompanyCode3"]);
        //    if (!dr["Status"].Equals(DBNull.Value))
        //    userinfo.Status = Convert.ToByte(dr["Status"]);
        //    return userinfo;
        //}
    }
}
