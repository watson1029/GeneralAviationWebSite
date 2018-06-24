using BLL.FlightPlan;
using System;
using System.Text;
using Untity;
using Model.EF;
using System.Linq.Expressions;
using Newtonsoft.Json;
using DAL.FlightPlan;
using System.Linq;
using ViewModel.FlightPlan;

public partial class FlightPlan_MySubmitRepetPlan :BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
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
                case "getinstance":
                    GetAllNodeInstance();
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
        //string orderField = sort.Replace("JSON_", "");
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
        predicate = predicate.And(m => m.PlanState != "0" && m.Creator == User.ID);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.Code == val);
        }
        return predicate;
    }
   
    private void GetData()
    {
        var id = Guid.Parse(Request.Form["id"]);
        RepetitivePlanVM model = new RepetitivePlanVM();
        var data = bll.Get(id);
        if (data != null)
        {
            model.FillObject(data);
            var airportList = bll.GetRepetitivePlanAirport(id);
            if (airportList != null && airportList.Any())
            {
                model.airportList.AddRange(airportList.Select(u => new AirportVM()
                {
                    Name = u.Name,
                    Code4 = u.Code4,
                    LatLong = (!string.IsNullOrEmpty(u.Latitude) && !string.IsNullOrEmpty(u.Longitude)) ? string.Concat("N", SpecialFunctions.ConvertDigitalToDegrees(u.Latitude), "E", SpecialFunctions.ConvertDigitalToDegrees(u.Longitude)) : ""
                }));
            }
            var masterList = bll.GetFileMasterList(u => u.RepetPlanID.Equals(id));
            var airlinelist = masterList.Where(u => u.WorkType.Equals("airline"));
            if (airlinelist != null && airlinelist.Any())
            {
                foreach (var item in airlinelist)
                {
                    var airlinevm = new AirlineVM();
                    airlinevm.FlyHeight = item.FlyHeight;
                    var detailList = bll.GetFileDetailList(o => o.MasterID == item.ID && o.RepetPlanID.Equals(id));
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
                    var detailList = bll.GetFileDetailList(o => o.MasterID == item.ID && o.RepetPlanID.Equals(id));
                    workvm.pointList.AddRange(detailList.Select(u => new PointVM()
                    {
                        Name = u.PointName,
                        LatLong = (!string.IsNullOrEmpty(u.Latitude) && !string.IsNullOrEmpty(u.Longitude)) ? string.Concat("N", SpecialFunctions.ConvertDigitalToDegrees(u.Latitude), "E", SpecialFunctions.ConvertDigitalToDegrees(u.Longitude)) : ""
                    }));

                    switch (item.WorkType.ToLower())
                    {
                        case "circle":
                            model.cworkList.Add(workvm);
                            break;
                        case "airlinelr":
                            if (workvm.pointList.Count > model.hworkMaxCol)
                            {
                                model.hworkMaxCol = workvm.pointList.Count;
                            }
                            model.hworkList.Add(workvm);
                            break;
                        case "area":
                            if (workvm.pointList.Count > model.pworkMaxCol)
                            {
                                model.pworkMaxCol = workvm.pointList.Count;
                            }
                            model.pworkList.Add(workvm);
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
    private void GetAllNodeInstance()
    {
        var planid = Guid.Parse(Request.Form["id"]);
        var list = insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.RepetitivePlan).Where(u => u.ActorID != User.ID).ToList();
       var strJSON = Serializer.JsonDate(new { rows = list, total = list.Count });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    
    }

    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "MySubmitRepetPlanCheck";
        }
    }
    #endregion
}