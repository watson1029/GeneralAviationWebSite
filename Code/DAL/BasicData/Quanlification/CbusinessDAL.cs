
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

    public class CBusinessDAL:DBHelper<CBusiness>
    {

        //private static SqlDbHelper dao = new SqlDbHelper();


        //public static bool Delete(string ids)
        //{
        //    var sql = string.Format("delete from CBusiness WHERE  (ID IN ({0}))", ids);

        //    return dao.ExecNonQuery(sql) > 0;
        //}
        //public static bool Add(CBusiness model)
        //{
        //    var sql = @"insert into CBusiness(CompanyCode3,LegalName,JoinData,LegalCard,JoinAddress,LegalAddress,Capital,LegalPhone,EffectiveData,LegalConsignor,
        //                                      Contacts,ConsignorAddress,ConsignorName,ConsignorCard,ConsignorPhone,CreateTime)
        //                  values (@CompanyCode3,@LegalName,@JoinData,@LegalCard,@JoinAddress,@LegalAddress,@Capital,@LegalPhone,@EffectiveData,@LegalConsignor,
        //                          @Contacts,@ConsignorAddress,@ConsignorName,@ConsignorCard,@ConsignorPhone,@CreateTime)";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@CompanyCode3",  model.CompanyCode3),
        //            new SqlParameter("@LegalName", model.LegalName),
        //            new SqlParameter("@JoinData", model.JoinData),
        //            new SqlParameter("@LegalCard", model.LegalCard),
        //            new SqlParameter("@JoinAddress", model.JoinAddress),
        //            new SqlParameter("@LegalAddress",  model.LegalAddress),
        //            new SqlParameter("@Capital", model.Capital),
        //            new SqlParameter("@LegalPhone", model.LegalPhone),
        //            new SqlParameter("@EffectiveData", model.EffectiveData),
        //            new SqlParameter("@LegalConsignor",  model.LegalConsignor),
        //            new SqlParameter("@Contacts", model.Contacts),
        //            new SqlParameter("@ConsignorAddress", model.ConsignorAddress),
        //            new SqlParameter("@ConsignorName", model.ConsignorName),
        //            new SqlParameter("@ConsignorCard", model.ConsignorCard),
        //            new SqlParameter("@ConsignorPhone", model.ConsignorPhone),
        //            new SqlParameter("@CreateTime", model.CreateTime),
        //    };
        //    return dao.ExecNonQuery(sql, parameters) > 0;

        //}
        //public static bool Update(CBusiness model)
        //{
        //    var sql = @"update CBusiness set CompanyCode3=@CompanyCode3,LegalName=@LegalName,JoinData=@JoinData,LegalCard=@LegalCard,JoinAddress=@JoinAddress
        //                LegalAddress=@LegalAddress Capital=@Capital LegalPhone=@LegalPhone EffectiveData=@EffectiveData LegalConsignor=@LegalConsignor
        //                Contacts=@Contacts ConsignorAddress=@ConsignorAddress ConsignorName=@ConsignorName ConsignorCard=@ConsignorCard
        //                ConsignorPhone=@ConsignorPhone where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@CompanyCode3",model.CompanyCode3),
        //            new SqlParameter("@LegalName",  model.LegalName),
        //            new SqlParameter("@JoinData",model.JoinData),
        //            new SqlParameter("@LegalCard", model.LegalCard),
        //            new SqlParameter("@JoinAddress", model.JoinAddress),
        //            new SqlParameter("@LegalAddress",model.LegalAddress),
        //            new SqlParameter("@Capital",  model.Capital),
        //            new SqlParameter("@LegalPhone",model.LegalPhone),
        //            new SqlParameter("@EffectiveData", model.EffectiveData),
        //            new SqlParameter("@LegalConsignor", model.LegalConsignor),
        //            new SqlParameter("@Contacts",  model.Contacts),
        //            new SqlParameter("@ConsignorAddress",model.ConsignorAddress),
        //            new SqlParameter("@ConsignorName", model.ConsignorName),
        //            new SqlParameter("@ConsignorCard", model.ConsignorCard),
        //            new SqlParameter("@ConsignorPhone",model.ConsignorPhone),
        //            new SqlParameter("@ID", model.ID)};
        //    return dao.ExecNonQuery(sql, parameters) > 0;

        //}


        //public static PagedList<CBusiness> GetList(int pageSize, int pageIndex, string strWhere)
        //{
        //    var sql = string.Format("select * from CBusiness where {0}", strWhere);
        //    return dao.ExecSelectCmd(ExecReader, sql).ToPagedList<CBusiness>(pageIndex, pageSize);

        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public static CBusiness Get(int id)
        //{
        //    var sql = "select  top 1 * from CBusiness where ID=@ID";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = id;
        //    return dao.ExecSelectSingleCmd<CBusiness>(ExecReader, sql, parameters);
        //}

        //private static CBusiness ExecReader(SqlDataReader dr)
        //{
        //    CBusiness  userinfo = new CBusiness ();
        //    userinfo.ID = Convert.ToInt32(dr["ID"]);
        //    userinfo.CompanyCode3 = Convert.ToInt32(dr["CompanyCode3"]);
        //    userinfo.LegalName = Convert.ToString(dr["LegalName"]);
        //    userinfo.JoinData = Convert.ToDateTime(dr["JoinData"]);
        //    userinfo.LegalCard = Convert.ToInt32(dr["LegalCard"]);
        //    userinfo.JoinAddress = Convert.ToString(dr["JoinAddress"]);
        //    userinfo.LegalAddress = Convert.ToString(dr["LegalAddress"]);
        //    userinfo.Capital = Convert.ToInt32(dr["Capital"]);
        //    userinfo.LegalPhone = Convert.ToInt32(dr["LegalPhone"]);
        //    userinfo.EffectiveData = Convert.ToDateTime(dr["EffectiveData"]);
        //    userinfo.LegalConsignor = Convert.ToString(dr["LegalConsignor"]);
        //    userinfo.Contacts = Convert.ToString(dr["Contacts"]);
        //    userinfo.ConsignorAddress = Convert.ToString(dr["ConsignorAddress"]);
        //    userinfo.ConsignorName = Convert.ToString(dr["ConsignorName"]);
        //    userinfo.ConsignorCard = Convert.ToInt32(dr["ConsignorCard"]);
        //    userinfo.ConsignorPhone = Convert.ToInt32(dr["ConsignorPhone"]);
        //    userinfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
        //    return userinfo;
        //}
    }
}
