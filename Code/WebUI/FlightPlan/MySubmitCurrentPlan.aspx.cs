using BLL.FlightPlan;
using System;
using System.Text;
using Untity;
using System.Linq.Expressions;
using Model.EF;
using DAL.FlightPlan;
using Newtonsoft.Json;
using System.Linq;
using System.Data.Entity;

public partial class FlightPlan_MySubmitCurrentPlan :BasePage
{
    private CurrentPlanBLL currPlanBll = new CurrentPlanBLL();
    private WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
    public override string PageRightCode
    {
        get
        {
            return "MySubmitCurrentPlanCheck";
        }
    }
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
        predicate = predicate.And(m => m.PlanState != "0" && m.Creator == User.ID && DbFunctions.TruncateTime(m.SOBT) == currDate);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = u => u.PlanCode == Request.Form["search_value"];
        }

        return predicate;
    }
    private void GetData()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = currPlanBll.GetByCurrid(planid);
        var strJSON = JsonConvert.SerializeObject(plan);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void GetAllNodeInstance()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        
        var list = insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.CurrentPlan).Where(u => u.ActorID != User.ID).ToList();
        var strJSON = Serializer.JsonDate(new { rows = list, total = list.Count });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();

    }
}