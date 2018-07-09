<%@ WebHandler Language="C#" Class="GetAirLine" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Model.EF;

public class GetAirLine : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        DAL.Map.Map_AirLineDAL dal = new DAL.Map.Map_AirLineDAL();
        var airlineList = new List<AirLine>();
        var list = dal.GetAirlineList();
        foreach (var l in list)
        {
            var airline = new AirLine();
            airline.airlineName = l;
            airline.location = dal.GetAirLine(l);
            airlineList.Add(airline);
        }
        context.Response.Write(JsonConvert.SerializeObject(airlineList));
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public class AirLine
    {
        public string airlineName;
        public List<Map_AirLine> location;
    }
}