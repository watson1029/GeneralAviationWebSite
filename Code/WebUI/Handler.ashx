<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using Model;
using DAL.SystemManagement;
using Untity;
public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request["action"];
        switch (action)
        {
            case "add":
                string title = context.Request["title"];
                string dealuser = context.Request["dealuser"];
                string resourcetype = context.Request["resourcetype"];
                string usefultime = context.Request["usefultime"];
                string filepath="";
                HttpFileCollection files = context.Request.Files;
                if (files.Count > 0)
                {
                    context.Request.Files["file"].SaveAs(context.Server.MapPath("~/UploadFile/") + context.Request.Files["file"].FileName);
                    filepath = context.Request.Files["file"].FileName;
                }
                Resource resource = new Resource();
                resource.Title = title;
                resource.DealUser = dealuser;
                resource.ResourceType = Convert.ToInt16(resourcetype);
                resource.UsefulTime = usefultime;
                resource.SenderId = 123;
                resource.FilePath = filepath;
                ResourceDAL.Add(resource);

                context.Response.ContentType = "text/plain";
                context.Response.Write("新增资料成功");
                break;
            case "get":
                int page = Convert.ToInt16(context.Request["page"]);
                int rows = Convert.ToInt16(context.Request["rows"]);
                int type = Convert.ToInt16(context.Request["type"]);
                int status = Convert.ToInt16(context.Request["status"]);
                List<Resource> list=ResourceDAL.GetList(1,1,page,rows);
                int count = ResourceDAL.GetCount(1,1);
                var data = new { total = count, rows = list };
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                context.Response.ContentType = "text/plain";
                context.Response.Write(jss.Serialize(data));
                break;
            case "download":
                string file=context.Server.MapPath("~/UploadFile/")+context.Request["filepath"];
                FileStream fs = new FileStream(file, FileMode.Open);
                if (fs != null)
                {
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=" + context.Request["filepath"]);
                    context.Response.BinaryWrite(bytes);
                    context.Response.Flush();
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("下载失败");
                }
                break;
        }

    }
    public void AddResource()
    {

    }
    public void Upload()
    {

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}