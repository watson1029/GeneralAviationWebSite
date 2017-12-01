namespace BLL.FlightPlan
{
    public class FlightPlanBLL
    {
        //private FlightPlanDAL dal = new FlightPlanDAL();

        //public void Delete(string ids)
        //{
        //    if (!string.IsNullOrEmpty(ids))
        //    {
        //        string[] idsArray = ids.Split(',');
        //        foreach (var item in idsArray)
        //        {
        //            dal.Delete(int.Parse(item));
        //        }
        //    }
        //}
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public bool Add(string PlanCode, string FlightType, string AircraftType, string FlightDirHeight, System.DateTime StartDate,
        //    System.DateTime EndDate, string CompanyCode3, string AttchFile,
        //    string PlanState, Nullable<int> ActorID, int Creator, string Remark, System.TimeSpan SOBT, System.TimeSpan SIBT, string WeekSchedule,
        //    string ADEP, string ADES, string CreatorName, string CallSign)
        //{
        //    /*
        //    需增加字段值检查逻辑                       
        //    */

        //    return dal.Add(PlanCode, FlightType, AircraftType, FlightDirHeight, StartDate,
        //    EndDate, CompanyCode3, AttchFile,
        //    PlanState, ActorID, Creator, Remark, SOBT, SIBT, WeekSchedule,
        //    ADEP, ADES, CreatorName, CallSign);
        //}

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(int? flightPlanID, string PlanCode, string FlightType, string AircraftType, string FlightDirHeight,
        //    System.DateTime StartDate, System.DateTime EndDate, string CompanyCode3, string AttchFile,
        //    string PlanState, Nullable<int> ActorID, string Remark, System.TimeSpan SOBT, System.TimeSpan SIBT, string WeekSchedule,
        //    string ADEP, string ADES, string CallSign)
        //{
        //    /*
        //    需增加字段值检查逻辑                       
        //    */

        //    return dal.Update(flightPlanID,PlanCode, FlightType, AircraftType, FlightDirHeight, StartDate,
        //    EndDate, CompanyCode3, AttchFile,
        //    PlanState, ActorID, Remark, SOBT, SIBT, WeekSchedule,
        //    ADEP, ADES, CallSign);
        //}


        //public PagedList<Model.EF.FlightPlan> GetFlightPlanPageListWhereAndOrderBy<TKey>(int PageIndex, int PageSize,
        //    Expression<Func<Model.EF.FlightPlan, bool>> planWhere,
        //    Expression<Func<Model.EF.FlightPlan, TKey>> orderBy, bool isASC)
        //{
        //    return dal.GetPageListWhereAndOrderBy(pageIndex, pageSize,  strWhere);
        //}

        //public static FlightPlan Get(int id)
        //{
        //    return FlightPlanDAL.Get(id);
        //}
    }
}
