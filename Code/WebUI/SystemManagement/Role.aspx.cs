using BLL.SystemManagement;
using Model.EF;
using Model.SystemManagement;
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

public partial class SystemManage_Role : BasePage
{
    RoleBLL bll = new RoleBLL();
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
        Role model = null;
        if (!id.HasValue)//新增
        {  
             model = new Role()
        {
            RoleName = Request.Form["RoleName"],
            Description = Request.Form["Description"],
            IsAdmin = (Request.Form["IsAdmin"].ToString() == "1") ? true : false,
            CreateTime = DateTime.Now
        };
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
                model.RoleName = Request.Form["RoleName"];
                model.Description = Request.Form["Description"];
                model.IsAdmin = (Request.Form["IsAdmin"].ToString() == "1") ? true : false;
         
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

    private void SavePermission()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        var roleid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var oldRoleMenuList = bll.GetRoleMenuList(roleid);
        List<int> newRoleMenuList = new List<int>();
        var array = (Request.Form["newRoleMenus"] ?? "").Split(',');
        foreach (var item in array)
        {
            newRoleMenuList.Add(int.Parse(item));
        }
        var sameRoleMenuList = oldRoleMenuList.Intersect(newRoleMenuList);
        var addRoleMenuList = newRoleMenuList.Except(sameRoleMenuList);
        var removeRoleMenuList = oldRoleMenuList.Except(sameRoleMenuList);
        if (bll.SetRoleMenu(roleid, addRoleMenuList, removeRoleMenuList))
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
        var role = bll.Get(roleid);
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
        int pageCount = 0;
        int rowCount = 0;
        if (page < 1) return;
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
    private Expression<Func<Role, bool>> GetWhere()
    {
        Expression<Func<Role, bool>> predicate = PredicateBuilder.True<Role>();
        predicate = predicate.And(m => 1 == 1);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.RoleName == val);
        }
        return predicate;
    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "RoleCheck";
        }
    }
    #endregion
}