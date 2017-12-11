using BLL.FlightPlan;
using System;
using System.Text;
using Untity;
using System.Linq.Expressions;

public partial class FlightPlan_MySubmitCurrentPlan :BasePage
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
}