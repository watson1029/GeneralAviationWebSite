using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Untity.DB;
using Untity;
using Model.BasicData;

namespace DAL.BasicData
{

    public class CInformationDAL:DBHelper<CInformation>
    {

        //private static SqlDbHelper dao = new SqlDbHelper();


        //public static bool Delete(string ids)
        //{
        //    var sql = string.Format("delete from CInformation WHERE  (ID IN ({0}))", ids);

        //    return dao.ExecNonQuery(sql) > 0;
        //}
        //public static bool Add(CInformation model)
        //{
        //    var sql = @"insert into CInformation(CompanyCode3,CompanyCode2,CompanyName,EnglishName,CreateTime)
        //                  values (@CompanyCode3,@CompanyCode2,@CompanyName,@EnglishName,@CreateTime)";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@CompanyCode3",  model.CompanyCode3),
        //            new SqlParameter("@CompanyCode2", model.CompanyCode2),
        //            new SqlParameter("@CompanyName", model.CompanyName),
        //            new SqlParameter("@EnglishName", model.EnglishName),
        //            new SqlParameter("@CreateTime", model.CreateTime),};
        //    return dao.ExecNonQuery(sql, parameters) > 0;

        //}
        //public static bool Update(CInformation model)
        //{
        //    var sql = @"update CInformation set CompanyCode3=@CompanyCode3,CompanyCode2=@CompanyCode2,CompanyName=@CompanyName,EnglishName=@EnglishName where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@CompanyCode3",model.CompanyCode3),
        //            new SqlParameter("@CompanyCode2",  model.CompanyCode2),
        //            new SqlParameter("@CompanyName",model.CompanyName),
        //            new SqlParameter("@EnglishName", model.EnglishName),
        //            new SqlParameter("@ID", model.ID)};
        //    return dao.ExecNonQuery(sql, parameters) > 0;

        //}


        //public static PagedList<CInformation> GetList(int pageSize, int pageIndex, string strWhere)
        //{
        //    var sql = string.Format("select * from CInformation where {0}", strWhere);
        //    return dao.ExecSelectCmd(ExecReader, sql).ToPagedList<CInformation>(pageIndex, pageSize);

        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public static CInformation Get(int id)
        //{
        //    var sql = "select  top 1 * from CInformation where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = id;
        //    return dao.ExecSelectSingleCmd<CInformation>(ExecReader, sql, parameters);
        //}

        //private static CInformation ExecReader(SqlDataReader dr)
        //{
        //    CInformation userinfo = new CInformation();
        //    userinfo.CompanyCode3 = Convert.ToInt32(dr["CompanyCode3"]);
        //    userinfo.ID = Convert.ToInt32(dr["ID"]);
        //    userinfo.CompanyCode2 = Convert.ToInt32(dr["CompanyCode2"]);
        //    userinfo.CompanyName = Convert.ToString(dr["CompanyName"]);
        //    userinfo.EnglishName = Convert.ToString(dr[".EnglishName"]);
        //    userinfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
        //    return userinfo;
        //}
    }
}
