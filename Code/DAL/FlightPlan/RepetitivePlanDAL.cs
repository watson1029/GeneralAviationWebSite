using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;
using Untity.DB;

namespace DAL.FlightPlan
{
    public class RepetitivePlanDAL
    {

        private static SqlDbHelper dao = new SqlDbHelper();


        public static bool Delete(string ids)
        {
            var sql = string.Format("delete from RepetitivePlan WHERE  (RepetPlanID IN ({0}))", ids);

            return dao.ExecNonQuery(sql) > 0;
        }

        public static bool Add(RepetitivePlan model)
        {
            var sql = @"insert into RepetitivePlan(FlightType,FlightDirHeight,PlanCode,AircraftType,StartDate,EndDate,CreateTime,Creator,ActorID,CompanyCode3,AttchFile,Remark,
PlanState,ADES,ADEP,WeekSchedule,SIBT,SOBT)
                                  values (@FlightType,@FlightDirHeight,@PlanCode,@AircraftType,@StartDate,@EndDate,@CreateTime,@Creator,@ActorID,@CompanyCode3,@AttchFile,@Remark,
@PlanState,@ADES,@ADEP,@WeekSchedule,@SIBT,@SOBT)";
            SqlParameter[] parameters = {
                            new SqlParameter("@FlightType",  model.FlightType),
                            new SqlParameter("@FlightDirHeight", model.FlightDirHeight),
                            new SqlParameter("@PlanCode", model.PlanCode),
                            new SqlParameter("@AircraftType", model.AircraftType),
                            new SqlParameter("@StartDate", model.StartDate),
                               new SqlParameter("@EndDate",  model.EndDate),
                            new SqlParameter("@CreateTime", model.CreateTime),
                            new SqlParameter("@Creator", model.Creator),
                            new SqlParameter("@ActorID", model.ActorID),
                            new SqlParameter("@CompanyCode3", model.CompanyCode3),
                            new SqlParameter("@AttchFile", model.AttchFile),
                            new SqlParameter("@Remark", model.Remark),
                            new SqlParameter("@PlanState", model.PlanState),
                            new SqlParameter("@ADES", model.ADES),
                            new SqlParameter("@ADEP", model.ADEP),
                            new SqlParameter("@WeekSchedule", model.WeekSchedule),
                            new SqlParameter("@SIBT", model.SIBT),
                            new SqlParameter("@SOBT", model.SOBT)
                                        };
            return dao.ExecNonQuery(sql, parameters) > 0;

        }
        public static bool Update(RepetitivePlan model)
        {
            var sql = @"update RepetitivePlan set FlightType=@FlightType,FlightDirHeight=@FlightDirHeight,PlanCode=@PlanCode,AircraftType=@AircraftType,StartDate=@StartDate,
 ,EndDate=@EndDate,ActorID=@ActorID,CompanyCode3=@CompanyCode3,AttchFile=@AttchFile,Remark=@Remark,
PlanState=@PlanState,ADES=@ADES,ADEP=@ADEP,WeekSchedule=@WeekSchedule,SIBT=@SIBT,SOBT=@SOBT where RepetPlanID=@ID";
            SqlParameter[] parameters = {
                             new SqlParameter("@FlightType",  model.FlightType),
                            new SqlParameter("@FlightDirHeight", model.FlightDirHeight),
                            new SqlParameter("@PlanCode", model.PlanCode),
                            new SqlParameter("@AircraftType", model.AircraftType),
                            new SqlParameter("@StartDate", model.StartDate),
                               new SqlParameter("@EndDate",  model.EndDate),
                            new SqlParameter("@CreateTime", model.CreateTime),
                            new SqlParameter("@Creator", model.Creator),
                            new SqlParameter("@ActorID", model.ActorID),
                            new SqlParameter("@CompanyCode3", model.CompanyCode3),
                            new SqlParameter("@AttchFile", model.AttchFile),
                            new SqlParameter("@Remark", model.Remark),
                            new SqlParameter("@PlanState", model.PlanState),
                            new SqlParameter("@ADES", model.ADES),
                            new SqlParameter("@ADEP", model.ADEP),
                            new SqlParameter("@WeekSchedule", model.WeekSchedule),
                            new SqlParameter("@SIBT", model.SIBT),
                            new SqlParameter("@SOBT", model.SOBT),
                              new SqlParameter("@ID", model.RepetPlanID)};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }


        public static PagedList<RepetitivePlan> GetMyRepetitivePlanList(int pageSize, int pageIndex, string strWhere)
        {

            var sql = string.Format("select * from RepetitivePlan where {0}", strWhere);
            return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<RepetitivePlan>()).ToPagedList<RepetitivePlan>(pageIndex, pageSize);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static RepetitivePlan Get(int id)
        {
            var sql = "select  top 1 * from RepetitivePlan where RepetPlanID=@PlanID";
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID",id)
			};
            return dao.ExecSelectSingleCmd<RepetitivePlan>(ExecReader, sql, parameters);
        }

        private static RepetitivePlan ExecReader(SqlDataReader dr)
        {
            RepetitivePlan plan = new RepetitivePlan();
            plan.FlightType = Convert.ToString(dr["FlightType"]);
            plan.FlightDirHeight = Convert.ToString(dr["FlightDirHeight"]);
            plan.AircraftType = Convert.ToString(dr["AircraftType"]);
            plan.PlanCode = Convert.ToString(dr["PlanCode"]);
            plan.RepetPlanID = Convert.ToInt32(dr["RepetPlanID"]);
            plan.StartDate = Convert.ToDateTime(dr["StartDate"]);
            plan.EndDate = Convert.ToDateTime(dr["EndDate"]);
            plan.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            plan.Creator = Convert.ToInt32(dr["Creator"]);
            plan.RepetPlanID = Convert.ToInt32(dr["RepetPlanID"]);
            if (!dr["ActorID"].Equals(DBNull.Value))
                plan.ActorID = Convert.ToInt32(dr["ActorID"]);
            plan.CompanyCode3 = Convert.ToString(dr["CompanyCode3"]);
            if (!dr["AttchFile"].Equals(DBNull.Value))
                plan.AttchFile = Convert.ToString(dr["AttchFile"]);
            if (!dr["Remark"].Equals(DBNull.Value))
                plan.Remark = Convert.ToString(dr["Remark"]);
            if (!dr["PlanState"].Equals(DBNull.Value))
                plan.PlanState = Convert.ToString(dr["PlanState"]);
            plan.ADES = Convert.ToString(dr["ADES"]);
            plan.ADEP = Convert.ToString(dr["ADEP"]);
            plan.WeekSchedule = Convert.ToString(dr["WeekSchedule"]);
            plan.SIBT = DateTime.ParseExact(dr["SIBT"].ToString(),"HH:mm:ss",null);
            plan.SOBT = DateTime.ParseExact(dr["SOBT"].ToString(), "HH:mm:ss", null);
            return plan;
        }
    }
}
