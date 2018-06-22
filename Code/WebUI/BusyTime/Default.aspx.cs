using BLL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class BusyTime_Default : BasePage
{
    private BusyTimeBLL bll = new BusyTimeBLL();
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
                default:
                    break;
            }
        }
    }

    private void Save()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        DateTime date = DateTime.MinValue;
        DateTime.TryParse(Request.Form["BusyDate"], out date);
        BusyTime model = null;
        if (!bll.IsHash(date))//新增
        {
            model = new BusyTime()
            {
                BusyDate = date,
                BusyBeginTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(Request.Form["BusyBeginTime"].Split(':')[0]), int.Parse(Request.Form["BusyBeginTime"].Split(':')[1]), 0),
                BusyEndTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(Request.Form["BusyEndTime"].Split(':')[0]), int.Parse(Request.Form["BusyEndTime"].Split(':')[1]), 0),
            };
            if (bll.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            model = bll.Get(date);
            if (model != null)
            {
                model.BusyBeginTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(Request.Form["BusyBeginTime"].Split(':')[0]), int.Parse(Request.Form["BusyBeginTime"].Split(':')[1]), 0);
                model.BusyEndTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(Request.Form["BusyEndTime"].Split(':')[0]), int.Parse(Request.Form["BusyEndTime"].Split(':')[1]), 0);

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

    /// <summary>
    /// 获取指定Date的数据
    /// </summary>
    private void GetData()
    {
        int id = int.Parse(Request.Form["id"]);
        var model = bll.Get(id);
        var strJSON = JsonConvert.SerializeObject(model);
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
    private Expression<Func<BusyTime, bool>> GetWhere()
    {
        Expression<Func<BusyTime, bool>> predicate = PredicateBuilder.True<BusyTime>();
        predicate = predicate.And(m => 1 == 1);
        //if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        //{
        //    var val = Request.Form["search_value"].Trim();
        //    predicate = predicate.And(m => m.UserName == val);

        //    //  sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        //}
        return predicate;
    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "BusyTimeCheck";
        }
    }
    #endregion
}