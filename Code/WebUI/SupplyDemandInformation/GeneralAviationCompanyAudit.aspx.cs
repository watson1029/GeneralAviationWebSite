using BLL.SupplyDemandInformation;
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

public partial class SupplyDemandInformation_GeneralAviationCompanyAudit : BasePage
{
    private CompanySummaryBLL bll = new CompanySummaryBLL();
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
    private Expression<Func<CompanySummary, bool>> GetWhere()
    {
        Expression<Func<CompanySummary, bool>> predicate = PredicateBuilder.True<CompanySummary>();
        predicate = predicate.And(m => m.ActorID == User.ID && m.State != "0");

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            string strValue = Request.Form["search_value"].ToString();
            if (Request.Form["search_type"].ToString() == "CompanyName")
            {
                predicate = u => u.CompanyName.Contains(strValue);
            }
        }

        return predicate;
    }

    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var id = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var sdi = bll.Get(id);
        var strJSON = "";
        if (sdi != null)
        {
            strJSON = JsonConvert.SerializeObject(sdi);
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
                bll.Audit(planid, Request.Form["AuditComment"] ?? "");
            }
            else
            {
                bll.Terminate(planid, Request.Form["AuditComment"] ?? "");
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

    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "GeneralAviationCompanyAuditCheck";
        }
    }
    #endregion
}