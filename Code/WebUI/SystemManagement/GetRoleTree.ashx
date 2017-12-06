<%@ WebHandler Language="C#" Class="GetRoleTree" %>

using System;
using System.Web;
using BLL.SystemManagement;
using Model.SystemManagement;
using System.Collections.Generic;
using Newtonsoft.Json;
public class GetRoleTree : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        var userid = context.Request.QueryString["id"] != null ? Convert.ToInt32(context.Request.QueryString["id"]) : 0;
        var strJSON = GetRole(userid);
        context.Response.Clear();
        context.Response.Write(strJSON);
        context.Response.ContentType = "application/json";
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private string GetRole(int userid)
    {
        UserInfoBLL ubll=new UserInfoBLL();
        RoleBLL bll = new RoleBLL();
        var model = ubll.Get(userid);
        var treeNodeList = new List<TreeNode>();
        if (model != null)
        {
            treeNodeList = bll.CreateRoleTree(userid);
        }
        return JsonConvert.SerializeObject(treeNodeList);

    }
}