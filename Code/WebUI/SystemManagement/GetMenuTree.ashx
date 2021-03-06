﻿<%@ WebHandler Language="C#" Class="GetMenuTree" %>

using BLL.SystemManagement;
using Model.SystemManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Untity;
public class GetMenuTree : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        var roleid = context.Request.QueryString["id"] != null ? Convert.ToInt32(context.Request.QueryString["id"]) : 0;
       var strJSON= GetRoleTree(roleid);
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
    private string  GetRoleTree(int roleid)
    {
        RoleBLL bll = new RoleBLL();
        MenuBLL menubll = new MenuBLL();
        var model = bll.Get(roleid);
        var treeNodeList = new List<TreeNode>();
        if (model != null)
        {
            treeNodeList = menubll.CreateMenuTree(null, roleid, model.IsAdmin);
        }
        return JsonConvert.SerializeObject(treeNodeList);

    }

}