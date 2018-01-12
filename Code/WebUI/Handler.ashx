<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using Model.EF;
using Model.SystemManagement;
using DAL.SystemManagement;
using DAL.FlightPlan;
using Untity;
using Newtonsoft.Json;
using DAL.BasicData;
using System.Linq;
public class Handler : IHttpHandler
{
    private ResourceDAL dao = new ResourceDAL();
    private RepetitivePlanDAL dd = new RepetitivePlanDAL();
    private CompanyDAL cDao = new CompanyDAL();
    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request["action"];
        switch (action)
        {
            case "update":
                update(context);
                break;
            case "delete":
                delete(context);
                break;
            case "add":
                add(context);
                break;
            case "get":
                get(context);
                break;
            case "download":
                download(context);
                break;
            case "getCompanies":
                getCompanies(context);
                break;
            case "collect2":
                collect2(context);
                break;
            case "collect":
                collect(context);
                break;
            case "dictionary":
                
                break;
        }

    }
    public void collect(HttpContext context)
    {
        List<TemplateClass4StatisticResult> ll;
        DateTime started = DateTime.Parse(context.Request["started"]);
        DateTime ended = DateTime.Parse(context.Request["ended"]);
        String strCompany = context.Request["company"];

        if (strCompany.IsNullOrEmpty() || strCompany.Equals("全部"))
        {
            ll = dd.getStatisticResult(started, ended);
        }
        else
        {
            Company company = cDao.Find(m => m.CompanyName == strCompany);
            ll = dd.getStatisticResult(started, ended, company.CompanyCode3);
        }
        List<string> list = new List<string>();
        List<string> category = new List<string>();
        List<Series> ss = new List<Series>();
        Series series = new Series();
        series.name = "长期飞行计划数量";
        series.type = "bar";
        series.itemStyle = new itemStyle
        {
            normal = new normal
            {
                areaStyle = new areaStyle
                {
                    type = "default"
                }
            }
        };
        if (ll.Count > 0)
        {
            foreach (var item in ll)
            {
                category.Add(item._field2);
                list.Add(item._field3.ToString());
            }
            series.data = list;
            ss.Add(series);
        }
        else
        {
            category.Add(strCompany);
            list.Add("0");
            series.data = list;
            ss.Add(series);
        }
        var result = new
        {
            category = category.ToArray(),
            series = ss.ToArray()
        };
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonConvert.SerializeObject(result));
    }
    public void collect2(HttpContext context)
    {
        List<TemplateClass4StatisticResult> ll;
        List<Company> companies=new List<Company>();
        DateTime start = DateTime.Parse(context.Request["started"]);
        DateTime end = DateTime.Parse(context.Request["ended"]);
        int timetype = Convert.ToInt16(context.Request["timetype"]);
        String strCompany = context.Request["company"];

        if (strCompany.IsNullOrEmpty() || strCompany.Equals("全部"))
        {
            ll = dd.getCollect(start, end,timetype);
            companies = cDao.FindList(m=>m.CompanyID,true);
        }
        else
        {
            Company company = cDao.Find(m => m.CompanyName == strCompany);
            ll = dd.getCollect(start, end, timetype, company.CompanyCode3);
            companies.Add(cDao.Find(m=>m.CompanyName==strCompany));
            
        }
        List<string> list = new List<string>();
        List<string> category = new List<string>();
        List<Series> ss = new List<Series>();
        List<string> legend = new List<string>();
        for (int i = 0; i < companies.Count; i++)
        {
            Series series = new Series();
            series.name = companies[i].CompanyName;
            series.type = "bar";
            series.data=new List<String>();
            series.itemStyle = new itemStyle
            {
                normal = new normal
                {
                    areaStyle = new areaStyle
                    {
                        type = "default"
                    }
                }
            };
            ss.Add(series);
            legend.Add(companies[i].CompanyName);
        }
        if (ll.Count > 0)
        {
            //ll.OrderBy(m => m._field1);
            foreach (var item in ll)
            {
                if (!category.Contains(item._field1))
                {
                    category.Add(item._field1);
                }
                for (int j = 0; j < ss.Count;j++ )
                {
                    if (ss[j].name.Equals(item._field2))
                    {
                        ss[j].data.Add(item._field3.ToString());
                    }
                    else
                    {
                        if (timetype == 3)
                        {
                            ss[j].data.Add("-");
                        }
                    }
                }
                //list.Add(item._field3.ToString());
            }
            //series.data = list;
            //ss.Add(series);
        }
        var result = new
        {
            category = category.ToArray(),
            series = ss.ToArray(),
            legend=legend.ToArray()
        };
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonConvert.SerializeObject(result));
    }
    /// <summary>
    /// 获取公司列表
    /// </summary>
    /// <param name="context"></param>
    public void getCompanies(HttpContext context)
    {
        List<Company> list=cDao.FindList(m=>m.CompanyName,true);
        List<Compobox> cbs = new List<Compobox>();
        Compobox c = new Compobox();
        c.id = 0;
        c.text = "全部";
        c.selected = true;
        cbs.Add(c);
        for (int i = 0; i < list.Count; i++)
        {
            Company cp=list[i];
            Compobox cb = new Compobox();
            cb.id = i+1;
            cb.text = cp.CompanyName;
            cb.desc = cp.CompanyCode3;
            cbs.Add(cb);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonConvert.SerializeObject(cbs));
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="context"></param>
    public void download(HttpContext context)
    {
        string file = context.Server.MapPath("~/")+context.Request["filepath"];
        if (System.IO.File.Exists(file))
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Substring(file.LastIndexOf("/")));
            context.Response.BinaryWrite(bytes);
            context.Response.Flush();
            context.Response.End();
        }
        else
        {
            context.Response.Write("下载失败");
        }
    }

    /// <summary>
    /// 获取资料
    /// </summary>
    /// <param name="context"></param>
    public void get(HttpContext context)
    {
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
    }
    /// <summary>
    /// 删除资料
    /// </summary>
    /// <param name="context"></param>
    public void delete(HttpContext context)
    {
        int id = Convert.ToInt16(context.Request["id"]);
        dao.DeleteResource(id);
        context.Response.ContentType = "text/plain";
        context.Response.Write("资料删除成功");
    }
    /// <summary>
    /// 新增资料
    /// </summary>
    /// <param name="context"></param>
    public void add(HttpContext context)
    {
        string title = context.Request["title"];
        string dealuser = context.Request["dealuser"];
        int resourcetype = Convert.ToInt16(context.Request["resourcetype"]);
        int status = Convert.ToInt16(context.Request["status"]);
        DateTime started = DateTime.Parse(context.Request["started"]);
        DateTime ended = DateTime.Parse(context.Request["ended"]);
        string filename = context.Request.Files["file"].FileName;
        string hou = filename.Substring(0, filename.IndexOf("."));
        string tt = filename.Substring(filename.IndexOf("."));
        if (context.Request.Files["file"].ContentLength > 0)
        {
            if (context.Request.Files["file"].ContentLength > 52428800)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("新增资料失败，附件太大了");
            }
            String time = Guid.NewGuid().ToString("N");
            context.Request.Files["file"].SaveAs(context.Server.MapPath("~/Files/Resource/") + filename);
            string filepath = "Files/Resource/"+ filename;

            Resource resource = new Resource();
            resource.Title = title;
            resource.DealUser = dealuser;
            resource.ResourceType = resourcetype;
            resource.UsefulTime = started.ToString("yyyy年MM月dd日") + "-" + ended.ToString("yyyy年MM月dd日");
            resource.SenderId = 123;
            resource.FilePath = filepath;
            resource.Created = DateTime.Now;
            resource.IsDeleted = 0;
            resource.Status = status;
            resource.Started = started;
            resource.Ended = ended;
            dao.AddResource(resource);

            context.Response.ContentType = "text/plain";
            context.Response.Write("新增资料成功");
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("新增资料失败，请选择附件");
        }
    }
    /// <summary>
    /// 更新资料信息
    /// </summary>
    /// <param name="context"></param>
    public void update(HttpContext context)
    {
        int id = Convert.ToInt16(context.Request["id"]);
        string title = context.Request["title"];
        string dealuser = context.Request["dealuser"];
        int resourcetype = Convert.ToInt16(context.Request["resourcetype"]);
        int status = Convert.ToInt16(context.Request["status"]);
        DateTime started = DateTime.Parse(context.Request["started"]);
        DateTime ended = DateTime.Parse(context.Request["ended"]);
        Resource resource = new Resource();
        if (context.Request.Files["file"].ContentLength > 0)
        {
            if (context.Request.Files["file"].ContentLength > 52428800)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("更新资料失败，附件太大了");
            }
            context.Request.Files["file"].SaveAs(context.Server.MapPath("~/Files/Resource/") + context.Request.Files["file"].FileName);
            resource.FilePath = "Files/Resource/"+context.Request.Files["file"].FileName;
        }

        resource.ID = id;
        resource.Title = title;
        resource.DealUser = dealuser;
        resource.ResourceType = resourcetype;
        resource.Status = status;
        resource.UsefulTime = started.ToString("yyyy年MM月dd日") + "-" + ended.ToString("yyyy年MM月dd日");
        resource.Started = started;
        resource.Ended = ended;
        dao.UpdateResource(resource);
        context.Response.ContentType = "text/plain";
        context.Response.Write("更新资料成功");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}