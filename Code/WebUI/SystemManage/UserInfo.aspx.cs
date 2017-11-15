using BLL.SystemManagement;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class SystemManage_UserInfo : System.Web.UI.Page
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
                    QueryOneData();
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
        var writeMsg = "删除失败！";
        if (Request.Form["cbx_select"] != null)
        {
            if (UserInfoBLL.Delete(Request.Form["cbx_select"].ToString()))
            {
                writeMsg = "删除成功！";
            }
        }
        Response.Clear();
        Response.Write(writeMsg);
        Response.End();
    }
    private void Save()
    {
        bool result = false;
        int? id =null;
        if(!string.IsNullOrEmpty(Request.Form["id"]))
        { id = Convert.ToInt32(Request.Form["id"]); }
        var writeMsg = "操作失败！";
        var model = new UserInfo()
        {
            UserName = Request.Form["UserName"],
            Password = Request.Form["Password"],
            Mobile = Request.Form["Mobile"],
            Status = byte.Parse(Request.Form["Status"] ?? "0"),
            IsGeneralAviation = byte.Parse(Request.Form["IsGeneralAviation"] ?? "0")
        };
        if (!id.HasValue)//新增
        {
            model.CreateTime = DateTime.Now;
            result = UserInfoBLL.Add(model);
            if (result)
            {
                writeMsg = "增加成功！";
            }
            else
            {
                writeMsg = "增加失败！";
            }
        }
        else//编辑
        {
            model.ID = id.Value ;
            result = UserInfoBLL.Update(model);
            if (result)
            {
                writeMsg = "更新成功！";
            }
            else
            {
                writeMsg = "更新失败！";
            }
        }
        Response.Clear();
        Response.Write(writeMsg);
        Response.End();
    }


    #region 查询指定ID 的数据
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void QueryOneData()
    {
        var userid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        DataTable dt = UserInfoBLL.Get(userid);
        var strJSON = JsonHelper.CreateJsonOne(dt, false);
        Response.Clear();
        Response.Write(strJSON);
        Response.End();
    }
    #endregion

    #region 查询数据

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

        DataTable dsUser = UserInfoBLL.GetList("UserInfo", "*", "ID", size, page, false, false, strWhere);
        string strJSON = JsonHelper.CreateJsonParameters(dsUser, true, 1);
        //   strJSON= "{ \"rows\":[ { \"JSON_ID\":\"1\",\"JSON_UserName\":\"adads\",\"JSON_Password\":\"asdasdf\",\"JSON_Mobile\":\"sdfasdf\",\"JSON_Status\":\"0\",\"JSON_CreateTime\":\"2017-11-1\",\"JSON_IsGeneralAviation\":1,\"JSON_CompanyCode3\":\"222\"} ],\"total\":1}";
        Response.Write(strJSON);
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private string GetWhere()
    {
        StringBuilder sb = new StringBuilder("1=1");
        var searchType = Request.Form["search_type"] != null ? Request.Form["search_type"] : string.Empty;
        var searchValue = Request.Form["search_value"] != null ? Request.Form["search_value"] : string.Empty;

        if (!string.IsNullOrEmpty(searchType) && !string.IsNullOrEmpty(searchValue))
        {
            sb.AppendFormat(" and charindex('{0}',{1})>0", searchValue, searchType);
        }
        else
        {
            sb.AppendFormat("");
        }
        return sb.ToString();
    }

    #endregion
}