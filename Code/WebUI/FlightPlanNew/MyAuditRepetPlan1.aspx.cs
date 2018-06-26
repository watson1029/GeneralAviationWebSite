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

public partial class FlightPlanNew_MyAuditRepetPlan1 : BasePage
{
    RepetitivePlanNewBLL bll = new RepetitivePlanNewBLL();
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
        int page = Convert.ToInt32(Request.Form["page"] ?? "0");
        int size = Convert.ToInt32(Request.Form["rows"] ?? "0");
        // string sort = Request.Form["sort"] ?? "";
        // string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        // string orderField = sort.Replace("JSON_", "");
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
    /// 
    private Expression<Func<RepetPlanNew, bool>> GetWhere()
    {

        Expression<Func<RepetPlanNew, bool>> predicate = PredicateBuilder.True<RepetPlanNew>();
        predicate = predicate.And(m => m.Status == 2);
        predicate = predicate.And(m => m.IsUrgentTask == false);
        predicate = predicate.And(m => m.IsCrossDay == true);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.PlanCode == val);
        }

        return predicate;
    }
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = bll.Get(planid);
        var strJSON = JsonConvert.SerializeObject(plan);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void AuditSubmit()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "提交失败！";
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        try
        {
            var model = bll.Get(planid);
            if (Request.Form["Auditresult"] == "0")
            {
                model.Status = 3;
            }
            else
            {
                model.Status = 4;
            }
            model.AuditTime = DateTime.Now;
            model.AuditComment = Request.Form["AuditComment"] ?? "";
            if (bll.Update(model))
            {
                result.IsSuccess = true;
                result.Msg = "提交成功！";
            }
        }
        catch (Exception)
        {
            result.IsSuccess = false;
            result.Msg = "操作失败！";
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();

    }


    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "MyAuditRepetPlanNewCheck";
        }
    }
    #endregion
}