<%@ WebHandler Language="C#" Class="GetGeneralizeNum" %>

using System;
using System.Web;

public class GetGeneralizeNum : IHttpHandler {
    
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