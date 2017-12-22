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
using System.Data.Entity;

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
                case "batchaudit":
                    BatchAuditSubmit();
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
        var currDate = DateTime.Now.Date;
        predicate = predicate.And(m => m.ActorID == User.ID && DbFunctions.TruncateTime(m.SOBT) == currDate);

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
        var plan = currPlanBll.GetByCurrid(planid);
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
    private void AuditSubmit()
    {
        AjaxResult result = new AjaxResult();        
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;

        try
        {
            if (Request.Form["Auditresult"] == "0")
            {
                currPlanBll.Audit(planid, Request.Form["AuditComment"] ?? "", User.ID);
            }
            else
            {
                currPlanBll.Terminate(planid, Request.Form["AuditComment"] ?? "", User.ID);
            }
            result.IsSuccess = true;
            result.Msg = "提交成功！";
        }
        catch(Exception ex)
        {
            result.IsSuccess = false;
            result.Msg = "提交失败！";
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    private void BatchAuditSubmit()
    {
        AjaxResult result = new AjaxResult();

        if (Request.Form["cbx_select"] != null)
        {
            try
            {
                var arr = Request.Form["cbx_select"].ToString().Split(',');
                var auditComment = Request.Form["BatchAuditComment"] ?? "";
                if (Request.Form["BatchAuditresult"] == "0")
                {
                    foreach (var item in arr)
                    {
                        currPlanBll.Audit(int.Parse(item), Request.Form["AuditComment"] ?? "", User.ID);
                    }
                }
                else
                {
                    foreach (var item in arr)
                    {
                        currPlanBll.Terminate(int.Parse(item), Request.Form["AuditComment"] ?? "", User.ID);
                    }
                }
                result.IsSuccess = true;
                result.Msg = "操作成功！";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = "操作失败！"+"\r\n"+ex.Message;
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();

    }
}