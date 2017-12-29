<%@ WebHandler Language="C#" Class="Loginout" %>

using System;
using System.Web;
using System.Web.Security;
public class Loginout : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (HttpContext.Current.Session != null)
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }
        HttpContext.Current.Response.Cookies.Clear();
        FormsAuthentication.SignOut();
        context.Response.Redirect("Default.aspx");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}