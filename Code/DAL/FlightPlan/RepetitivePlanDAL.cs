using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Untity;
using Untity.DB;
using Model.EF;
using System.Data.Entity;

namespace DAL.FlightPlan
{
    public class RepetitivePlanDAL
    {
        private ZHCC_GAPlanEntities db = new ZHCC_GAPlanEntities();

        public static bool Delete(string ids)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = string.Format("delete from RepetitivePlan WHERE  (RepetPlanID IN ({0}))", ids);

            return dao.ExecNonQuery(sql) > 0;
        }

        //使用EF     
        #region

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public bool Add(Model.EF.RepetitivePlan plan)
        {
            if (plan == null) return false;

            db.RepetitivePlan.Add(plan);
            db.SaveChanges();
            return false;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            if (id == null) return false;

            var plan = db.RepetitivePlan.Find(id);
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
        /// 获取全部记录，按照指定方式排序
        /// 调用方式为  List<Model.EF.RepetitivePlan> result=GetAllListOrderBy<string>(
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="orderBy">排序字段</param>
        /// /// <param name="isASC">是否升序</param>
        /// <returns></returns>
        public List<Model.EF.RepetitivePlan> GetAllListOrderBy<TKey>(Expression<Func<Model.EF.RepetitivePlan, TKey>> orderBy,bool isASC)
        {
            if(isASC)
                return db.RepetitivePlan.OrderBy(orderBy).ToList();
            else
                return db.RepetitivePlan.OrderByDescending(orderBy).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="planWhere"></param>
        /// <param name="orderBy"></param>
        /// <param name="isASC"></param>
        /// <returns></returns>
        public List<Model.EF.RepetitivePlan> GetAllListWhereAndOrderBy<TKey>(Expression<Func<Model.EF.RepetitivePlan,bool>> planWhere,
            Expression<Func<Model.EF.RepetitivePlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.RepetitivePlan.Where(planWhere).OrderBy(orderBy).ToList();
            else
                return db.RepetitivePlan.Where(planWhere).OrderByDescending(orderBy).ToList();
        }

        public List<Model.EF.RepetitivePlan> GetPageListWhereAndOrderBy<TKey>(int PageIndex,int PageSize,
            Expression<Func<Model.EF.RepetitivePlan, bool>> planWhere,
            Expression<Func<Model.EF.RepetitivePlan, TKey>> orderBy, bool isASC)
        {
            if (isASC)
                return db.RepetitivePlan.Where(planWhere).OrderBy(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            else
                return db.RepetitivePlan.Where(planWhere).OrderByDescending(orderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public bool Update(Model.EF.RepetitivePlan plan)
        {
            var RepetitivePlan = db.RepetitivePlan.Find(plan.RepetPlanID);

            RepetitivePlan.FlightType = plan.FlightType;
            RepetitivePlan.FlightDirHeight = plan.FlightDirHeight;
            RepetitivePlan.PlanCode = plan.PlanCode;
            RepetitivePlan.AircraftType = plan.AircraftType;
            RepetitivePlan.StartDate = plan.StartDate;
            RepetitivePlan.EndDate = plan.EndDate;
            RepetitivePlan.ModifyTime = plan.ModifyTime;
            RepetitivePlan.AttchFile = plan.AttchFile;
            RepetitivePlan.Remark = plan.Remark;
            RepetitivePlan.ADES = plan.ADES;
            RepetitivePlan.ADEP = plan.ADEP;
            RepetitivePlan.WeekSchedule = plan.WeekSchedule;
            RepetitivePlan.SIBT = plan.SIBT;
            RepetitivePlan.SOBT = plan.SOBT;

            if (plan != null)
            {
                return db.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public static bool Add(Model.FlightPlan.RepetitivePlan model)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = @"insert into RepetitivePlan(FlightType,FlightDirHeight,PlanCode,AircraftType,StartDate,EndDate,CreateTime,ModifyTime,Creator,CreatorName,ActorID,CompanyCode3,AttchFile,Remark,
PlanState,ADES,ADEP,WeekSchedule,SIBT,SOBT,CallSign)
                                  values (@FlightType,@FlightDirHeight,@PlanCode,@AircraftType,@StartDate,@EndDate,@CreateTime,@ModifyTime,@Creator,@CreatorName,@ActorID,@CompanyCode3,@AttchFile,@Remark,
@PlanState,@ADES,@ADEP,@WeekSchedule,@SIBT,@SOBT,@CallSign)";
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
                            new SqlParameter("@CallSign", model.CallSign)
                                        };
            return dao.ExecNonQuery(sql, parameters) > 0;

        }
        public static bool Update(Model.FlightPlan.RepetitivePlan model)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = @"update RepetitivePlan set FlightType=@FlightType,FlightDirHeight=@FlightDirHeight,ModifyTime=@ModifyTime,PlanCode=@PlanCode,AircraftType=@AircraftType,StartDate=@StartDate,
 ,EndDate=@EndDate,AttchFile=@AttchFile,Remark=@Remark,ADES=@ADES,ADEP=@ADEP,WeekSchedule=@WeekSchedule,SIBT=@SIBT,SOBT=@SOBT,CallSign=@CallSign where RepetPlanID=@ID";
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
                                         new SqlParameter("@CallSign", model.CallSign)};
            return dao.ExecNonQuery(sql, parameters) > 0;

        }


        public static PagedList<Model.FlightPlan.RepetitivePlan> GetMyRepetitivePlanList(int pageSize, int pageIndex, string strWhere)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = string.Format("select * from RepetitivePlan where {0}", strWhere);
            return (dao.ExecSelectCmd(ExecReader, sql) ?? new List<Model.FlightPlan.RepetitivePlan>()).ToPagedList<Model.FlightPlan.RepetitivePlan>(pageIndex, pageSize);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.FlightPlan.RepetitivePlan Get(int id)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = "select  top 1 * from RepetitivePlan where RepetPlanID=@PlanID";
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID",id)
			};
            return dao.ExecSelectSingleCmd<Model.FlightPlan.RepetitivePlan>(ExecReader, sql, parameters);
        }

        private static Model.FlightPlan.RepetitivePlan ExecReader(SqlDataReader dr)
        {
            Model.FlightPlan.RepetitivePlan plan = new Model.FlightPlan.RepetitivePlan();
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
            plan.WeekSchedule = Convert.ToString(dr["WeekSchedule"]).Replace("*","");
            plan.SIBT = DateTime.ParseExact(dr["SIBT"].ToString(),"HH:mm:ss",null);
            plan.SOBT = DateTime.ParseExact(dr["SOBT"].ToString(), "HH:mm:ss", null);
            plan.CallSign = Convert.ToString(dr["CallSign"]);
            return plan;
        }
    }
}
