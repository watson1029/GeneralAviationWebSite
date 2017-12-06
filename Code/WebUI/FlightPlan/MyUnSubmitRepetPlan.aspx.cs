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
        RepetitivePlan model = null;
        if (!id.HasValue)//新增
        {
             model = new RepetitivePlan()
            {
                PlanCode = OrderHelper.GenerateId(User.CompanyCode3),
                FlightType = Request.Form["FlightType"],
                AircraftType = Request.Form["AircraftType"],
                FlightDirHeight = Request.Form["FlightDirHeight"],
                StartDate = DateTime.Parse(Request.Form["StartDate"]),
                EndDate = DateTime.Parse(Request.Form["EndDate"]),
                ModifyTime = DateTime.Now,
                AttchFile = Request.Params["AttchFilesInfo"],
                Remark = Request.Form["Remark"],
                ADES = Request.Form["ADES"],
                ADEP = Request.Form["ADEP"],
                WeekSchedule = Request.Form["qx"],
                SIBT = TimeSpan.Parse(Request.Form["SIBT"]),
                SOBT = TimeSpan.Parse(Request.Form["SOBT"]),
                CallSign = Request.Form["CallSign"]
            };
            model.PlanState = "0";
            model.CompanyCode3 = User.CompanyCode3??"";
            model.Creator = User.ID;
            model.CreatorName = User.UserName;
            model.ActorID = User.ID;
            model.CreateTime = DateTime.Now;
            if (bll.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
          model=  bll.Get(id.Value);
          if (model!=null)
            {
                   model.FlightType = Request.Form["FlightType"];
                    model.AircraftType = Request.Form["AircraftType"];
                    model.FlightDirHeight = Request.Form["FlightDirHeight"];
                    model.StartDate = DateTime.Parse(Request.Form["StartDate"]);
                    model.EndDate = DateTime.Parse(Request.Form["EndDate"]);
                    model.ModifyTime = DateTime.Now;
                    model.AttchFile = Request.Params["AttchFilesInfo"];
                    model.Remark = Request.Form["Remark"];
                    model.ADES = Request.Form["ADES"];
                    model.ADEP = Request.Form["ADEP"];
                    model.WeekSchedule = Request.Form["qx"];
                    model.SIBT = TimeSpan.Parse(Request.Form["SIBT"]);
                    model.SOBT = TimeSpan.Parse(Request.Form["SOBT"]);
                    model.CallSign = Request.Form["CallSign"];
            
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
        WorkflowTemplateBLL.CreateWorkflowInstance(1, planid, User.ID, User.UserName);
        WorkflowNodeInstanceDAL.Submit(planid, "", WorkflowNodeInstanceDAL.UpdateRepetPlan);

        result.IsSuccess = true;
        result.Msg = "提交成功！";

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
        var plan = bll.Get(planid);
        var strJSON = "";
        if (plan != null)
        {
            strJSON=JsonConvert.SerializeObject(plan);
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
    private Expression<Func<RepetitivePlan, bool>> GetWhere()
    {

        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        predicate = predicate.And(m => m.PlanState=="0");
        predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = predicate.And(m => m.PlanCode == Request.Form["search_value"]);
        }

        return predicate;
    }


}