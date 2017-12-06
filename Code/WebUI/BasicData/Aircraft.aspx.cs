using BLL.BasicData;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Web.UI;
using Untity;

public partial class BasicData_Aircraft : Page
{
    AircraftBLL bll = new AircraftBLL();
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
                    Save();
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
            if (bll.Delete(Request.Form["cbx_select"].ToString())>0)
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
        var model = new Aircraft()
        {
            AircraftSign = int.Parse(Request.Form["AircraftSign"]),
            FuelCapacity = int.Parse(Request.Form["FuelCapacity"]),
            AcfType = Request.Form["AcfType"],
            Range = int.Parse(Request.Form["Range"]),
            AcfNo = Request.Form["AcfNo"], 
            ASdate = int.Parse(Request.Form["ASdate"]),
            AcfClass = Request.Form["AcfClass"],
            CruiseAltd = int.Parse(Request.Form["CruiseAltd"]),
            Manufacture = Request.Form["Manufacturer"],
            CruiseSpeed = int.Parse(Request.Form["CruiseSpeed"]),
            WakeTurbulance = Request.Form["WakeTurbulance"],
            MaxSpeed = int.Parse(Request.Form["MaxSpeed"]),
            FueledWeight = int.Parse(Request.Form["FueledWeight"]),
            MinSpeed = int.Parse(Request.Form["MinSpeed"]),
        };
        if (!id.HasValue)//新增
        {
            model.CreateTime = DateTime.Now;
            if (bll.Add(model)>0)
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            model.AircraftID = id.Value;
            if (bll.Update(model)>0)
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

    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var aircraftid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var aircraft = bll.Get(aircraftid);
        var strJSON = JsonConvert.SerializeObject(aircraft);
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
   


    private Expression<Func<Aircraft, bool>> GetWhere()
    {


        Expression<Func<Aircraft, bool>> predicate = PredicateBuilder.True<Aircraft>();
        predicate = predicate.And(m => 1 == 1);
        //   StringBuilder sb = new StringBuilder("1=1");
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = u => u.AircraftID == int.Parse(Request.Form["search_value"]);

            //  sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        }
        return predicate;
    }

}