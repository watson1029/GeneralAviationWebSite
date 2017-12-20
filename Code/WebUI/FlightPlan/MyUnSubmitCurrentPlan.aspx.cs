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
                case "batchImport":
                    BatchImport();
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
            var model = new CurrentFlightPlan();
            model.FlightPlanID = planid;
            model.ActualStartTime = DateTime.Parse(Request.Form["ActualStartTime"]);
            model.ActualEndTime = DateTime.Parse(Request.Form["ActualEndTime"]);
            currPlanBll.Update(model, new string[] { "ActualStartTime", "ActualEndTime" });
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
            plan.WeekSchedule = plan.WeekSchedule.Replace("*", "");
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
    private Expression<Func<CurrentFlightPlan, bool>> GetWhere()
    {
        Expression<Func<CurrentFlightPlan, bool>> predicate = PredicateBuilder.True<CurrentFlightPlan>();
        var currDate = DateTime.Now.Date;
        predicate = predicate.And(m => m.PlanState == "0" && m.Creator == User.ID && m.EffectDate == currDate);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = u => u.PlanCode == Request.Form["search_value"];
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
    private void BatchImport()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = true;
        result.Msg = "操作成功！";
        try
        {
            #region 校验数据

            string filepath = Request.Params["PlanFilesPath"].Split(',')[0];
            string temppath = DownFile(filepath);
            DataTable dt = OfficeTools.GetDT(temppath);
            if (dt.Rows.Count > 100)
            {
                result.IsSuccess = false;
                result.Msg = "最多只能导入500条数据！";
                Response.Write(result.ToJsonString());
                Response.ContentType = "application/json";
                Response.End();
            }
            if (dt.Columns.Count != 12)
            {
                result.IsSuccess = false;
                result.Msg = "导入的文件模板不正确，请更新导入模板！";
                Response.Write(result.ToJsonString());
                Response.ContentType = "application/json";
                Response.End();
            }
            int length = 0;
            string baseerrormessage = "第{0}行错误,错误信息为{1}:";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowobj = dt.Rows[i];
                for (int j = 0; j < rowobj.ItemArray.Length; j++)
                {
                    var colobj = rowobj.ItemArray[j].ToString();

                    switch (j)
                    {
                        case 0:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "任务类型不能为空！"));
                            if (length > 3)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "任务类型不能超过3个字符！"));
                            break;
                        case 1:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器呼号不能为空！"));
                            if (length > 36)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器呼号不能超过36个字符！"));
                            break;
                        case 2:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "使用机型不能为空！"));
                            if (length > 8)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "使用机型不能超过8个字符！"));
                            break;
                        case 3:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航线走向和飞行高度不能为空！"));
                            if (length > 32)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器类型不能超过32个字符！"));
                            break;
                        case 4:
                            DateTime bdt = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out bdt))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "预计开始日期格式不正确！"));
                            break;                            
                        case 5:
                            DateTime edt = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out edt))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "预计结束日期格式不正确！"));
                            if (edt < Convert.ToDateTime(rowobj.ItemArray[j - 1]))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "预计结束日期不能小于预计开始日期！"));
                            break;                            
                        case 6:
                            TimeSpan bts = TimeSpan.MinValue;
                            colobj = Convert.ToDateTime(colobj).ToString("hh:mm:ss");
                            if (!string.IsNullOrEmpty(colobj) && !TimeSpan.TryParse(colobj, out bts))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "起飞时刻格式不正确！"));                            
                            break;
                        case 7:
                            TimeSpan ets = TimeSpan.MinValue;
                            colobj = Convert.ToDateTime(colobj).ToString("hh:mm:ss");
                            if (!string.IsNullOrEmpty(colobj) && !TimeSpan.TryParse(colobj, out ets))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "降落时刻格式不正确！"));                            
                            break;
                        case 8:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "起飞机场不能为空！"));
                            if (length > 4)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "起飞机场不能超过4个字符！"));
                            break;
                        case 9:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "降落机场不能为空！"));
                            if (length > 4)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "降落机场不能超过4个字符！"));
                            break;
                        case 10:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "周执行计划不能为空！"));
                            if (length > 7)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "周执行计划不能超过7个字符！"));
                            break;
                        case 11:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

                            if (length == 0)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "其他需要说明的事项不能为空！"));

                            if (length > 200)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "其他需要说明的事项不能超过200个字符！"));
                            break;
                    }
                }
            }

            #endregion
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowobj = dt.Rows[i];
                var model = new CurrentFlightPlan()
                {
                    FlightType = rowobj.ItemArray[0].ToString(),
                    AircraftType = rowobj.ItemArray[1].ToString(),
                    CallSign = rowobj.ItemArray[2].ToString(),
                    FlightDirHeight = rowobj.ItemArray[3].ToString(),
                    StartDate = DateTime.Parse(rowobj.ItemArray[4].ToString()),
                    EndDate = DateTime.Parse(rowobj.ItemArray[5].ToString()),
                    SOBT = TimeSpan.Parse(Convert.ToDateTime(rowobj.ItemArray[6]).ToString("hh:mm:ss")),
                    SIBT = TimeSpan.Parse(Convert.ToDateTime(rowobj.ItemArray[7]).ToString("hh:mm:ss")),
                    ADEP = rowobj.ItemArray[8].ToString(),
                    ADES = rowobj.ItemArray[9].ToString(),
                    WeekSchedule = rowobj.ItemArray[10].ToString(),
                    Remark = rowobj.ItemArray[11].ToString(),
                    PlanState = "0",
                    CompanyCode3 = User.CompanyCode3 ?? "",
                    CompanyName = User.CompanyName,
                    Creator = User.ID,
                    CreatorName = User.UserName,
                    ActorID = User.ID,
                    CreateTime = DateTime.Now,
                    ModifyTime = DateTime.Now,
                    PlanCode = OrderHelper.GenerateId("", User.CompanyCode3),
                    EffectDate = DateTime.Now.Date
                };
                currPlanBll.Add(model);

                if (result.IsSuccess)
                {
                    result.Msg = "导入成功！";
                }
            }
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Msg = ex.Message;
        }
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
}