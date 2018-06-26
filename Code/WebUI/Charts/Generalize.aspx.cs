using BLL.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Charts;
using BLL.FlightPlan;
using Newtonsoft.Json;

public partial class Charts_Generalize : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string GetData()
    {
        MenuBLL menuBll = new MenuBLL();
        RepetitivePlanBLL repetBll = new RepetitivePlanBLL();
        FlightPlanBLL flyBll = new FlightPlanBLL();
        var data = new GeneralizeModel();
        //获取长期计划提交数量
        if (menuBll.JudgeMenuRole(User.ID, "MyUnSubmitRepetPlanCheck"))
        {
            data.NameItem.Add(GeneralizeModel.MyUnSubmitRepetPlanCheck);
            data.ColorItem.Add(GeneralizeModel.usrpColor);
            var pie = new GeneralizeData();
            pie.name = GeneralizeModel.MyUnSubmitRepetPlanCheck;
            foreach (DateTime t in data.TimeParse)
            {
                pie.data.Add(repetBll.GetRepetSubmitNum(User.ID, t, t.AddDays(7)));
            }
            data.GeneralizeData.Add(pie);
        }
        //获取长期计划审批数量
        if (menuBll.JudgeMenuRole(User.ID, "MyAuditRepetPlanCheck"))
        {
            data.NameItem.Add(GeneralizeModel.MyAuditRepetPlanCheck);
            data.ColorItem.Add(GeneralizeModel.arpColor);
            var pie = new GeneralizeData();
            pie.name = GeneralizeModel.MyAuditRepetPlanCheck;
            foreach (DateTime t in data.TimeParse)
            {
                pie.data.Add(repetBll.GetRepetAuditNum(User.ID, t, t.AddDays(7)));
            }
            data.GeneralizeData.Add(pie);
        }
        //获取飞行计划提交数量
        if (menuBll.JudgeMenuRole(User.ID, "MyUnSubmitFlightPlanCheck"))
        {
            data.NameItem.Add(GeneralizeModel.MyUnSubmitFlightPlanCheck);
            data.ColorItem.Add(GeneralizeModel.usfpColor);
            var pie = new GeneralizeData();
            pie.name = GeneralizeModel.MyUnSubmitFlightPlanCheck;
            foreach (DateTime t in data.TimeParse)
            {
                pie.data.Add(flyBll.GetFlySubmitNum(User.ID, t, t.AddDays(7)));
            }
            data.GeneralizeData.Add(pie);
        }
        //获取飞行计划审批数量
        if (menuBll.JudgeMenuRole(User.ID, "MyAuditFlightPlanCheck"))
        {
            data.NameItem.Add(GeneralizeModel.MyAuditFlightPlanCheck);
            data.ColorItem.Add(GeneralizeModel.afpColor);
            var pie = new GeneralizeData();
            pie.name = GeneralizeModel.MyAuditFlightPlanCheck;
            foreach (DateTime t in data.TimeParse)
            {
                pie.data.Add(flyBll.GetFlyAuditNum(User.ID, t, t.AddDays(7)));
            }
            data.GeneralizeData.Add(pie);
        }
        return JsonConvert.SerializeObject(data);
    }
}