<%@ WebHandler Language="C#" Class="GetRepetPlanData" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class GetRepetPlanData : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        BLL.FlightPlan.SuperMapBLL bll = new BLL.FlightPlan.SuperMapBLL();
        string keyValue = context.Request["keyValue"];
        var map = bll.GetRepetMapData(keyValue);
        context.Response.Write(JsonConvert.SerializeObject(map));
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}