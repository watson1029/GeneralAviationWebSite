using BLL.FlightPlan;
using DAL.FlightPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;
using System.Linq.Expressions;
using Model.EF;
using Model.FlightPlan;
using System.Data.Entity;

public partial class FlightPlan_MyAuditCurrentPlanOdy : BasePage
{
    private CurrentPlanBLL currPlanBll = new CurrentPlanBLL();
    private FlightPlanBLL flyBLL = new FlightPlanBLL();
    public override string PageRightCode
    {
        get
        {
            return "MyAuditCurrentPlanOdyCheck";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
                    break;
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "save":
                    Save();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 查询数据
    /// </summary>
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
        var pageList = currPlanBll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private Expression<Func<V_CurrentPlan, bool>> GetWhere()
    {
        Expression<Func<V_CurrentPlan, bool>> predicate = PredicateBuilder.True<V_CurrentPlan>();
        //var currDate = DateTime.Now.Date;
        //&& DbFunctions.TruncateTime(m.SOBT) == currDate
        predicate = predicate.And(m => m.ActorID == null && m.PlanState == "end");

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = u => u.PlanCode == Request.Form["search_value"];
        }

        return predicate;
    }
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = currPlanBll.Get(planid);
        var strJSON = "";
        if (plan != null)
        {
            strJSON = JsonConvert.SerializeObject(plan);
        }

        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void Save()
    {
        AjaxResult result = new AjaxResult();

        try
        {
            var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
            var startTime = Request.Form["ActualStartTime"];
            var endTime = Request.Form["ActualEndTime"];

            FlightPlan model = flyBLL.Get(planid);
            model.GetEntitySearchPars<RepetitivePlan>(this.Context);
            model.ActualStartTime = Convert.ToDateTime(startTime);
            model.ActualEndTime = Convert.ToDateTime(endTime);
            model.ModifyTime = DateTime.Now;
            flyBLL.Update(model);

            result.IsSuccess = true;
            result.Msg = "更新成功！";
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Msg = "更新失败！\r\n" + ex.Message;
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
}