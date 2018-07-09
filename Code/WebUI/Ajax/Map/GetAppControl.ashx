<%@ WebHandler Language="C#" Class="GetAppControl" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Model.EF;

public class GetAppControl : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        DAL.Map.Map_AppControlDAL dal = new DAL.Map.Map_AppControlDAL();
        var appList = new List<App>();
        var list = dal.GetAppList();
        foreach (var l in list)
        {
            var app = new App();
            app.appName = l;
            app.location = dal.GetApp(l);
            appList.Add(app);
        }
        context.Response.Write(JsonConvert.SerializeObject(appList));
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public class App
    {
        public string appName;
        public List<Map_AppControl> location;
    }
}