using BLL.SystemManagement;
using Model.SystemManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class SystemManage_Role : BasePage
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
                    GetData();
                    break;
                case "submit":
                    Save();
                    break;
                case "del":
                    Delete();
                    break;
                case "setrole":
                    SavePermission();
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
            if (RoleBLL.Delete(Request.Form["cbx_select"].ToString()))
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
        var model = new Role()
        {
            RoleName = Request.Form["RoleName"],
            Description = Request.Form["Description"],
            IsAdmin = (Request.Form["IsAdmin"].ToString() == "1") ? true : false
        };
        if (!id.HasValue)//新增
        {
            model.CreateTime = DateTime.Now;
            if (RoleBLL.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            model.ID = id.Value;
            if (RoleBLL.Update(model))
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

    private void SavePermission()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        var roleid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var oldRoleMenuList = RoleBLL.GetRoleMenuList(string.Format("RoleID={0}", roleid));
        List<int> newRoleMenuList = new List<int>();
        var array = (Request.Form["newRoleMenus"] ?? "").Split(',');
        foreach (var item in array)
        {
            newRoleMenuList.Add(int.Parse(item));
        }
        var sameRoleMenuList = oldRoleMenuList.Intersect(newRoleMenuList);
        var addRoleMenuList = newRoleMenuList.Except(sameRoleMenuList);
        var removeRoleMenuList = oldRoleMenuList.Except(sameRoleMenuList);
        if (RoleBLL.SetRoleMenu(roleid, addRoleMenuList, removeRoleMenuList))
        {
            result.IsSuccess = true;
            result.Msg = "保存成功！";
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
        var roleid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var role = RoleBLL.Get(roleid);
        var strJSON = JsonConvert.SerializeObject(role);
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
        string orderField = sort.Replace("JSON_", "");
        string strWhere = GetWhere();
        var pageList = RoleBLL.GetList(size, page, strWhere);

        var strJSON = Serializer.JsonDate(new { rows = pageList, total = pageList.TotalCount });
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
        StringBuilder sb = new StringBuilder("1=1");
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        }
        else
        {
            sb.AppendFormat("");
        }
        return sb.ToString();
    }

}