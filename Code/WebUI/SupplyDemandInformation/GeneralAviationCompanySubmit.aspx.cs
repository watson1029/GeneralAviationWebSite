﻿using BLL.SupplyDemandInformation;
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

public partial class SupplyDemandInformation_GeneralAviationCompanySubmit : BasePage
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
                case "queryone":
                    GetData();
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
        predicate = predicate.And(m => m.State != "0" && m.ModifiedBy == User.ID);

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

    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "GeneralAviationCompanySubmitCheck";
        }
    }
    #endregion
}