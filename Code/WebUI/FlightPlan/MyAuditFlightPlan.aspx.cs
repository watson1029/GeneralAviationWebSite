﻿using BLL.FlightPlan;
using DAL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_MyAuditFlightPlan : BasePage
{
    FlightPlanBLL bll = new FlightPlanBLL();
    WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
                    break;
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "auditsubmit":
                    AuditSubmit();
                    break;
                default:
                    break;
            }
        }
    }




    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryData()
    {
        int page = Convert.ToInt32(Request.Form["page"] ?? "0");
        int size = Convert.ToInt32(Request.Form["rows"] ?? "0");
        // string sort = Request.Form["sort"] ?? "";
        // string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        // string orderField = sort.Replace("JSON_", "");
        var strWhere = GetWhere();
        var pageList = bll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    /// 
    private Expression<Func<FlightPlan, bool>> GetWhere()
    {

        Expression<Func<FlightPlan, bool>> predicate = PredicateBuilder.True<FlightPlan>();
        predicate = predicate.And(m => m.ActorID == User.ID);
        predicate = predicate.And(m => m.Creator != User.ID);
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
          predicate = predicate.And(m => m.PlanCode == val);
        }

        return predicate;
    }
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = bll.Get(planid);
        var strJSON = JsonConvert.SerializeObject(plan);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void AuditSubmit()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "提交失败！";
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        if (Request.Form["Auditresult"] == "0")
        {
            insdal.Submit(planid, (int)TWFTypeEnum.FlightPlan, Request.Form["AuditComment"] ?? "", insdal.UpdateFlightPlan);
        }
        else
        {
            insdal.Terminate(planid, (int)TWFTypeEnum.FlightPlan, Request.Form["AuditComment"] ?? "", insdal.UpdateFlightPlan);
        }
        result.IsSuccess = true;
        result.Msg = "提交成功！";

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();


    }
    private void DownlodFile()
    {
        string fileName = "";
        string filePath = Server.MapPath("~/");
        FileStream fs = new FileStream(filePath, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}