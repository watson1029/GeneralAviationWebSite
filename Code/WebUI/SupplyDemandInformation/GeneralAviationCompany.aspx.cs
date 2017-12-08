using BLL.BasicData;
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

public partial class SupplyDemandInformation_GeneralAviationCompany : BasePage
{
    CompanyBLL bll = new CompanyBLL();
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
                default:
                    break;
            }
        }
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
            model.ModifiedTime = DateTime.Parse(Request.Form["ModifiedTime"]);
            model.Summary = Server.HtmlDecode(Request.Form["Summary"]);
            model.SummaryCode = Server.HtmlDecode(Request.Form["SummaryCode"]);
            model.ModifiedBy = User.ID;
            model.ModifiedByName = User.UserName;
            model.State = "0";
            if (bll.Update(model) > 0)
            {
                result.IsSuccess = true;
                result.Msg = "更新成功！";
            }
        }
        else
        {
            result.IsSuccess = false;
            result.Msg = "没有找到相关记录！";
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
        //if (bll.Submit(id, User.ID, User.UserName))
        //{
        //    result.IsSuccess = true;
        //    result.Msg = "提交成功！";
        //}
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
        var company = bll.Get(id);
        var strJSON = "";
        if (company != null)
        {
            company.ModifiedBy = User.ID;
            company.ModifiedByName = User.UserName;
            company.ModifiedTime = DateTime.Now;
            strJSON = JsonConvert.SerializeObject(company);
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
    private Expression<Func<Company, bool>> GetWhere()
    {
        Expression<Func<Company, bool>> predicate = PredicateBuilder.True<Company>();
        predicate = predicate.And(m => m.Catalog == 1);

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

}