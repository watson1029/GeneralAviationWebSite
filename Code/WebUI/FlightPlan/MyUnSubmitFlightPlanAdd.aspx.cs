using BLL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_MyUnSubmitFlightPlanAdd : BasePage
{
    FlightPlanBLL bll = new FlightPlanBLL();
    RepetitivePlanBLL rpbll = new RepetitivePlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {

                case "getplancode":
                    GetPlanCode();
                    break;
                 case "gerrpplan":
                    GetRepetPlanData();
                    break;
                default:
                    break;
            }
        }
        if (Request.QueryString["type"] != null)
        {
            switch (Request.QueryString["type"])
            {
              
                case "getallplancode":
                    GetAllRepPlanCode();
                    break;

                default:
                    break;
            }
        }
    }


    private void GetRepetPlanData()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = rpbll.Get(planid);
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
        Response.Write(OrderHelper.GenerateId(OrderTypeEnum.FP, User.CompanyCode3));
        Response.ContentType = "text/plain";
        Response.End();
    }

    private void  GetAllRepPlanCode()
    {
        List<RepetitivePlan> list = rpbll.GetList(u => u.PlanState == "end" && u.Creator == User.ID);
        ArrayList arr = new ArrayList();
        foreach (var item in list)
        {
            arr.Add(new { id = item.RepetPlanID, text = item.PlanCode });
        }
        Response.Clear();
        Response.Write(JsonConvert.SerializeObject(arr));
        Response.ContentType = "application/json";
        Response.End();
    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "MyUnSubmitFlightPlanCheck";
        }
    }
    #endregion
}