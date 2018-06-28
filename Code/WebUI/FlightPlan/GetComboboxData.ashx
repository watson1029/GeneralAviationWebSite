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
    RepetitivePlanBLL rbll = new RepetitivePlanBLL();
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
                case "4":
                    strJSON = GetAllRepPlanCode();
                    break;
                case "5":
                    strJSON = GetAllAirlineWork(context.Request.Params["id"]);
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
            arr.Add(new { id = item.TaskCode, text = item.Description });
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
    private string GetAllRepPlanCode()
    {
        var userInfo = UserLoginService.Instance.GetUser();
        var list = new List<RepetitivePlan>();
        if (userInfo != null)
        {
            Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
            predicate = predicate.And(u => u.PlanState == "end" && u.Creator == userInfo.ID);
            list = rbll.GetList(predicate);
        }
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id =item.RepetPlanID.ToString(), text = item.Code });
        }
        return JsonConvert.SerializeObject(arr);
    }
    private string GetAllAirlineWork(string id)
    {
        var masterlist = rbll.GetFileMasterList(u => u.RepetPlanID.Equals(id));
        ArrayList arr = new ArrayList();
        foreach (var item in masterlist)
        {
            arr.Add(new { id = item.ID, text = item.LineDescript });
        }
        return JsonConvert.SerializeObject(arr);
    }
}