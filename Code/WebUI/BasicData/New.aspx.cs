using BLL.BasicData;
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

public partial class BasicData_New : BasePage
{
    NewBLL bll = new NewBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryNewData();
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

    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryNewData()
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
    private Expression<Func<News, bool>> GetWhere()
    {

        Expression<Func<News, bool>> predicate = PredicateBuilder.True<News>();
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = u => u.NewTitle.Contains(val);
        }
        return predicate;
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
        News model = null;
        if (!id.HasValue)//新增
        {
            model = new News();
        //   model.GetEntitySearchPars<News>(this.Context);
            model.Author = "";
            model.IsDelete = false;
            model.IsTop = byte.Parse(Request.Form["IsTop"]);
            model.Sort =int.Parse(Request.Form["Sort"]);
            model.NewTitle = Request.Form["NewTitle"];
            model.Author = Request.Form["Author"];
            model.CreateUser = User.UserName;
            model.CreateTime = DateTime.Now;
            model.NewContent = Server.HtmlDecode(Request.Form["SummaryCode"]);
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
                //model.GetEntitySearchPars<News>(this.Context);
                model.IsTop = byte.Parse(Request.Form["IsTop"]);
                model.Sort = int.Parse(Request.Form["Sort"]);
                model.NewTitle = Request.Form["NewTitle"];
                model.Author = Request.Form["Author"];
                model.NewContent = Server.HtmlDecode(Request.Form["SummaryCode"]);
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
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var newid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = bll.Get(newid);
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
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "NewsCheck";
        }
    }
    #endregion
}