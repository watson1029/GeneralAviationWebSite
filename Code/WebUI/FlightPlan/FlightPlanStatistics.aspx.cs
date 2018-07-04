using BLL.FlightPlan;
using Model.EF;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_FlightPlanStatistics : BasePage
{
    private CurrentPlanBLL currPlanBll = new CurrentPlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
                    break;
                case "getinstance":
                    GetAllNodeInstance();
                    break;
                default:
                    break;
            }
        }
    }

    private void QueryData()
    {
        int page = Request.Form["page"] != null ? Convert.ToInt32(Request.Form["page"]) : 0;
        int size = Request.Form["rows"] != null ? Convert.ToInt32(Request.Form["rows"]) : 0;
        if (page < 1) return;
        int rowCount = 0;
        DateTime started = Request.Form["started"] != null ? Convert.ToDateTime(Request.Form["started"]) : DateTime.Now;
        DateTime ended= Request.Form["ended"] != null ? Convert.ToDateTime(Request.Form["ended"]) : DateTime.Now;
        var pageList = currPlanBll.GetList(page, size,out rowCount,started,ended);
        List<FlightPlanStatisticsAll> pagestatis = new List<FlightPlanStatisticsAll>();
        foreach (var item in pageList)
        {
            FlightPlanStatisticsAll statis = new FlightPlanStatisticsAll();
            statis.TimeDiff= "0";
            if (item.SecondDiff > 0 && item.SecondDiff < 60)
            {
                statis.TimeDiff = item.SecondDiff.ToString() + "秒";
            }
            else if (item.SecondDiff >= 60 && item.SecondDiff < 60 * 60)
            {
                statis.TimeDiff = (item.SecondDiff / 60).ToString() + "分" + (item.SecondDiff % 60).ToString() + "秒";
            }
            else if (item.SecondDiff >= 60 * 60)// && item.SecondDiff < 60 * 60 * 60
            {
                statis.TimeDiff = (item.SecondDiff / 3600).ToString() + "时" + ((item.SecondDiff - (item.SecondDiff / 3600) * 3600) / 60).ToString() + "分" + ((item.SecondDiff - (item.SecondDiff / 3600) * 3600) % 60).ToString() + "秒";
            }
            statis.AircraftNum = item.AircraftNum;
            statis.CompanyName = item.CompanyName;
            statis.Creator = item.Creator;
            statis.SecondDiff = item.SecondDiff;
            pagestatis.Add(statis);
        }
        var strJSON = Serializer.JsonDate(new { rows = pagestatis, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void GetAllNodeInstance()
    {
        int page = Request.Form["page"] != null ? Convert.ToInt32(Request.Form["page"]) : 0;
        int size = Request.Form["rows"] != null ? Convert.ToInt32(Request.Form["rows"]) : 0;
        string sort = Request.Form["sort"] ?? "";
        string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        //int pageCount = 0;
        int rowCount = 0;
        string orderField = sort.Replace("JSON_", "");
        int Creator = Request.Form["Creator"] != null ? Convert.ToInt32(Request.Form["Creator"]) : 0;
        //var strWhere = GetWhere(Creator);
        List<vGetCurrentPlanNodeInstance> pageList = currPlanBll.GetList(page, size,out rowCount,Creator, DateTime.Parse(Request.Form["started"]), DateTime.Parse(Request.Form["ended"]));
        foreach (var item in pageList)
        {
            item.Code = "0";
            item.StepID =Convert.ToInt32(item.ActualEndTime.Value.Subtract(item.ActualStartTime.Value).TotalSeconds);//用StepID代表时长
            if (item.StepID > 0 && item.StepID < 60)
            {
                item.Code = item.StepID.ToString() + "秒";
            }
            else if (item.StepID >= 60&& item.StepID< 60 * 60)
            {
                item.Code =(item.StepID/60).ToString()+"分"+ (item.StepID % 60).ToString()+"秒";
            }
            else if (item.StepID >= 60 * 60 && item.StepID < 60 * 60*60)
            {
                item.Code = (item.StepID /3600).ToString() + "时" + ((item.StepID-(item.StepID/3600)*3600)/60).ToString() + "分"+ ((item.StepID - (item.StepID / 3600) * 3600)%60).ToString() + "秒";
            }
        }
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    //private Expression<Func<vGetCurrentPlanNodeInstance, bool>> GetWhere(int Creator)
    //{
    //    Expression<Func<vGetCurrentPlanNodeInstance, bool>> predicate = PredicateBuilder.True<vGetCurrentPlanNodeInstance>();
    //    predicate = predicate.And(m => m.ActorID != m.Creator);
    //    predicate = predicate.And(m => m.State == 2 || m.State == 3);
    //    predicate = predicate.And(m => m.Creator== Creator);
    //    if (Request.Form["started"] != null && Request.Form["ended"] != null)
    //    {
    //        predicate = predicate.And(m => m.SOBT >=DateTime.Parse(Request.Form["started"]));
    //        predicate = predicate.And(m => m.SIBT <= DateTime.Parse(Request.Form["ended"]));
    //    }
    //    return predicate;
    //}
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "FlightPlanStatisticsCheck";
        }
    }
    #endregion
}