using BLL.FlightPlan;
using DAL.FlightPlan;
using Model.FlightPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_MyUnSubmitFlightPlan : BasePage
{
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
            if (FlightPlanBLL.Delete(Request.Form["cbx_select"].ToString()))
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
        var model = new vFlightPlan()
        {
            PlanCode = OrderHelper.GenerateId(""),
            FlightType = Request.Form["FlightType"],
            AircraftType = Request.Form["AircraftType"],
            FlightDirHeight = Request.Form["FlightDirHeight"],
            StartDate = DateTime.Parse(Request.Form["StartDate"]),
            EndDate = DateTime.Parse(Request.Form["EndDate"]),
            ModifyTime = DateTime.Now,
            AttchFile = "",
            Remark = Request.Form["Remark"],
            ADES = Request.Form["ADES"],
            ADEP = Request.Form["ADEP"],
            WeekSchedule = Request.Form["qx"],
            SIBT = DateTime.Parse(Request.Form["SIBT"]),
            SOBT = DateTime.Parse(Request.Form["SOBT"]),
            CallSign = Request.Form["CallSign"],
            ContactWay = Request.Form["CallSign"],
            WeatherCondition = Request.Form["CallSign"],
            Pilot = Request.Form["Pilot"],
            AircraftNum =int.Parse(Request.Form["Pilot"]),
            AircrewGroupNum = int.Parse(Request.Form["Pilot"]),
            RadarCode=Request.Form["RadarCode"]
        };
        if (!id.HasValue)//新增
        {
            model.PlanState = "0";
            model.CompanyCode3 = "";
            model.Creator = User.ID;
            model.CreatorName = User.UserName;
            model.ActorID = User.ID;
            model.CreateTime = DateTime.Now;
            if (FlightPlanBLL.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            model.RepetPlanID = id.Value;
            if (FlightPlanBLL.Update(model))
            {
                result.IsSuccess = true;
                result.Msg = "更新成功！";
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
        WorkflowTemplateBLL.CreateWorkflowInstance(1, planid, User.ID, User.UserName);
        WorkflowNodeInstanceDAL.Submit(planid, "");

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
        var plan = FlightPlanBLL.Get(planid);
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
        string sort = Request.Form["sort"] ?? "";
        string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        string orderField = sort.Replace("JSON_", "");
        string strWhere = GetWhere();
        var pageList = FlightPlanBLL.GetMyFlightPlanList(size, page, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = pageList.TotalCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private string GetWhere()
    {
        StringBuilder sb = new StringBuilder("1=1");
        sb.AppendFormat(" and Creator={0} and PlanState='0'", User.ID);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        }
        else
        {
            sb.AppendFormat("");
        }
        return sb.ToString();
    }
}