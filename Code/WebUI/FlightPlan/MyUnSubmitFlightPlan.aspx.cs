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
using ViewModel.FlightPlan;

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
                case "queryone1"://获取一条记录
                    GetData1();
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
            if (bool.Parse(Request.Form["IsTempFlightPlan"] ?? "false"))
            {
                entity.RepetPlanID = Guid.Empty.ToString();
                bll.AddFlightPlanTempOther(Request.Form["AirlineText"], Request.Form["CWorkText"], Request.Form["PWorkText"], Request.Form["HWorkText"], entity.FlightPlanID.ToString(), Request.Form["id"], ref airlineworkText);
            }
            else
            {
                bll.AddFlightPlanOther(Request.Form["MasterIDs"], entity.FlightPlanID.ToString(), Request.Form["id"], ref airlineworkText);
            }
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
                entity.GetEntitySearchPars<FlightPlan>(this.Context);
                entity.ModifyTime = DateTime.Now;
                if (bool.Parse(Request.Form["IsTempFlightPlan"] ?? "false"))
                {
                    bll.AddFlightPlanTempOther(Request.Form["AirlineText"], Request.Form["CWorkText"], Request.Form["PWorkText"], Request.Form["HWorkText"], entity.FlightPlanID.ToString(), Request.Form["id"], ref airlineworkText);
                }
                else
                    bll.AddFlightPlanOther(Request.Form["MasterIDs"], entity.FlightPlanID.ToString(), Request.Form["id"], ref airlineworkText);
                entity.AirlineWorkText = airlineworkText;
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
        var planid = Guid.Parse(Request.Form["id"]);
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
    /// 临时计划
    /// </summary>

    private void GetData1()
    {
        var planid = Request.Form["id"];
        FlightPlanVM model = new FlightPlanVM();
        var data = bll.Get(Guid.Parse(planid));
        if (data != null)
        {
            model.FillObject(data);
            var filemasterList = bll.GetFileFlightPlanMasterList(u => u.FlightPlanID.Equals(planid)).Select(u=>u.MasterID);
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