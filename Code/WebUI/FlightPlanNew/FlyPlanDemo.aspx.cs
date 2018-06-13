using BLL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlanNew_FlyPlanDemo : System.Web.UI.Page
{
    private FlyPlanDemoBLL flybll = new FlyPlanDemoBLL();
    private BusyTimeBLL busybll = new BusyTimeBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "general"://根据长期计划自动生成飞行计划
                    GeneralData();
                    break;
                case "query"://查询数据
                    QueryData();
                    break;
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "submit":
                    Save();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 获取指定Date的数据
    /// </summary>
    private void GetData()
    {
        var model = flybll.GetFlyPlan(Request.Form["id"]);
        var strJSON = JsonConvert.SerializeObject(model);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    private void GeneralData()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "飞行计划生成失败！";
        if (flybll.GeneralFlyData())
        {
            result.IsSuccess = true;
            result.Msg = "飞行计划生成成功！";
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }

    private void QueryData()
    {
        int page = Request.Form["page"] != null ? Convert.ToInt32(Request.Form["page"]) : 0;
        int size = Request.Form["rows"] != null ? Convert.ToInt32(Request.Form["rows"]) : 0;
        string sort = Request.Form["sort"] ?? "";
        string order = Request.Form["order"] ?? "";

        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        string orderField = sort.Replace("JSON_", "");
        var strWhere = GetWhere();

        var pageList = flybll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private Expression<Func<FlyPlanDemo, bool>> GetWhere()
    {
        Expression<Func<FlyPlanDemo, bool>> predicate = PredicateBuilder.True<FlyPlanDemo>();
        predicate = predicate.And(m => 1 == 1);
        //if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        //{
        //    var val = Request.Form["search_value"].Trim();
        //    predicate = predicate.And(m => m.UserName == val);

        //    //  sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        //}
        return predicate;
    }

    private void Save()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        var model = flybll.GetFlyPlan(Request.Form["id"]);
        if (model != null)
        {
            var busy = busybll.Get(model.PlanDate.Value);
            var begintime = new DateTime(model.PlanDate.Value.Year, model.PlanDate.Value.Month, model.PlanDate.Value.Day, int.Parse(Request.Form["PlanBeginTime"].Split(':')[0]), int.Parse(Request.Form["PlanBeginTime"].Split(':')[1]), 0);
            var endtime = new DateTime(model.PlanDate.Value.Year, model.PlanDate.Value.Month, model.PlanDate.Value.Day, int.Parse(Request.Form["PlanEndTime"].Split(':')[0]), int.Parse(Request.Form["PlanEndTime"].Split(':')[1]), 0);
            if (begintime >= busy.BusyEndTime || endtime <= busy.BusyBeginTime)
            {
                //model.PlanDate = string.IsNullOrEmpty(Request.Form["PlanDate"].ToString()) ? (DateTime?)null : DateTime.Parse(Request.Form["PlanDate"].ToString());
                model.PlanBeginTime = begintime;
                model.PlanEndTime = endtime;
                model.AircraftModel = Request.Form["AircraftModel"];
                model.TakeOffTime = string.IsNullOrEmpty(Request.Form["TakeOffTime"]) ? (DateTime?)null : new DateTime(model.PlanDate.Value.Year, model.PlanDate.Value.Month, model.PlanDate.Value.Day, int.Parse(Request.Form["TakeOffTime"].Split(':')[0]), int.Parse(Request.Form["TakeOffTime"].Split(':')[1]), 0);
                model.LandTime = string.IsNullOrEmpty(Request.Form["LandTime"]) ? (DateTime?)null : new DateTime(model.PlanDate.Value.Year, model.PlanDate.Value.Month, model.PlanDate.Value.Day, int.Parse(Request.Form["LandTime"].Split(':')[0]), int.Parse(Request.Form["LandTime"].Split(':')[1]), 0);

                if (flybll.Update(model))
                {
                    result.IsSuccess = true;
                    result.Msg = "更新成功！";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Msg = string.Format("计划时间处于繁忙时间段：{0} - {1}！", busy.BusyBeginTime.Value.ToString("HH:mm"), busy.BusyEndTime.Value.ToString("HH:mm"));
            }
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
}