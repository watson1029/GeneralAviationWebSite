<%@ WebHandler Language="C#" Class="GetMenuID" %>

using System;
using System.Web;

public class GetMenuID : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}