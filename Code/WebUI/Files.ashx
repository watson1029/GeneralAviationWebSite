<%@ WebHandler Language="C#" Class="Files" %>

using System;
using System.Web;

public class Files : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        HttpFileCollection files = context.Request.Files;
        if (files.Count > 0)
        {
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                if (file.ContentLength > 0)
                {
                    string FullName = file.FileName;
                    file.SaveAs(context.Server.MapPath("~/UploadFile/") + FullName);
                }
            }
        }
        
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}