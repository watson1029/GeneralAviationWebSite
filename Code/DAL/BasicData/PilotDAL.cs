using System;
using System.Data;
using System.Data.SqlClient;
using Untity.DB;
using Untity;
using Model.BasicData;

namespace DAL.BasicData
{

    public class PilotDAL
    {

        //private static SqlDbHelper dao = new SqlDbHelper();


        //public static bool Delete(string ids)
        //{
        //    var sql = string.Format("delete from Pilot WHERE  (ID IN ({0}))", ids);

        //    return dao.ExecNonQuery(sql) > 0;
        //}
        //public static bool Add(Pilot model)
        //{
        //    var sql = @"insert into UserInfo(Pilots,DataTime,Password,Company,Sex,CreateTime)
        //                  values (@Pilots,@DataTime,@Password,@Company,@Sex,@CreateTime)";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Pilots",  model.Pilots),
        //            new SqlParameter("@DataTime", model.DataTime),
        //            new SqlParameter("@Password", model.Password),
        //            new SqlParameter("@Company", model.Company),
        //            new SqlParameter("@Sex", model.Sex),
        //            new SqlParameter("@CreateTime", model.CreateTime),};
        //    return dao.ExecNonQuery(sql, parameters) > 0;

        //}
        //public static bool Update(Pilot model)
        //{
        //    var sql = @"update UserInfo set Pilots=@Pilots,DataTime=@DataTime,Password=@Password,Company=@Company,Sex=@Sex where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Pilots",model.Pilots),
        //            new SqlParameter("@DataTime",  model.DataTime),
        //            new SqlParameter("@Password",model.Password),
        //            new SqlParameter("@Company", model.Company),
        //            new SqlParameter("@Sex", model.Sex),
        //            new SqlParameter("@ID", model.ID)};
        //    return dao.ExecNonQuery(sql, parameters) > 0;

        //}


        //public static PagedList<Pilot> GetList(int pageSize, int pageIndex, string strWhere)
        //{
        //    var sql = string.Format("select * from Pilot where {0}", strWhere);
        //    return dao.ExecSelectCmd(ExecReader, sql).ToPagedList<Pilot>(pageIndex, pageSize);

        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public static Pilot Get(int id)
        //{
        //    var sql = "select  top 1 * from Pilot where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = id;
        //    return dao.ExecSelectSingleCmd<Pilot>(ExecReader, sql, parameters);
        //}

        //private static Pilot ExecReader(SqlDataReader dr)
        //{
        //    Pilot pilot = new Pilot();
        //    pilot.Pilots = Convert.ToString(dr["Pilots"]);
        //    pilot.ID = Convert.ToInt32(dr["ID"]);
        //    pilot.DataTime = Convert.ToDateTime(dr["DataTime"]);
        //    pilot.Password = Convert.ToString(dr["Password"]);
        //    pilot.Company = Convert.ToByte(dr["Company"]);
        //    pilot.Sex = Convert.ToByte(dr["Sex"]);
        //    pilot.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
        //    return pilot;
        //}
    }
}
