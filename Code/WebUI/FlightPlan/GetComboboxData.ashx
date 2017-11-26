<%@ WebHandler Language="C#" Class="GetComboboxData" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Model.BasicData;
using BLL.BasicData;
public class GetComboboxData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Params["type"] != null)
        {
            switch (context.Request.Form["type"])
            {
                case "1":
                    GetAllFlightTask();
                    break;
                case "2":
                    GetAllAircraftType();
                    break;
                default:
                    break;
            }
            var strJSON = GetAllFlightTask();
            context.Response.Clear();
            context.Response.Write(strJSON);
            context.Response.ContentType = "application/json";
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private string GetAllFlightTask()
    {

        List<FlightTask> list = FlightTaskBLL.GetAllList();
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id = item.TaskCode, text = item.Abbreviation });
        }
        return JsonConvert.SerializeObject(arr);
    }
    private string GetAllAircraftType()
    {

        List<FlightTask> list = FlightTaskBLL.GetAllList();
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id = item.TaskCode, text = item.Abbreviation });
        }
        return JsonConvert.SerializeObject(arr);
    }
}