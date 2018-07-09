<%@ WebHandler Language="C#" Class="GetAreaControl" %>

using System;
using System.Web;
using System.Collections.Generic;
using Model.EF;
using Newtonsoft.Json;

public class GetAreaControl : IHttpHandler {

    public void ProcessRequest(HttpContext context) {
        context.Response.ContentType = "text/plain";
        DAL.Map.Map_AreaControlDAL dal = new DAL.Map.Map_AreaControlDAL();
        var areaList = new List<Area>();
        var list = dal.GetAreaList();
        foreach (var l in list)
        {
            var area = new Area();
            area.areaName = l;
            area.location = dal.GetArea(l);
            areaList.Add(area);
        }
        context.Response.Write(JsonConvert.SerializeObject(areaList));
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public class Area
    {
        public string areaName;
        public List<Map_AreaControl> location;
    }
}