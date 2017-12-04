<%@ WebHandler Language="C#" Class="GetComboboxData" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Model.BasicData;
using BLL.BasicData;
using Model.EF;
public class GetComboboxData : IHttpHandler {
    FlightTaskBLL bll = new FlightTaskBLL();
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
                case "3":
                    
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
        List<FlightTask> list = bll.GetList();
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id = item.TaskCode, text = item.Abbreviation });
        }
        return JsonConvert.SerializeObject(arr);
    }
    //private string GetAllCompany()
    //{
    //    List<Company> list = bll.GetList();
    //    ArrayList arr = new ArrayList();
    //    foreach (var item in list)
    //    {
    //        arr.Add(new { id = item.TaskCode, text = item.Abbreviation });
    //    }
    //    return JsonConvert.SerializeObject(arr);
    //}
    private string GetAllAircraftType()
    {

        List<FlightTask> list = bll.GetList();
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id = item.TaskCode, text = item.Abbreviation });
        }
        return JsonConvert.SerializeObject(arr);
    }
}