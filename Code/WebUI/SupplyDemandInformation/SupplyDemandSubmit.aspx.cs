using BLL.SupplyDemandInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class SupplyDemandInformation_SupplyDemandSubmit : BasePage
{
    private SupplyDemandBLL bll = new SupplyDemandBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
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
    private Expression<Func<Model.EF.SupplyDemandInfo, bool>> GetWhere()
    {
        Expression<Func<Model.EF.SupplyDemandInfo, bool>> predicate = PredicateBuilder.True<Model.EF.SupplyDemandInfo>();
        predicate = predicate.And(m => m.State != "0");
        predicate = predicate.And(m => m.Creator == User.ID);
        predicate = predicate.And(m => m.CreateTime == DateTime.Now.AddDays(-1));

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            string strValue = Request.Form["search_value"].ToString();
            if (Request.Form["search_type"].ToString() == "CompanyName")
            {
                predicate = u => u.CompanyName.Contains(strValue);
            }
            else if (Request.Form["search_type"].ToString() == "Catalog")
            {
                predicate = u => u.Catalog.Contains(strValue);
            }
        }

        return predicate;
    }
}