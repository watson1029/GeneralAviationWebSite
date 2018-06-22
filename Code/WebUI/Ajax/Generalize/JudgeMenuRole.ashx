<%@ WebHandler Language="C#" Class="JudgeMenuRole" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL.SystemManagement;
using Newtonsoft.Json;

public class JudgeMenuRole : IHttpHandler, IRequiresSessionState {

    public void ProcessRequest (HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        bool result = new MenuBLL().JudgeMenuRole(UserLoginService.Instance.GetUser().ID, context.Request.Form["menuCode"]);
        context.Response.Write(JsonConvert.SerializeObject(result));
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}