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
        int? id = null;
        if (!string.IsNullOrEmpty(Request.Form["id"]))
        { id = Convert.ToInt32(Request.Form["id"]); }

           FlightPlan model = null;
           if (!id.HasValue)//新增
           {
               model = new FlightPlan();  
              
               model.GetEntitySearchPars<FlightPlan>(this.Context);
            var repetplan=rpbll.Get(model.RepetPlanID);
               if (repetplan != null)
               {
                   model.RepetPlanID = repetplan.RepetPlanID;
                   model.FlightType = repetplan.FlightType;
                   model.AircraftType = repetplan.AircraftType;
                   model.CallSign = repetplan.CallSign;
                   model.FlightArea = repetplan.FlightArea;
                   model.FlightHeight = repetplan.FlightHeight;
                   // model.FlightDirHeight = repetplan.FlightDirHeight;
                   model.CallSign = repetplan.CallSign;
                   model.ADEP = repetplan.ADEP;
                   model.ADES = repetplan.ADES;
                   model.Remark = repetplan.Remark;
                   model.Alternate = repetplan.Alternate;
              //     model.FillObject(repetplan);
               }
               model.PlanCode = Request.Form["PlanCode"] ?? "";
               model.PlanState = "0";
               model.CompanyCode3 = User.CompanyCode3 ?? "";
               model.CompanyName = User.CompanyName;
               model.Creator = User.ID;
               model.CreatorName = User.UserName;
               model.ActorID = User.ID;
               model.CreateTime = DateTime.Now;
               model.ModifyTime = DateTime.Now;
               model.CreateSource = 2;
               if (bll.Add(model))
               {
                   result.IsSuccess = true;
                   result.Msg = "增加成功！";
               }
           }
           else//编辑
           {
               model = bll.Get(id.Value);
               if (model != null)
               {
                   model.GetEntitySearchPars<FlightPlan>(this.Context);
                   if (bll.Update(model))
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
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        if (insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.FlightPlan).Count > 0)
        {
            result.Msg = "一条长期计划无法创建两条申请流程，请联系管理员！";
        }
        else
        {
            try
            {
                wftbll.CreateWorkflowInstance((int)TWFTypeEnum.FlightPlan, planid, User.ID, User.UserName);
                insdal.Submit(planid, (int)TWFTypeEnum.FlightPlan, "", insdal.UpdateFlightPlan);

                result.IsSuccess = true;
                result.Msg = "提交成功！";
            }
             catch(Exception e)
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
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
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
            predicate = predicate.And(m => m.PlanCode == val);
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