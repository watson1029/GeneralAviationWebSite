using BLL.FlightPlan;
using DAL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Text;
using Untity;

public partial class FlightPlan_MyUnSubmitRepetPlan : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
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
        RepetitivePlan model = null;
        if (!id.HasValue)//新增
        {
            model = new RepetitivePlan();
            model.GetEntitySearchPars<RepetitivePlan>(this.Context);
            model.WeekSchedule = Request.Form["qx"];
            model.AttchFile = Request.Params["AttchFilesInfo"];
            model.PlanState = "0";
            model.CompanyCode3 = User.CompanyCode3 ?? "";
            model.Creator = User.ID;
            model.CreatorName = User.UserName;
            model.ActorID = User.ID;
            model.CreateTime = DateTime.Now;
            model.ModifyTime = DateTime.Now;
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
                model.GetEntitySearchPars<RepetitivePlan>(this.Context);
                model.ModifyTime = DateTime.Now;
                model.AttchFile = Request.Params["AttchFilesInfo"];
                model.WeekSchedule = Request.Form["qx"];
                if (bll.Update(model))
                {
                    result.IsSuccess = true;
                    result.Msg = "更新成功！";
                }
            }
        };
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
        WorkflowTemplateBLL.CreateWorkflowInstance((int)TWFTypeEnum.RepetitivePlan, planid, User.ID, User.UserName);
        WorkflowNodeInstanceDAL.Submit(planid,(int)TWFTypeEnum.RepetitivePlan, "", WorkflowNodeInstanceDAL.UpdateRepetPlan);

        result.IsSuccess = true;
        result.Msg = "提交成功！";

        Response.Clear();
        Response.Write(result.ToJsonString());
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
    private Expression<Func<RepetitivePlan, bool>> GetWhere()
    {

        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        predicate = predicate.And(m => m.PlanState == "0");
        predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = predicate.And(m => m.PlanCode == Request.Form["search_value"]);
        }

        return predicate;
    }


}