<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
public class Upload : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            HttpPostedFile httpfile = context.Request.Files["Filedata"];
            var filePath =HttpContext.Current.Server.UrlDecode(context.Request["filePath"]);
            if (httpfile != null)
            {
                string newFileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(httpfile.FileName);
                var savePath = HttpContext.Current.Server.MapPath("~/") + filePath;
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                savePath += newFileName;
                httpfile.SaveAs(savePath);
                context.Response.Write(filePath + newFileName);

            }
            context.Response.Write("");

        }
        catch (Exception ex)
        {
            context.Response.Write("");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}