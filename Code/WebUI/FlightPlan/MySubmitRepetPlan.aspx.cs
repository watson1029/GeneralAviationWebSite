﻿using BLL.FlightPlan;
using System;
using System.Text;
using Untity;
using Model.EF;
using System.Linq.Expressions;
using Newtonsoft.Json;
using DAL.FlightPlan;
using System.Linq;
using ViewModel.FlightPlan;
using Model.FlightPlan;

public partial class FlightPlan_MySubmitRepetPlan : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
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
                case "getinstance":
                    GetAllNodeInstance();
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
        int page = Convert.ToInt32(Request.Form["page"] ?? "0");
        int size = Convert.ToInt32(Request.Form["rows"] ?? "0");
        // string sort = Request.Form["sort"] ?? "";
        // string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        //string orderField = sort.Replace("JSON_", "");
        var strWhere = GetWhere();
        var pageList = bll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private Expression<Func<RepetitivePlan, bool>> GetWhere()
    {
        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        predicate = predicate.And(m => m.PlanState != "0" && m.Creator == User.ID);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.Code == val);
        }
        return predicate;
    }

    private void GetData()
    {
        var planid = Guid.Parse(Request.Form["id"]);
        var plan = bll.Get(planid);
        var strJSON = JsonConvert.SerializeObject(plan);
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    private void GetAllNodeInstance()
    {
        var planid = Guid.Parse(Request.Form["id"]);
        var list = insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.RepetitivePlan).Where(u => u.ActorID != User.ID).ToList();
       var strJSON = Serializer.JsonDate(new { rows = list, total = list.Count });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    
    }

    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "MySubmitRepetPlanCheck";
        }
    }
    #endregion
}