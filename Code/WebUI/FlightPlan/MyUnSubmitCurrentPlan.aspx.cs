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
using ViewModel.FlightPlan;

public partial class FlightPlan_MyUnSubmitCurrentPlan : BasePage
{
    CurrentPlanBLL currPlanBll = new CurrentPlanBLL();
    RepetitivePlanBLL rpbll = new RepetitivePlanBLL();
    public override string PageRightCode
    {
        get
        {
            return "MyUnSubmitCurrentPlanCheck";
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
                case "queryone1"://获取一条记录
                    GetData1();
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
        var planid = Guid.Parse(Request.Form["id"]);

        try
        {
            CurrentFlightPlanVM model = new CurrentFlightPlanVM() {
                AircraftNum=int.Parse(Request.Form["AircraftNum"]),
                //ContractWay= Request.Form["ContractWay"],
                //Pilot = Request.Form["Pilot"],
                ActualStartTime =DateTime.Parse(Request.Form["ActualStartTime"]),
                ActualEndTime = DateTime.Parse(Request.Form["ActualEndTime"])
            };
            currPlanBll.Submit(planid,User.ID,User.UserName, User.RoleName.First(), model);
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
        var planid = Guid.Parse(Request.Form["id"]);
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
    /// 临时计划
    /// </summary>

    private void GetData1()
    {
        var planid = Request.Form["id"];
        CurrentFlightPlanVM model = new CurrentFlightPlanVM();
        var data = currPlanBll.Get(Guid.Parse(planid));
        if (data != null)
        {
            model.FillObject(data);
            var filemasterList = currPlanBll.GetFileCurrentPlanMasterList(u => u.CurrentPlanID.Equals(planid)).Select(u => u.MasterID);
            var masterList = rpbll.GetFileMasterList(u => filemasterList.Contains(u.ID));
            var airlinelist = masterList.Where(u => u.WorkType.Equals("airline"));
            if (airlinelist != null && airlinelist.Any())
            {
                foreach (var item in airlinelist)
                {
                    var airlinevm = new AirlineVM();
                    airlinevm.FlyHeight = item.FlyHeight;
                    var detailList = rpbll.GetFileDetailList(o => o.MasterID == item.ID);
                    airlinevm.pointList.AddRange(detailList.Select(u => new PointVM()
                    {
                        Name = u.PointName,
                        LatLong = (!string.IsNullOrEmpty(u.Latitude) && !string.IsNullOrEmpty(u.Longitude)) ? string.Concat("N", SpecialFunctions.ConvertDigitalToDegrees(u.Latitude), "E", SpecialFunctions.ConvertDigitalToDegrees(u.Longitude)) : ""
                    }));
                    if (airlinevm.pointList.Count > model.airLineMaxCol)
                    {
                        model.airLineMaxCol = airlinevm.pointList.Count;
                    }
                    model.airlineList.Add(airlinevm);
                    model.airlineworkList.Add(item.ID);
                };
            }
            var worklist = masterList.Where(u => u.WorkType.Equals("circle") || u.WorkType.Equals("airlinelr") || u.WorkType.Equals("area"));
            if (worklist != null && worklist.Any())
            {
                foreach (var item in worklist)
                {
                    var workvm = new WorkVM();
                    workvm.FlyHeight = item.FlyHeight;
                    workvm.Raidus = (item.RaidusMile ?? 0).ToString();
                    var detailList = rpbll.GetFileDetailList(o => o.MasterID == item.ID);
                    workvm.pointList.AddRange(detailList.Select(u => new PointVM()
                    {
                        Name = u.PointName,
                        LatLong = (!string.IsNullOrEmpty(u.Latitude) && !string.IsNullOrEmpty(u.Longitude)) ? string.Concat("N", SpecialFunctions.ConvertDigitalToDegrees(u.Latitude), "E", SpecialFunctions.ConvertDigitalToDegrees(u.Longitude)) : ""
                    }));

                    switch (item.WorkType.ToLower())
                    {
                        case "circle":
                            model.cworkList.Add(workvm);
                            model.airlineworkList.Add(item.ID);
                            break;
                        case "airlinelr":
                            if (workvm.pointList.Count > model.hworkMaxCol)
                            {
                                model.hworkMaxCol = workvm.pointList.Count;
                            }
                            model.hworkList.Add(workvm);
                            model.airlineworkList.Add(item.ID);
                            break;
                        case "area":
                            if (workvm.pointList.Count > model.pworkMaxCol)
                            {
                                model.pworkMaxCol = workvm.pointList.Count;
                            }
                            model.pworkList.Add(workvm);
                            model.airlineworkList.Add(item.ID);
                            break;
                        default:
                            break;
                    }
                };
            }
        }
        Response.Write(JsonConvert.SerializeObject(model));
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
        var pageList = new List<vCurrentPlan>();
        try
        {
            pageList=currPlanBll.GetList(page, size, out pageCount, out rowCount, strWhere);
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
    private Expression<Func<vCurrentPlan, bool>> GetWhere()
    {
        Expression<Func<vCurrentPlan, bool>> predicate = PredicateBuilder.True<vCurrentPlan>();
        try
        {            
            var currDate = DateTime.Now.Date;
            predicate = predicate.And(m => m.CurrentFlightPlanID == null && DbFunctions.TruncateTime(m.SOBT) == currDate&&m.Creator1 == User.ID);

            if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
            {
                predicate = u => u.Code == Request.Form["search_value"];
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