using BLL.SupplyDemandInformation;
using Newtonsoft.Json;
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
                case "queryone"://获取一条记录
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
    private Expression<Func<Model.EF.SupplyDemandInfo, bool>> GetWhere()
    {
        Expression<Func<Model.EF.SupplyDemandInfo, bool>> predicate = PredicateBuilder.True<Model.EF.SupplyDemandInfo>();
        predicate = predicate.And(m => m.State != "0" && m.Creator == User.ID);

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

    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var id = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var supplyDemandInfo = bll.Get(id);
        var strJSON = "";
        if (supplyDemandInfo != null)
        {
            strJSON = JsonConvert.SerializeObject(supplyDemandInfo);
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
            return "SupplyDemandSubmitCheck";
        }
    }
    #endregion
}