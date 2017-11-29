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
    public class FlightPlanDAL
    {
        public static bool Delete(string ids)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = string.Format("delete from FlightPlan WHERE  (FlightPlanID IN ({0}))", ids);

            return dao.ExecNonQuery(sql) > 0;
        }

        public static bool Add(vFlightPlan model)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = @"insert into FlightPlan(FlightType,FlightDirHeight,PlanCode,AircraftType,StartDate,EndDate,CreateTime,ModifyTime,Creator,CreatorName,ActorID,CompanyCode3,AttchFile,Remark,
PlanState,ADES,ADEP,WeekSchedule,SIBT,SOBT,CallSign,AircrewGroupNum,WeatherCondition,RadarCode,Pilot,AircraftNum,ContactWay)
                                  values (@FlightType,@FlightDirHeight,@PlanCode,@AircraftType,@StartDate,@EndDate,@CreateTime,@ModifyTime,@Creator,@CreatorName,@ActorID,@CompanyCode3,@AttchFile,@Remark,
@PlanState,@ADES,@ADEP,@WeekSchedule,@SIBT,@SOBT,@CallSign,@AircrewGroupNum,@WeatherCondition,@RadarCode,@Pilot,@AircraftNum,@ContactWay)";
            SqlParameter[] parameters = {
                            new SqlParameter("@FlightType",  model.FlightType),
                            new SqlParameter("@FlightDirHeight", model.FlightDirHeight),
                            new SqlParameter("@PlanCode", model.PlanCode),
                            new SqlParameter("@AircraftType", model.AircraftType),
                            new SqlParameter("@StartDate", model.StartDate),
                               new SqlParameter("@EndDate",  model.EndDate),
                            new SqlParameter("@CreateTime", model.CreateTime),
                            new SqlParameter("@ModifyTime", model.ModifyTime),
                            new SqlParameter("@Creator", model.Creator),
                             new SqlParameter("@CreatorName", model.CreatorName),
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
                            new SqlParameter("@CallSign", model.CallSign),
                             new SqlParameter("@AircrewGroupNum", model.AircrewGroupNum),
                              new SqlParameter("@WeatherCondition", model.WeatherCondition),
                               new SqlParameter("@RadarCode", model.RadarCode),
                               new SqlParameter("@Pilot", model.Pilot),
                               new SqlParameter("@AircraftNum", model.AircraftNum),
                               new SqlParameter("@ContactWay", model.ContactWay)
                                        };
            return dao.ExecNonQuery(sql, parameters) > 0;

        }
        public static bool Update(vFlightPlan model)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = @"update RepetitivePlan set FlightType=@FlightType,FlightDirHeight=@FlightDirHeight,ModifyTime=@ModifyTime,PlanCode=@PlanCode,AircraftType=@AircraftType,StartDate=@StartDate,
,EndDate=@EndDate,AttchFile=@AttchFile,Remark=@Remark,ADES=@ADES,ADEP=@ADEP,WeekSchedule=@WeekSchedule,SIBT=@SIBT,SOBT=@SOBT,CallSign=@CallSign,CallSign=@CallSign,WeatherCondition=@WeatherCondition
,RadarCode=@RadarCode,Pilot=@Pilot,AircraftNum=@AircraftNum,ContactWay=@ContactWay where RepetPlanID=@ID";
            SqlParameter[] parameters = {
                             new SqlParameter("@FlightType",  model.FlightType),
                            new SqlParameter("@FlightDirHeight", model.FlightDirHeight),
                            new SqlParameter("@PlanCode", model.PlanCode),
                            new SqlParameter("@AircraftType", model.AircraftType),
                            new SqlParameter("@StartDate", model.StartDate),
                               new SqlParameter("@EndDate",  model.EndDate),
                            new SqlParameter("@ModifyTime", model.ModifyTime),
                            new SqlParameter("@AttchFile", model.AttchFile),
                            new SqlParameter("@Remark", model.Remark),
                            new SqlParameter("@ADES", model.ADES),
                            new SqlParameter("@ADEP", model.ADEP),
                            new SqlParameter("@WeekSchedule", model.WeekSchedule),
                            new SqlParameter("@SIBT", model.SIBT),
                            new SqlParameter("@SOBT", model.SOBT),
                              new SqlParameter("@ID", model.RepetPlanID),
                                                   new SqlParameter("@CallSign", model.CallSign),
                             new SqlParameter("@AircrewGroupNum", model.AircrewGroupNum),
                              new SqlParameter("@WeatherCondition", model.WeatherCondition),
                               new SqlParameter("@RadarCode", model.RadarCode),
                               new SqlParameter("@Pilot", model.Pilot),
                               new SqlParameter("@AircraftNum", model.AircraftNum),
                               new SqlParameter("@ContactWay", model.ContactWay)};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }


        public static PagedList<vFlightPlan> GetMyFlightPlanList(int pageSize, int pageIndex, string strWhere)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = string.Format("select * from FlightPlan where {0}", strWhere);
            return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<vFlightPlan>()).ToPagedList<vFlightPlan>(pageIndex, pageSize);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static vFlightPlan Get(int id)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = "select  top 1 * from FlightPlan where FlightPlanID=@PlanID";
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID",id)
			};
            return dao.ExecSelectSingleCmd<vFlightPlan>(ExecReader, sql, parameters);
        }

        private static vFlightPlan ExecReader(SqlDataReader dr)
        {
            vFlightPlan plan = new vFlightPlan();
            plan.FlightType = Convert.ToString(dr["FlightType"]);
            plan.FlightDirHeight = Convert.ToString(dr["FlightDirHeight"]);
            plan.AircraftType = Convert.ToString(dr["AircraftType"]);
            plan.PlanCode = Convert.ToString(dr["PlanCode"]);
            plan.RepetPlanID = Convert.ToInt32(dr["RepetPlanID"]);
            plan.StartDate = Convert.ToDateTime(dr["StartDate"]);
            plan.EndDate = Convert.ToDateTime(dr["EndDate"]);
            plan.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            plan.Creator = Convert.ToInt32(dr["Creator"]);
            plan.CreatorName = Convert.ToString(dr["CreatorName"]);
            plan.CallSign = Convert.ToString(dr["CallSign"]);
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
            plan.WeekSchedule = Convert.ToString(dr["WeekSchedule"]).Replace("*", "");
            plan.SIBT = DateTime.ParseExact(dr["SIBT"].ToString(), "HH:mm:ss", null);
            plan.SOBT = DateTime.ParseExact(dr["SOBT"].ToString(), "HH:mm:ss", null);
             plan.AircrewGroupNum = Convert.ToInt32(dr["AircrewGroupNum"]);
             plan.WeatherCondition = Convert.ToString(dr["WeatherCondition"]);
            plan.RadarCode = Convert.ToString(dr["RadarCode"]);
            plan.Pilot = Convert.ToString(dr["Pilot"]);  
            plan.AircraftNum = Convert.ToInt32(dr["AircraftNum"]);
            plan.ContactWay = Convert.ToString(dr["ContactWay"]);
            if (!dr["ActualStartTime"].Equals(DBNull.Value))
                plan.ActualStartTime = Convert.ToDateTime(dr["ActualStartTime"]);
            if (!dr["ActualEndTime"].Equals(DBNull.Value))
                plan.ActualEndTime = Convert.ToDateTime(dr["ActualEndTime"]);
            return plan;
        }
    }
}
