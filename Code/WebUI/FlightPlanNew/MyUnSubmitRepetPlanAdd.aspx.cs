﻿using BLL.FlightPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_MyUnSubmitRepetPlanAdd : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "getplancode":
                    GetPlanCode();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid = Request.Form["id"] != null ? Guid.Parse(Request.Form["id"]) : Guid.NewGuid();
        var plan = bll.Get(planid);
        var strJSON = "";
        if (plan != null)
        {
            strJSON = JsonConvert.SerializeObject(plan);
        }

        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void GetPlanCode()
    {
        Response.Clear();
        Response.Write(OrderHelper.GenerateId(OrderTypeEnum.RP, User.CompanyCode3));
        Response.ContentType = "text/plain";
        Response.End();
    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "MyUnSubmitRepetPlanNewCheck";
        }
    }
    #endregion
}