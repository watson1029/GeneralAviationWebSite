<%@ WebHandler Language="C#" Class="GetCurrentPlanData" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class GetCurrentPlanData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        BLL.FlightPlan.SuperMapBLL bll = new BLL.FlightPlan.SuperMapBLL();
        string keyValue = context.Request["keyValue"];
        var map = bll.GetCurrentMapData(keyValue);
        context.Response.Write(JsonConvert.SerializeObject(map));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}