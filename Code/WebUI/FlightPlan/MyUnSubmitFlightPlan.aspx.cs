using BLL.FlightPlan;
using DAL.FlightPlan;
using Model.EF;
using Model.FlightPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_MyUnSubmitFlightPlan : BasePage
{
    FlightPlanBLL bll = new FlightPlanBLL();
    RepetitivePlanBLL rpbll = new RepetitivePlanBLL();
    WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
    WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
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
        //int? id = null;
        //if (!string.IsNullOrEmpty(Request.Form["id"]))
        //{ id = Convert.ToInt32(Request.Form["id"]); }

        FlightPlan entity = null;
        var airlineworkText = "";
        if (string.IsNullOrEmpty(Request.Form["id"]))//新增
        {
            entity = new FlightPlan();

            entity.GetEntitySearchPars<FlightPlan>(this.Context);
            entity.FlightPlanID = Guid.NewGuid();
            entity.PlanState = "0";
            entity.CompanyCode3 = User.CompanyCode3 ?? "";
            entity.CompanyName = User.CompanyName;
            entity.Creator = User.ID;
            entity.CreatorName = User.UserName;
            entity.ActorID = User.ID;
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            bll.AddFlightPlanOther(Request.Form["MasterIDs"], entity.FlightPlanID.ToString(), Request.Form["id"], ref airlineworkText);
            entity.AirlineWorkText = airlineworkText;
            if (bll.Add(entity))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            entity = bll.Get(Guid.Parse(Request.Form["id"]));
            if (entity != null)
            {
                //      model.AttachFile = Request.Params["AttchFilesInfo"];
                entity.GetEntitySearchPars<FlightPlan>(this.Context);
                if (bll.Update(entity))
                {
                    result.IsSuccess = true;
                    result.Msg = "更新成功！";
                }
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
        var planid = Guid.Parse(Request.Form["id"]);
        if (insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.FlightPlan).Count > 0)
        {
            result.Msg = "一条长期计划无法创建两条申请流程，请联系管理员！";
        }
        else
        {
            try
            {
                wftbll.CreateWorkflowInstance((int)TWFTypeEnum.FlightPlan, planid, User.ID, User.UserName);
                insdal.Submit(planid, (int)TWFTypeEnum.FlightPlan, User.ID,User.UserName, User.RoleName.First(), "", insdal.UpdateFlightPlan);

                result.IsSuccess = true;
                result.Msg = "提交成功！";
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Msg = "提交失败！";
            }
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
        var planid =Guid.Parse(Request.Form["id"]);
        var plan = bll.GetvFlightPlan(planid);
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


    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryData()
    {
        int page = Request.Form["page"] != null ? Convert.ToInt32(Request.Form["page"]) : 0;
        int size = Request.Form["rows"] != null ? Convert.ToInt32(Request.Form["rows"]) : 0;
        //   string sort = Request.Form["sort"] ?? "";
        //   string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        //    string orderField = sort.Replace("JSON_", "");
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
    private Expression<Func<FlightPlan, bool>> GetWhere()
    {

        Expression<Func<FlightPlan, bool>> predicate = PredicateBuilder.True<FlightPlan>();
        predicate = predicate.And(m => m.PlanState == "0");
        predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.Code == val);
        }

        return predicate;
    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "MyUnSubmitFlightPlanCheck";
        }
    }
    #endregion
}