using System;
using System.Data;
using System.Data.SqlClient;
using Untity.DB;
using Untity;
using Model.BasicData;

namespace DAL.BasicData
{

    public class CLicenceDAL
    {

        private static SqlDbHelper dao = new SqlDbHelper();


        public static bool Delete(string ids)
        {
            var sql = string.Format("delete from Clicence WHERE  (ID IN ({0}))", ids);

            return dao.ExecNonQuery(sql) > 0;
        }
        public static bool Add(CLicence model)
        {
            var sql = @"insert into CLicence(CompanyCode3, LegalPerson, Licence, Project, BaseAirport, EffectiveData, CompanyType, LssueData, Capital, Quota, CreateTime)
                          values (@CompanyCode3,@ LegalPerson,@Licence,@Project,@BaseAirport,@EffectiveData,@CompanyType,@LssueData,@Capital,@Quota,@CreateTime)";
            SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCode3",  model.CompanyCode3),
                    new SqlParameter("@ LegalPerson", model. LegalPerson),
                    new SqlParameter("@Licence", model.Licence),
                    new SqlParameter("@Project", model.Project),
                    new SqlParameter("@BaseAirport", model.BaseAirport),
                    new SqlParameter("@EffectiveData", model.EffectiveData),
                    new SqlParameter("@CompanyType", model.CompanyType),
                    new SqlParameter("@LssueData", model.LssueData),
                    new SqlParameter("@Capital", model.Capital),
                    new SqlParameter("@Quota", model.Quota),
                    new SqlParameter("@CreateTime", model.CreateTime),
            };
            return dao.ExecNonQuery(sql, parameters) > 0;

        }
        public static bool Update(CLicence model)
        {
            var sql = @"update CLicence set CompanyCode3=@CompanyCode3, LegalPerson=@ LegalPerson,Licence=@Licence,Project=@Project,BaseAirport=@BaseAirport 
                                            EffectiveData=@EffectiveData, CompanyType=@CompanyType, LssueData=@LssueData, Capital=@Capital,
                                            where ID=@ID";
            SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCode3",model.CompanyCode3),
                    new SqlParameter("@LegalPerson",model. LegalPerson),
                    new SqlParameter("@Licence",model.Licence),
                    new SqlParameter("@Project", model.Project),
                    new SqlParameter("@BaseAirport", model.BaseAirport),
                    new SqlParameter("@EffectiveData", model.EffectiveData),
                    new SqlParameter("@CompanyCode3", model.CompanyCode3),
                    new SqlParameter("@LssueData", model.LssueData),
                    new SqlParameter("@Capital", model.Capital),
                    new SqlParameter("@CreataTime", model.CreateTime),
                    new SqlParameter("@ID", model.ID)};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }


        public static PagedList<CLicence > GetList(int pageSize, int pageIndex, string strWhere)
        {
            var sql = string.Format("select * from CLicence where {0}", strWhere);
            return dao.ExecSelectCmd(ExecReader, sql).ToPagedList<CLicence>(pageIndex, pageSize);

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static CLicence Get(int id)
        {
            var sql = "select  top 1 * from CLicence where ID=@ID";
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            return dao.ExecSelectSingleCmd<CLicence>(ExecReader, sql, parameters);
        }

        private static CLicence  ExecReader(SqlDataReader dr)
        {
            CLicence  userinfo = new CLicence();
            userinfo.ID = Convert.ToInt32(dr["ID"]);
            userinfo.CompanyCode3 = Convert.ToInt32(dr["CompanyCode3"]);
            userinfo.LegalPerson = Convert.ToInt32(dr["LegalPerson"]);
            userinfo.Licence = Convert.ToInt32(dr["Licence"]);
            userinfo.Project = Convert.ToString(dr["Project"]);
            userinfo.BaseAirport = Convert.ToString(dr["BaseAirport"]);
            userinfo.EffectiveData = Convert.ToDateTime(dr["EffectiveData"]);
            userinfo.CompanyType = Convert.ToString(dr["CompanyType"]);
            userinfo.LssueData = Convert.ToDateTime(dr["LssueData"]);
            userinfo.Capital = Convert.ToInt32(dr["Capital"]);
            userinfo.Quota = Convert.ToInt32(dr["Quota"]);
            userinfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            return userinfo;
        }
    }
}
