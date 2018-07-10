using BLL.FlightPlan;
using DAL.FlightPlan;
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

public partial class FlightPlan_MyFinishAuditFlightPlan1 : BasePage
{
    FlightPlanBLL bll = new FlightPlanBLL();
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
        var pageList = bll.GetNodeInstanceList(page, size, out pageCount, out rowCount, strWhere);
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
    private Expression<Func<vGetFlightPlanNodeInstance, bool>> GetWhere()
    {

        Expression<Func<vGetFlightPlanNodeInstance, bool>> predicate = PredicateBuilder.True<vGetFlightPlanNodeInstance>();
        predicate = predicate.And(m => User.RoleName.Contains(m.RoleName) && m.NextID == Guid.Empty);
        predicate = predicate.And(m => m.State == 2 || m.State == 3);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.Code == val);
        }

        return predicate;
    }
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid = Guid.Parse(Request.Form["id"]);
        Expression<Func<vGetFlightPlanNodeInstance, bool>> predicate = PredicateBuilder.True<vGetFlightPlanNodeInstance>();
        predicate = predicate.And(m => m.ActorID != m.Creator);
        predicate = predicate.And(m => m.ActorID == User.ID);
        predicate = predicate.And(m => m.State == 2 || m.State == 3);
        predicate = predicate.And(m => m.PlanID == planid);
        predicate = predicate.And(m => m.TWFID == (int)TWFTypeEnum.FlightPlan);
        var plan = bll.GetFlightPlanNodeInstance(predicate);
        var strJSON = JsonConvert.SerializeObject(plan);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void Save()
    {
        WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        var planid = Guid.Parse(Request.Form["id"]);
        try
        {

            var instance = insdal.GetNodeInstance(User.ID, (int)TWFTypeEnum.FlightPlan, planid);
            if (instance != null)
            {
                if (insdal.UpdateComment(instance.ID, Request.Form["Comments"] ?? ""))
                {
                    result.IsSuccess = true;
                    result.Msg = "保存成功！";
                }
            }
        }
        catch (Exception)
        {
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
            return "MyFinishAuditFlightPlanCheck";
        }
    }
    #endregion

}