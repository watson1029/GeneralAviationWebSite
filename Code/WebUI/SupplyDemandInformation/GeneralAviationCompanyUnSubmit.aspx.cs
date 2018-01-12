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

public partial class SupplyDemandInformation_GeneralAviationCompanyUnSubmit : BasePage
{
    CompanySummaryBLL bll = new CompanySummaryBLL();
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
                case "submit":
                    Submit();
                    break;
                case "del":
                    Delete();
                    break;
                case "init":
                    Init();
                    break;
                default:
                    break;
            }
        }
    }

    private void Delete()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "删除失败！";
        if (Request.Form["cbx_select"] != null)
        {
            if (bll.Delete(Request.Form["cbx_select"].ToString()))
            {
                result.IsSuccess = true;
                result.Msg = "删除成功！";
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }

    private void Save()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        int id = 0;
        if (!string.IsNullOrEmpty(Request.Form["id"]))
        {
            id = Convert.ToInt32(Request.Form["id"]);
            var model = bll.Get(id);
            model.Title = Request.Form["Title"];
            model.ModifiedTime = DateTime.Parse(Request.Form["ModifiedTime"]);
            model.Summary = Server.HtmlDecode(Request.Form["Summary"]);
            model.SummaryCode = Server.HtmlDecode(Request.Form["SummaryCode"]);
            model.State = "0";
            if (bll.Update(model))
            {
                result.IsSuccess = true;
                result.Msg = "更新成功！";
            }
        }
        else
        {
            var model = new CompanySummary();
            model.Title = Request.Form["Title"];
            model.ModifiedTime = DateTime.Parse(Request.Form["ModifiedTime"]);
            model.Summary = Server.HtmlDecode(Request.Form["Summary"]);
            model.SummaryCode = Server.HtmlDecode(Request.Form["SummaryCode"]);
            model.ModifiedBy = User.ID;
            model.ModifiedByName = User.UserName;
            model.CompanyName = User.CompanyName;
            model.ActorID = User.ID;
            model.State = "0";
            if (bll.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }

    private void Submit()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "提交失败！";
        var id = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        if (bll.Submit(id, User.ID, User.UserName))
        {
            result.IsSuccess = true;
            result.Msg = "提交成功！";
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var id = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var companySummary = bll.Get(id);
        var strJSON = "";
        if (companySummary != null)
        {
            strJSON = JsonConvert.SerializeObject(companySummary);
        }
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
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
        predicate = predicate.And(m => m.State == "0" && m.ActorID == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            string strValue = Request.Form["search_value"].ToString();
            if (Request.Form["search_type"].ToString() == "CompanyName")
            {
                predicate = predicate.And(u => u.CompanyName.Contains(strValue));
            }
        }

        return predicate;
    }

    private void Init()
    {
        CompanySummary cs = new CompanySummary
        {
            ModifiedBy = User.ID,
            ModifiedByName = User.UserName,
            ModifiedTime = DateTime.Today,
            CompanyName = User.CompanyName
        };
        var strJSON = string.Empty;
        strJSON = JsonConvert.SerializeObject(cs);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "GeneralAviationCompanyUnSubmitCheck";
        }
    }
    #endregion
}