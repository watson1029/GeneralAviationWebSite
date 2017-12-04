<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using Model;
using Model.EF;
using DAL.SystemManagement;
using Untity;
public class Handler : IHttpHandler
{
    private ResourceDAL dao=new ResourceDAL();
    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request["action"];
        switch (action)
        {
            case "update":

                break;
            case "delete":
                int id = Convert.ToInt16(context.Request["id"]);
                dao.Delete(id);
                context.Response.ContentType = "text/plain";
                context.Response.Write("资料删除成功");
                break;
            case "add":
                string title = context.Request["title"];
                string dealuser = context.Request["dealuser"];
                string resourcetype = context.Request["resourcetype"];
                string usefultime = context.Request["usefultime"];
                string filepath = "";
                HttpFileCollection files = context.Request.Files;
                if (files.Count > 0&&!context.Request.Files["file"].FileName.Equals(""))
                {
                    context.Request.Files["file"].SaveAs(context.Server.MapPath("~/UploadFile/") + context.Request.Files["file"].FileName);
                    filepath = context.Request.Files["file"].FileName;

                    Resource resource = new Resource();
                    resource.Title = title;
                    resource.DealUser = dealuser;
                    resource.ResourceType = Convert.ToInt16(resourcetype);
                    resource.UsefulTime = usefultime;
                    resource.SenderId = 123;
                    resource.FilePath = filepath;
                    dao.Add(resource);

                    context.Response.ContentType = "text/plain";
                    context.Response.Write("新增资料成功");
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("新增资料失败，请选择附件");
                }
                break;
            case "get":
                int page = Convert.ToInt16(context.Request["page"]);
                int rows = Convert.ToInt16(context.Request["rows"]);
                int type = Convert.ToInt16(context.Request["type"]);
                int status = Convert.ToInt16(context.Request["status"]);
                List<Resource> list = dao.GetList(type, status, page, rows);
                int count = dao.GetCount(type, status);
                var data = new { total = count, rows = list };
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                context.Response.ContentType = "text/plain";
                context.Response.Write(jss.Serialize(data));
                break;
            case "download":
                string file = context.Server.MapPath("~/UploadFile/") + context.Request["filepath"];
                if (System.IO.File.Exists(file))
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
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
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}