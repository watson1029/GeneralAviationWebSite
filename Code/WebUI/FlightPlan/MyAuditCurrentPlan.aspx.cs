﻿using BLL.FlightPlan;
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

public partial class FlightPlan_MyAuditCurrentPlan : BasePage
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
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "auditsubmit":
                    AuditSubmit();
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
    private Expression<Func<Model.EF.FlightPlan, bool>> GetWhere()
    {
        Expression<Func<Model.EF.FlightPlan, bool>> predicate = PredicateBuilder.True<Model.EF.FlightPlan>();
        predicate = predicate.And(m => m.PlanState != "0");
        predicate = predicate.And(m => m.Creator == User.ID);
        predicate = predicate.And(m => m.CreateTime == DateTime.Now.AddDays(-1));

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
            plan.WeekSchedule = plan.WeekSchedule.Replace("*", "");
            strJSON = JsonConvert.SerializeObject(plan);
        }

        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void AuditSubmit()
    {
        AjaxResult result = new AjaxResult();        
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;

        try
        {
            if (Request.Form["Auditresult"] == "0")
            {
                currPlanBll.Audit(planid, Request.Form["AuditComment"] ?? "");
            }
            else
            {
                currPlanBll.Terminate(planid,Request.Form["AuditComment"] ?? "");
            }
            result.IsSuccess = true;
            result.Msg = "提交成功！";
        }
        catch
        {
            result.IsSuccess = false;
            result.Msg = "提交失败！";
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
}