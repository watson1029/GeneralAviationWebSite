using BLL.FlightPlan;
using DAL.FlightPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;
using System.Linq.Expressions;
using Model.EF;
using System.IO;
using System.Data;
using System.Data.Entity;

public partial class FlightPlan_MyUnSubmitCurrentPlan : BasePage
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
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "submit":
                    Submit();
                    break;
                default:
                    break;
            }
        }
    }

    private void Submit()
    {
        AjaxResult result = new AjaxResult();        
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;

        try
        {
            currPlanBll.Submit(planid,User.ID,User.UserName);
            result.IsSuccess = true;
            result.Msg = "提交成功！";
        }
        catch(Exception ex) 
        {
            result.IsSuccess = false;
            result.Msg = "提交失败！"+ex.Message; 
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
   /**
    public void Save(){
        AjaxResult result = new AjaxResult();
        int id = 0;
        if (!string.IsNullOrEmpty(Request.Form["id"]))
        { id = Convert.ToInt32(Request.Form["id"]); }

        try
        {
            var model = new Model.EF.FlightPlan();
            model.FlightPlanID = id;
            model.ActualStartTime = DateTime.Parse(Request.Form["ActualStartTime"]);
            model.ActualEndTime = DateTime.Parse(Request.Form["ActualEndTime"]);
            currPlanBll.Update(model, new string[] { "ActualStartTime", "ActualEndTime" });
            result.IsSuccess = true;
            result.Msg = "保存成功！";
        }
        catch(Exception ex)
        {
            result.IsSuccess = false;
            result.Msg = "保存失败！"+ex.Message;
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    */
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = currPlanBll.Get(planid);
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
        int pageCount = 0;
        int rowCount = 0;
        string orderField = sort.Replace("JSON_", "");
        var strWhere = GetWhere();
        var pageList = new List<V_CurrentPlan>();
        try
        {
            pageList = currPlanBll.GetList(page, size, out pageCount, out rowCount, strWhere);
        }
        catch (Exception ex)
        {

        }
        
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
        try
        {            
            var currDate = DateTime.Now.Date;
            predicate = predicate.And(m => m.CurrentFlightPlanID == null && DbFunctions.TruncateTime(m.SOBT) == currDate);

            if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
            {
                predicate = u => u.PlanCode == Request.Form["search_value"];
            }
        }
        catch(Exception ex)
        {
            
        }

        return predicate;
    }

    private string DownFile(string attachfile)
    {
        var localNewFileName = Path.GetFileName(attachfile);
        var localTargetCategory = Server.MapPath("~/Files/PJ/CurrentPlanTemp");
        if (string.IsNullOrEmpty(localNewFileName))
            throw new ApplicationException(string.Format("获取文件路径[{0}]中的文件名为空", attachfile));
        if (!Directory.Exists(localTargetCategory))
            Directory.CreateDirectory(localTargetCategory);

        var filePath = Path.Combine(localTargetCategory, localNewFileName);
        return filePath;
    }
}