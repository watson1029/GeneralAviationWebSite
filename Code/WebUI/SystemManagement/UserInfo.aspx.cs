using BLL.SystemManagement;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Linq;
using Untity;
using System.Collections.Generic;
using System.Linq.Expressions;

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
                case "setrole":
                    SaveUserRole();
                    break;
                case "savepwd":
                    SavePwd();
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

    private void SavePwd()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        if (!Request.Form["NewPassword"].Trim().Equals(Request.Form["ComfirmPassword"].Trim()))
        {
            result.Msg = "新密码不一致！";

        }
        else
        {
            var id = Convert.ToInt32(Request.Form["id"]);
            var model = userBll.Get(id);
            if (model != null)
            {
                if (model.Password == CryptTools.HashPassword(Request.Form["OldPassword"]))
                {
                    model.Password = CryptTools.HashPassword(Request.Form["ComfirmPassword"]);
                    if (userBll.Update(model))
                    {
                        result.IsSuccess = true;
                        result.Msg = "更新成功！";
                    }
                }
                else
                {
                    result.Msg = "旧密码不正确！";
                }
            }
        }
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
        UserInfo model = null;
        if (!id.HasValue)//新增
        {
            var entiy = userBll.Get(Request.Form["UserName"]);
            if (entiy != null)
            {
                result.Msg = "用户名存在！";
            }
            else
            {
                model = new UserInfo()
           {
               UserName = Request.Form["UserName"],
               Password = CryptTools.HashPassword(Request.Form["Password"]),
               Mobile = Request.Form["Mobile"],
               Status = byte.Parse(Request.Form["Status"] ?? "0"),
               IsGeneralAviation = byte.Parse(Request.Form["IsGeneralAviation"]),
               CompanyCode3 = byte.Parse(Request.Form["IsGeneralAviation"]) == 1 ? Request.Form["CompanyCode3"] : "",
               CreateTime = DateTime.Now
           };
                if (userBll.Add(model))
                {
                    result.IsSuccess = true;
                    result.Msg = "增加成功！";
                }
            }
        }
        else//编辑
        {
            model = userBll.Get(id.Value);
            if (model != null)
            {
                model.UserName = Request.Form["UserName"];
                //  model.Password = CryptTools.HashPassword(Request.Form["Password"]);
                model.Mobile = Request.Form["Mobile"];
                model.Status = byte.Parse(Request.Form["Status"] ?? "0");
                model.IsGeneralAviation = byte.Parse(Request.Form["IsGeneralAviation"] ?? "0");
                model.CompanyCode3 = model.IsGeneralAviation == 1 ? Request.Form["CompanyCode3"] : "";

                if (userBll.Update(model))
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

        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        string orderField = sort.Replace("JSON_", "");
        var strWhere = GetWhere();

        var pageList = userBll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private Expression<Func<UserInfo, bool>> GetWhere()
    {


        Expression<Func<UserInfo, bool>> predicate = PredicateBuilder.True<UserInfo>();
        predicate = predicate.And(m => 1 == 1);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.UserName == val);

            //  sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        }
        return predicate;
    }
    private void SaveUserRole()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        var userid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var oldUserRoleList = userBll.GetUserRoleList(userid);
        List<int> newUserRoleList = new List<int>();
        var array = (Request.Form["newUserRoles"] ?? "").Split(',');
        foreach (var item in array)
        {
            newUserRoleList.Add(int.Parse(item));
        }
        var sameUserRoleList = oldUserRoleList.Intersect(newUserRoleList);
        var addUserRoleList = newUserRoleList.Except(sameUserRoleList);
        var removeUserRoleList = oldUserRoleList.Except(sameUserRoleList);
        if (userBll.SetUserRole(userid, addUserRoleList, removeUserRoleList))
        {
            result.IsSuccess = true;
            result.Msg = "保存成功！";
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "UserInfoCheck";
        }
    }
    #endregion
}