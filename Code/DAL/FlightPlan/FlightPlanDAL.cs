using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Untity;
using Untity.DB;

namespace DAL.FlightPlan
{
    public class FlightPlanDAL
    {
        private ZHCC_GAPlanEntities db = new ZHCC_GAPlanEntities();

        //使用EF     
        #region

        /// <summary>
        /// 【新增】:字段验证在BLL中进行
        /// </summary>
        /// <returns>true or false</returns>
        public bool Add(Model.EF.FlightPlan plan)
        {
            db.FlightPlan.Add(plan);
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 【删除】
        /// </summary>
        /// <param name="flightPlanID">主键值</param>
        /// <returns>true or false</returns>
        public bool Delete(int? flightPlanID)
        {
            if (flightPlanID == null) return false;

            var plan = db.FlightPlan.Find(flightPlanID);
            if (plan != null)
            {
                db.FlightPlan.Remove(plan);
                return db.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 【删除多条记录】
        /// </summary>
        /// <param name="flightPlanIDs">以英文逗号隔开的多个FlightPlanID</param>
        /// <returns></returns>
        public bool BatchDelete(string flightPlanIDs)
        {
            if (string.IsNullOrEmpty(flightPlanIDs)) return false;

            string[] ids = flightPlanIDs.Split(',');
            int id = -1;
            Model.EF.FlightPlan plan;
            foreach (var item in ids)
            {
                id = int.Parse(item);
                plan = db.FlightPlan.Find(id);
                db.FlightPlan.Remove(plan);
            }
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 【修改】
        /// </summary>
        /// <returns>true or false</returns>
        public bool Update(Model.EF.FlightPlan plan)
        {
            var FlightPlan = db.FlightPlan.Find(plan.FlightPlanID);
            if (FlightPlan != null)
            {
                FlightPlan.PlanCode = plan.PlanCode;
                FlightPlan.FlightType = plan.FlightType;
                FlightPlan.AircraftType = plan.AircraftType;
                FlightPlan.FlightDirHeight = plan.FlightDirHeight;
                FlightPlan.StartDate = plan.StartDate;
                FlightPlan.EndDate = plan.EndDate;
                FlightPlan.ModifyTime = DateTime.Now;
                FlightPlan.CompanyCode3 = plan.CompanyCode3;
                FlightPlan.AttchFile = plan.AttchFile;
                FlightPlan.PlanState = plan.PlanState;
                FlightPlan.ActorID = plan.ActorID;
                FlightPlan.Remark = plan.Remark;
                FlightPlan.SOBT = plan.SOBT;
                FlightPlan.SIBT = plan.SIBT;
                FlightPlan.WeekSchedule = plan.WeekSchedule;
                FlightPlan.ADEP = plan.ADEP;
                FlightPlan.ADES = plan.ADES;
                FlightPlan.CallSign = plan.CallSign;

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
        /// <param name="flightPlanID">主键值</param>
        /// <returns>Model.EF.FlightPlan类型对象</returns>
        public Model.EF.FlightPlan Get(int? flightPlanID)
        {
            if (flightPlanID == null) return null;

            return db.FlightPlan.Find(flightPlanID);
        }

        /// <summary>
        /// 【查询】：获取全部记录，按照指定方式排序
        /// 调用方式:
        /// FlightPlanDAL dal = new FlightPlanDAL();
        /// List<Model.EF.FlightPlan> result=dal.GetAllListOrderBy(m => m.FlightPlanID, true);
        /// 按照主键升序排列，如需按照其它列进行排序，指定【m => m.FlightPlanID】为所需列名即可
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.FlightPlan></returns>
        public List<Model.EF.FlightPlan> GetAllListOrderBy<TKey>(Expression<Func<Model.EF.FlightPlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.FlightPlan.OrderBy(orderBy).ToList();
            else
                return db.FlightPlan.OrderByDescending(orderBy).ToList();
        }

        /// <summary>
        /// 【查询】：按查询条件、排序方式获取记录
        /// 调用方式:
        /// FlightPlanDAL dal = new FlightPlanDAL();
        /// List<Model.EF.FlightPlan> result=dal.GetAllListWhereAndOrderBy(m => m.FlightPlanID>2, m=>m.FlightPlanID, true);
        /// 查询条件为FlightPlanID>2，如需其它查询条件，通过修改【m => m.FlightPlanID>2】即可
        /// 排序条件为FlightPlanID,如需按照其它列进行排序，指定【m => m.FlightPlanID】为所需列名即可
        /// 按照主键升序排列
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="planWhere">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.FlightPlan></returns>
        public List<Model.EF.FlightPlan> GetAllListWhereAndOrderBy<TKey>(Expression<Func<Model.EF.FlightPlan, bool>> planWhere,
            Expression<Func<Model.EF.FlightPlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.FlightPlan.Where(planWhere).OrderBy(orderBy).ToList();
            else
                return db.FlightPlan.Where(planWhere).OrderByDescending(orderBy).ToList();
        }

        /// <summary>
        /// 【查询】：按分页，排序方式获取记录
        /// 调用方式:
        /// FlightPlanDAL dal = new FlightPlanDAL();
        /// List<Model.EF.FlightPlan> result=dal.GetPageListOrderBy(1,10, m=>m.FlightPlanID, true);
        /// 排序条件为FlightPlanID,如需按照其它列进行排序，指定【m => m.FlightPlanID】为所需列名即可
        /// 按照主键升序排列
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <typeparam name="PageIndex">页码，从1开始</typeparam>
        /// <typeparam name="PageSize">每页显示的记录数</typeparam>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.FlightPlan></returns>
        public List<Model.EF.FlightPlan> GetPageListOrderBy<TKey>(int PageIndex, int PageSize,
            Expression<Func<Model.EF.FlightPlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.FlightPlan.OrderBy(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            else
                return db.FlightPlan.OrderByDescending(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 【查询】：按分页，查询条件、排序方式获取记录
        /// 调用方式:
        /// FlightPlanDAL dal = new FlightPlanDAL();
        /// List<Model.EF.FlightPlan> result=dal.GetPageListWhereAndOrderBy(1,10,m => m.FlightPlanID>2, m=>m.FlightPlanID, true);
        /// 查询条件为FlightPlanID>2，如需其它查询条件，通过修改【m => m.FlightPlanID>2】即可
        /// 排序条件为FlightPlanID,如需按照其它列进行排序，指定【m => m.FlightPlanID】为所需列名即可
        /// 按照主键升序排列
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <typeparam name="PageIndex">页码，从1开始</typeparam>
        /// <typeparam name="PageSize">每页显示的记录数</typeparam>
        /// <param name="planWhere">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.FlightPlan></returns>
        public List<Model.EF.FlightPlan> GetPageListWhereAndOrderBy<TKey>(int PageIndex, int PageSize,
            Expression<Func<Model.EF.FlightPlan, bool>> planWhere,
            Expression<Func<Model.EF.FlightPlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.FlightPlan.Where(planWhere).OrderBy(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            else
                return db.FlightPlan.Where(planWhere).OrderByDescending(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        #endregion

        //未使用EF
        #region 
        //        public static bool Delete(string ids)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = string.Format("delete from FlightPlan WHERE  (FlightPlanID IN ({0}))", ids);

        //            return dao.ExecNonQuery(sql) > 0;
        //        }

        //        public static bool Add(Model.EF.FlightPlan model)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = @"insert into FlightPlan(FlightType,FlightDirHeight,PlanCode,AircraftType,StartDate,EndDate,CreateTime,ModifyTime,Creator,CreatorName,ActorID,CompanyCode3,AttchFile,Remark,
        //PlanState,ADES,ADEP,WeekSchedule,SIBT,SOBT,CallSign,AircrewGroupNum,WeatherCondition,RadarCode,Pilot,AircraftNum,ContactWay)
        //                                  values (@FlightType,@FlightDirHeight,@PlanCode,@AircraftType,@StartDate,@EndDate,@CreateTime,@ModifyTime,@Creator,@CreatorName,@ActorID,@CompanyCode3,@AttchFile,@Remark,
        //@PlanState,@ADES,@ADEP,@WeekSchedule,@SIBT,@SOBT,@CallSign,@AircrewGroupNum,@WeatherCondition,@RadarCode,@Pilot,@AircraftNum,@ContactWay)";
        //            SqlParameter[] parameters = {
        //                            new SqlParameter("@FlightType",  model.FlightType),
        //                            new SqlParameter("@FlightDirHeight", model.FlightDirHeight),
        //                            new SqlParameter("@PlanCode", model.PlanCode),
        //                            new SqlParameter("@AircraftType", model.AircraftType),
        //                            new SqlParameter("@StartDate", model.StartDate),
        //                               new SqlParameter("@EndDate",  model.EndDate),
        //                            new SqlParameter("@CreateTime", model.CreateTime),
        //                            new SqlParameter("@ModifyTime", model.ModifyTime),
        //                            new SqlParameter("@Creator", model.Creator),
        //                             new SqlParameter("@CreatorName", model.CreatorName),
        //                            new SqlParameter("@ActorID", model.ActorID),
        //                            new SqlParameter("@CompanyCode3", model.CompanyCode3),
        //                            new SqlParameter("@AttchFile", model.AttchFile),
        //                            new SqlParameter("@Remark", model.Remark),
        //                            new SqlParameter("@PlanState", model.PlanState),
        //                            new SqlParameter("@ADES", model.ADES),
        //                            new SqlParameter("@ADEP", model.ADEP),
        //                            new SqlParameter("@WeekSchedule", model.WeekSchedule),
        //                            new SqlParameter("@SIBT", model.SIBT),
        //                            new SqlParameter("@SOBT", model.SOBT),
        //                            new SqlParameter("@CallSign", model.CallSign),
        //                             new SqlParameter("@AircrewGroupNum", model.AircrewGroupNum),
        //                              new SqlParameter("@WeatherCondition", model.WeatherCondition),
        //                               new SqlParameter("@RadarCode", model.RadarCode),
        //                               new SqlParameter("@Pilot", model.Pilot),
        //                               new SqlParameter("@AircraftNum", model.AircraftNum),
        //                               new SqlParameter("@ContactWay", model.ContactWay)
        //                                        };
        //            return dao.ExecNonQuery(sql, parameters) > 0;

        //        }
        //        public static bool Update(Model.EF.FlightPlan model)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = @"update RepetitivePlan set FlightType=@FlightType,FlightDirHeight=@FlightDirHeight,ModifyTime=@ModifyTime,PlanCode=@PlanCode,AircraftType=@AircraftType,StartDate=@StartDate,
        //,EndDate=@EndDate,AttchFile=@AttchFile,Remark=@Remark,ADES=@ADES,ADEP=@ADEP,WeekSchedule=@WeekSchedule,SIBT=@SIBT,SOBT=@SOBT,CallSign=@CallSign,CallSign=@CallSign,WeatherCondition=@WeatherCondition
        //,RadarCode=@RadarCode,Pilot=@Pilot,AircraftNum=@AircraftNum,ContactWay=@ContactWay where RepetPlanID=@ID";
        //            SqlParameter[] parameters = {
        //                             new SqlParameter("@FlightType",  model.FlightType),
        //                            new SqlParameter("@FlightDirHeight", model.FlightDirHeight),
        //                            new SqlParameter("@PlanCode", model.PlanCode),
        //                            new SqlParameter("@AircraftType", model.AircraftType),
        //                            new SqlParameter("@StartDate", model.StartDate),
        //                               new SqlParameter("@EndDate",  model.EndDate),
        //                            new SqlParameter("@ModifyTime", model.ModifyTime),
        //                            new SqlParameter("@AttchFile", model.AttchFile),
        //                            new SqlParameter("@Remark", model.Remark),
        //                            new SqlParameter("@ADES", model.ADES),
        //                            new SqlParameter("@ADEP", model.ADEP),
        //                            new SqlParameter("@WeekSchedule", model.WeekSchedule),
        //                            new SqlParameter("@SIBT", model.SIBT),
        //                            new SqlParameter("@SOBT", model.SOBT),
        //                              new SqlParameter("@ID", model.RepetPlanID),
        //                                                   new SqlParameter("@CallSign", model.CallSign),
        //                             new SqlParameter("@AircrewGroupNum", model.AircrewGroupNum),
        //                              new SqlParameter("@WeatherCondition", model.WeatherCondition),
        //                               new SqlParameter("@RadarCode", model.RadarCode),
        //                               new SqlParameter("@Pilot", model.Pilot),
        //                               new SqlParameter("@AircraftNum", model.AircraftNum),
        //                               new SqlParameter("@ContactWay", model.ContactWay)};
        //            return dao.ExecNonQuery(sql, parameters) > 0;

        //        }


        //        public static PagedList<Model.EF.FlightPlan> GetMyFlightPlanList(int pageSize, int pageIndex, string strWhere)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = string.Format("select * from FlightPlan where {0}", strWhere);
        //            return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<Model.EF.FlightPlan>()).ToPagedList<Model.EF.FlightPlan>(pageIndex, pageSize);
        //        }


        //        /// <summary>
        //        /// 得到一个对象实体
        //        /// </summary>
        //        public static Model.EF.FlightPlan Get(int id)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = "select  top 1 * from FlightPlan where FlightPlanID=@PlanID";
        //            SqlParameter[] parameters = {
        //					new SqlParameter("@PlanID",id)
        //			};
        //            return dao.ExecSelectSingleCmd<Model.EF.FlightPlan>(ExecReader, sql, parameters);
        //        }

        //        private static vFlightPlan ExecReader(SqlDataReader dr)
        //        {
        //            vFlightPlan plan = new vFlightPlan();
        //            plan.FlightType = Convert.ToString(dr["FlightType"]);
        //            plan.FlightDirHeight = Convert.ToString(dr["FlightDirHeight"]);
        //            plan.AircraftType = Convert.ToString(dr["AircraftType"]);
        //            plan.PlanCode = Convert.ToString(dr["PlanCode"]);
        //            plan.RepetPlanID = Convert.ToInt32(dr["RepetPlanID"]);
        //            plan.StartDate = Convert.ToDateTime(dr["StartDate"]);
        //            plan.EndDate = Convert.ToDateTime(dr["EndDate"]);
        //            plan.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
        //            plan.Creator = Convert.ToInt32(dr["Creator"]);
        //            plan.CreatorName = Convert.ToString(dr["CreatorName"]);
        //            plan.CallSign = Convert.ToString(dr["CallSign"]);
        //            plan.RepetPlanID = Convert.ToInt32(dr["RepetPlanID"]);
        //            if (!dr["ActorID"].Equals(DBNull.Value))
        //                plan.ActorID = Convert.ToInt32(dr["ActorID"]);
        //            plan.CompanyCode3 = Convert.ToString(dr["CompanyCode3"]);
        //            if (!dr["AttchFile"].Equals(DBNull.Value))
        //                plan.AttchFile = Convert.ToString(dr["AttchFile"]);
        //            if (!dr["Remark"].Equals(DBNull.Value))
        //                plan.Remark = Convert.ToString(dr["Remark"]);
        //            if (!dr["PlanState"].Equals(DBNull.Value))
        //                plan.PlanState = Convert.ToString(dr["PlanState"]);
        //            plan.ADES = Convert.ToString(dr["ADES"]);
        //            plan.ADEP = Convert.ToString(dr["ADEP"]);
        //            plan.WeekSchedule = Convert.ToString(dr["WeekSchedule"]).Replace("*", "");
        //            plan.SIBT = DateTime.ParseExact(dr["SIBT"].ToString(), "HH:mm:ss", null);
        //            plan.SOBT = DateTime.ParseExact(dr["SOBT"].ToString(), "HH:mm:ss", null);
        //             plan.AircrewGroupNum = Convert.ToInt32(dr["AircrewGroupNum"]);
        //             plan.WeatherCondition = Convert.ToString(dr["WeatherCondition"]);
        //            plan.RadarCode = Convert.ToString(dr["RadarCode"]);
        //            plan.Pilot = Convert.ToString(dr["Pilot"]);  
        //            plan.AircraftNum = Convert.ToInt32(dr["AircraftNum"]);
        //            plan.ContactWay = Convert.ToString(dr["ContactWay"]);
        //            if (!dr["ActualStartTime"].Equals(DBNull.Value))
        //                plan.ActualStartTime = Convert.ToDateTime(dr["ActualStartTime"]);
        //            if (!dr["ActualEndTime"].Equals(DBNull.Value))
        //                plan.ActualEndTime = Convert.ToDateTime(dr["ActualEndTime"]);
        //            return plan;
        //        }
        #endregion
    }
}
