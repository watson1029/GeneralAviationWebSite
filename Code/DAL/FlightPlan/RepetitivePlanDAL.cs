using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Untity;
using Untity.DB;
using Model.EF;
using System.Data.Entity;
using Model.FlightPlan;

namespace DAL.FlightPlan
{
    public class RepetitivePlanDAL
    {
        private ZHCC_GAPlanEntities db = new ZHCC_GAPlanEntities();

        //使用EF
        #region

        /// <summary>
        /// 【新增】:字段验证在BLL中进行
        /// </summary>
        /// <returns>true or false</returns>
        public bool Add(RepetitivePlan plan)
        {
            db.RepetitivePlan.Add(plan);
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 【删除】
        /// </summary>
        /// <param name="repetPlanID">主键值</param>
        /// <returns>true or false</returns>
        public bool Delete(int? repetPlanID)
        {
            if (repetPlanID == null) return false;

            var plan = db.RepetitivePlan.Find(repetPlanID);
            if (plan != null)
            {
                db.RepetitivePlan.Remove(plan);
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
        /// <param name="flightPlanIDs">以英文逗号隔开的多个RepetPlanID</param>
        /// <returns></returns>
        public bool BatchDelete(string repetPlanIDs)
        {
            if (string.IsNullOrEmpty(repetPlanIDs)) return false;

            string[] ids = repetPlanIDs.Split(',');
            int id = -1;
            Model.EF.RepetitivePlan plan;
            foreach (var item in ids)
            {
                id = int.Parse(item);
                plan = db.RepetitivePlan.Find(id);
                db.RepetitivePlan.Remove(plan);
            }
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 【修改】
        /// </summary>
        /// <param name="repetPlanID">主键值</param>
        /// <returns>true or false</returns>
        public bool Update(RepetitivePlan plan)
        {
            var repetitivePlan = db.RepetitivePlan.Find(plan.RepetPlanID);
            if (repetitivePlan != null)
            {
                repetitivePlan.PlanCode = plan.PlanCode;
                repetitivePlan.FlightType = plan.FlightType;
                repetitivePlan.AircraftType = plan.AircraftType;
                repetitivePlan.FlightDirHeight = plan.FlightDirHeight;
                repetitivePlan.StartDate = plan.StartDate;
                repetitivePlan.EndDate = plan.EndDate;
                repetitivePlan.ModifyTime = DateTime.Now;
                repetitivePlan.CompanyCode3 = plan.CompanyCode3;
                repetitivePlan.AttchFile = plan.AttchFile;
                repetitivePlan.PlanState = plan.PlanState;
                repetitivePlan.ActorID = plan.ActorID;
                repetitivePlan.Remark = plan.Remark;
                repetitivePlan.SOBT = plan.SOBT;
                repetitivePlan.SIBT = plan.SIBT;
                repetitivePlan.WeekSchedule = plan.WeekSchedule;
                repetitivePlan.ADEP = plan.ADEP;
                repetitivePlan.ADES = plan.ADES;
                repetitivePlan.CallSign = plan.CallSign;

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
        /// <param name="repetPlanID">主键值</param>
        /// <returns>Model.EF.RepetitivePlan</returns>
        public Model.EF.RepetitivePlan Get(int? repetPlanID)
        {
            if (repetPlanID == null) return null;

            return db.RepetitivePlan.Find(repetPlanID);
        }

        /// <summary>
        /// 【查询】：获取全部记录，按照指定方式排序
        /// 调用方式:
        /// RepetitivePlanDAL dal = new RepetitivePlanDAL();
        /// List<Model.EF.RepetitivePlan> result=dal.GetAllListOrderBy(m => m.RepetPlanID, true);
        /// 按照主键升序排列，如需按照其它列进行排序，指定【m => m.RepetPlanID】为所需列名即可
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.FlightPlan></returns>
        public List<Model.EF.RepetitivePlan> GetAllListOrderBy<TKey>(Expression<Func<Model.EF.RepetitivePlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.RepetitivePlan.OrderBy(orderBy).ToList();
            else
                return db.RepetitivePlan.OrderByDescending(orderBy).ToList();
        }

        /// <summary>
        /// 【查询】：按查询条件、排序方式获取记录
        /// 调用方式:
        /// RepetitivePlanDAL dal = new RepetitivePlanDAL();
        /// List<Model.EF.RepetitivePlan> result=dal.GetAllListWhereAndOrderBy(m => m.RepetPlanID>2, m=>m.RepetPlanID, true);
        /// 查询条件为RepetPlanID>2，如需其它查询条件，通过修改【m => m.RepetPlanID>2】即可
        /// 排序条件为RepetPlanID,如需按照其它列进行排序，指定【m => m.RepetPlanID】为所需列名即可
        /// 按照主键升序排列
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="planWhere">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.RepetitivePlan></returns>
        public List<Model.EF.RepetitivePlan> GetAllListWhereAndOrderBy<TKey>(Expression<Func<Model.EF.RepetitivePlan, bool>> planWhere,
            Expression<Func<Model.EF.RepetitivePlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.RepetitivePlan.Where(planWhere).OrderBy(orderBy).ToList();
            else
                return db.RepetitivePlan.Where(planWhere).OrderByDescending(orderBy).ToList();
        }

        /// <summary>
        /// 【查询】：按分页，排序方式获取记录
        /// 调用方式:
        /// RepetitivePlanDAL dal = new RepetitivePlanDAL();
        /// List<Model.EF.RepetitivePlan> result=dal.GetPageListOrderBy(1,10, m=>m.RepetPlanID, true);
        /// 排序条件为RepetPlanID,如需按照其它列进行排序，指定【m => m.RepetPlanID】为所需列名即可
        /// 按照主键升序排列
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <typeparam name="PageIndex">页码，从1开始</typeparam>
        /// <typeparam name="PageSize">每页显示的记录数</typeparam>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.RepetitivePlan></returns>
        public List<Model.EF.RepetitivePlan> GetPageListOrderBy<TKey>(int PageIndex, int PageSize,
            Expression<Func<Model.EF.RepetitivePlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.RepetitivePlan.OrderBy(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            else
                return db.RepetitivePlan.OrderByDescending(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 【查询】：按分页，查询条件、排序方式获取记录
        /// 调用方式:
        /// RepetitivePlanDAL dal = new RepetitivePlanDAL();
        /// List<Model.EF.RepetitivePlan> result=dal.GetPageListWhereAndOrderBy(1,10,m => m.RepetitivePlanID>2, m=>m.RepetitivePlanID, true);
        /// 查询条件为RepetitivePlanID>2，如需其它查询条件，通过修改【m => m.RepetitivePlanID>2】即可
        /// 排序条件为RepetitivePlanID,如需按照其它列进行排序，指定【m => m.RepetitivePlanID】为所需列名即可
        /// 按照主键升序排列
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <typeparam name="PageIndex">页码，从1开始</typeparam>
        /// <typeparam name="PageSize">每页显示的记录数</typeparam>
        /// <param name="planWhere">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isASC">是否升序</param>
        /// <returns>List<Model.EF.RepetitivePlan></returns>
        public List<Model.EF.RepetitivePlan> GetPageListWhereAndOrderBy<TKey>(int PageIndex, int PageSize,
            Expression<Func<Model.EF.RepetitivePlan, bool>> planWhere,
            Expression<Func<Model.EF.RepetitivePlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.RepetitivePlan.Where(planWhere).OrderBy(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            else
                return db.RepetitivePlan.Where(planWhere).OrderByDescending(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        #endregion


        //未使用EF
        #region
        //        public static bool Delete(string ids)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = string.Format("delete from RepetitivePlan WHERE  (RepetPlanID IN ({0}))", ids);

        //            return dao.ExecNonQuery(sql) > 0;
        //        }

        //        public static bool Add(FlightPlan model)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = @"insert into RepetitivePlan(FlightType,FlightDirHeight,PlanCode,AircraftType,StartDate,EndDate,CreateTime,ModifyTime,Creator,CreatorName,ActorID,CompanyCode3,AttchFile,Remark,
        //PlanState,ADES,ADEP,WeekSchedule,SIBT,SOBT,CallSign)
        //                                  values (@FlightType,@FlightDirHeight,@PlanCode,@AircraftType,@StartDate,@EndDate,@CreateTime,@ModifyTime,@Creator,@CreatorName,@ActorID,@CompanyCode3,@AttchFile,@Remark,
        //@PlanState,@ADES,@ADEP,@WeekSchedule,@SIBT,@SOBT,@CallSign)";
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
        //                            new SqlParameter("@CallSign", model.CallSign)
        //                                        };
        //            return dao.ExecNonQuery(sql, parameters) > 0;

        //        }
        //        public static bool Update(FlightPlan model)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = @"update RepetitivePlan set FlightType=@FlightType,FlightDirHeight=@FlightDirHeight,ModifyTime=@ModifyTime,PlanCode=@PlanCode,AircraftType=@AircraftType,StartDate=@StartDate,
        // ,EndDate=@EndDate,AttchFile=@AttchFile,Remark=@Remark,ADES=@ADES,ADEP=@ADEP,WeekSchedule=@WeekSchedule,SIBT=@SIBT,SOBT=@SOBT,CallSign=@CallSign where RepetPlanID=@ID";
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
        //                                         new SqlParameter("@CallSign", model.CallSign)};
        //            return dao.ExecNonQuery(sql, parameters) > 0;

        //        }


        //        public static PagedList<FlightPlan> GetMyRepetitivePlanList(int pageSize, int pageIndex, string strWhere)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = string.Format("select * from RepetitivePlan where {0}", strWhere);
        //            return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<FlightPlan>()).ToPagedList<FlightPlan>(pageIndex, pageSize);
        //        }


        //        /// <summary>
        //        /// 得到一个对象实体
        //        /// </summary>
        //        public static FlightPlan Get(int id)
        //        {
        //            SqlDbHelper dao = new SqlDbHelper();
        //            var sql = "select  top 1 * from RepetitivePlan where RepetPlanID=@PlanID";
        //            SqlParameter[] parameters = {
        //					new SqlParameter("@PlanID",id)
        //			};
        //            return dao.ExecSelectSingleCmd<FlightPlan>(ExecReader, sql, parameters);
        //        }

        //        private static FlightPlan ExecReader(SqlDataReader dr)
        //        {
        //            FlightPlan plan = new FlightPlan();
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
        //            plan.WeekSchedule = Convert.ToString(dr["WeekSchedule"]).Replace("*","");
        //            plan.SIBT = DateTime.ParseExact(dr["SIBT"].ToString(), "HH:mm:ss", null);
        //            plan.SOBT = DateTime.ParseExact(dr["SOBT"].ToString(), "HH:mm:ss", null);
        //            plan.CallSign = Convert.ToString(dr["CallSign"]);
        //            return plan;
        //        }

        #endregion
    }
}
