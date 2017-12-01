using BLL.SystemManagement;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Text;
using Untity;

public partial class SystemManage_UserInfo : BasePage
{
    UserInfoBLL userBll = new UserInfoBLL();
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
            if (userBll.Delete(Request.Form["cbx_select"].ToString()))
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
        var model = new UserInfo()
        {
            UserName = Request.Form["UserName"],
            Password = CryptTools.HashPassword(Request.Form["Password"]),
            Mobile = Request.Form["Mobile"],
            Status = byte.Parse(Request.Form["Status"] ?? "0"),
            IsGeneralAviation = byte.Parse(Request.Form["IsGeneralAviation"] ?? "0")
        };
        if (!id.HasValue)//新增
        {
            model.CreateTime = DateTime.Now;
            if (userBll.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            model.ID = id.Value;
            if (userBll.Update(model))
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
        var userid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var userinfo = userBll.Get(userid);
        var strJSON = JsonConvert.SerializeObject(userinfo);
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
        int pageCount=0;
        int rowCount=0;
        if (page < 1) return;
        string orderField = sort.Replace("JSON_", "");
        string strWhere = GetWhere();
        Expression<System.Func<UserInfo, bool>> exp = u => u.UserName == "admin";
        var pageList = userBll.GetList(page,size, out pageCount, out rowCount, exp);
       var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
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

        Expression<System.Func<UserInfo, bool>> exp;
        StringBuilder sb = new StringBuilder("1=1");
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            exp = u => u.UserName == Request.Form["search_value"];

          //  sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        }
        else
        {
            sb.AppendFormat("");
        }
        return sb.ToString();
    }

}