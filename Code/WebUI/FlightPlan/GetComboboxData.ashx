<%@ WebHandler Language="C#" Class="GetComboboxData" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using BLL.BasicData;
using Model.EF;
using BLL.FlightPlan;
using System.Linq.Expressions;
using Untity;
public class GetComboboxData : IHttpHandler {
    
    FlightTaskBLL bll = new FlightTaskBLL();
    AircraftBLL all = new AircraftBLL();
    CompanyBLL cbll = new CompanyBLL();
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Params["type"] != null)
        {
            var strJSON = "";
            switch (context.Request.Params["type"])
            {
                case "1":
                   strJSON= GetAllFlightTask();
                    break;
                case "2":
                    strJSON = GetAllAircraftType();
                    break;
                case "3":
                    strJSON = GetAllCompany();
                    break;
            }
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
    private string GetAllCompany()
    {
        Expression<Func<Company, bool>> predicate = PredicateBuilder.True<Company>();
        predicate = predicate.And(m => m.Catalog ==1);
        List<Company> list = cbll.GetList(predicate);
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id = item.CompanyCode3, text = item.CompanyName });
        }
        return JsonConvert.SerializeObject(arr);
    }
    private string GetAllAircraftType()
    {
        var userInfo = UserLoginService.Instance.GetUser();
        var list = new List<Aircraft>();    
        if (userInfo != null)
        {
            Expression<Func<Aircraft, bool>> predicate = PredicateBuilder.True<Aircraft>();
            predicate = predicate.And(m => m.CompanyCode3 == userInfo.CompanyCode3);
            list = all.GetList(predicate);
        }
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id = item.AcfType, text = item.AcfType });
        }
        return JsonConvert.SerializeObject(arr);
    }

}