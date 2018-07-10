using BLL.FlightPlan;
using BLL.SystemManagement;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class BackLog : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string GetUserDataJson()
    {
        var menuListJson = new MenuBLL().CreateMenuJson(User.ID);
        var currDate = DateTime.Now.Date;
        List<MenuStatis> StatisList = new List<MenuStatis>();
        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        List<RepetitivePlan> RepetitivePlanList = GetMenuStatisData(predicate);
        Expression<Func<FlightPlan, bool>> Fpredicate = PredicateBuilder.True<FlightPlan>();
        List<FlightPlan> FlightPlanList = GetFlightPlanData(Fpredicate);
        Expression<Func<vCurrentPlan, bool>> vcpredicate = PredicateBuilder.True<vCurrentPlan>();
        List<vCurrentPlan> VCurrentPlanList = GetCurrentPlanData(vcpredicate);
        if (menuListJson.Contains("MyUnSubmitRepetPlan.aspx")) //长期计划列表(待提交)
        {
            MenuStatis statis = new MenuStatis("待提交长期计划", "MyUnSubmitRepetPlan.aspx", 0, "Rpundo.jpg");
            statis.MenuPlanCount = RepetitivePlanList.Where(a => a.PlanState == "0" && a.Creator == User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyAuditRepetPlan.aspx"))//长期计划列表(待审核)
        {
            MenuStatis statis = new MenuStatis("待审核长期计划", "MyAuditRepetPlan.aspx", 0, "Psubmit.jpg");
            statis.MenuPlanCount = RepetitivePlanList.Where(a => a.ActorID == User.ID && a.Creator != User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyUnSubmitFlightPlan.aspx"))//飞行计划列表(待提交)
        {
            MenuStatis statis = new MenuStatis("待提交飞行计划", "MyUnSubmitFlightPlan.aspx", 0, "unsubmit.jpg");
            statis.MenuPlanCount = FlightPlanList.Where(m => m.PlanState == "0" && m.Creator == User.ID && DbFunctions.TruncateTime(m.SOBT) == DateTime.Now.Date.AddDays(1)).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyAuditFlightPlan.aspx"))//飞行计划列表(待审核)
        {
            MenuStatis statis = new MenuStatis("待审核飞行计划", "MyAuditFlightPlan.aspx", 0, "RpPlan.jpg");
            statis.MenuPlanCount = FlightPlanList.Where(m => m.ActorID == User.ID && m.Creator != User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyUnSubmitCurrentPlan.aspx"))
        {
            MenuStatis statis = new MenuStatis("待提交当日起飞申请", "MyUnSubmitCurrentPlan.aspx", 0, "uncurrent.jpg");
            //vcpredicate = vcpredicate.And(m => m.CurrentFlightPlanID == null && DbFunctions.TruncateTime(m.SOBT) == currDate);
            statis.MenuPlanCount = VCurrentPlanList.Where(m => m.CurrentFlightPlanID == null && m.SOBT.ToString("yyyy-MM-dd") == currDate.ToString("yyyy-MM-dd") && m.Creator1 == User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyAuditCurrentPlan.aspx"))
        {
            MenuStatis statis = new MenuStatis("待审核当日起飞申请", "MyAuditCurrentPlan.aspx", 0, "unAudit.jpg");
            //(m => m.ActorID == User.ID && DbFunctions.TruncateTime(m.SOBT) == currDate);
            statis.MenuPlanCount = VCurrentPlanList.Where(m => m.ActorID == User.ID && m.SOBT.ToString("yyyy-MM-dd") == currDate.ToString("yyyy-MM-dd")).Count();
            StatisList.Add(statis);
        }
        return JsonConvert.SerializeObject(StatisList);
    }

    List<RepetitivePlan> GetMenuStatisData(Expression<Func<RepetitivePlan, bool>> predicate)
    {
        List<RepetitivePlan> FlightPlanList = bll.GetList(predicate);
        return FlightPlanList;
    }
    List<FlightPlan> GetFlightPlanData(Expression<Func<FlightPlan, bool>> predicate)
    {
        var FlightPlanList = new FlightPlanBLL().GetList(predicate);
        return FlightPlanList;
    }
    List<vCurrentPlan> GetCurrentPlanData(Expression<Func<vCurrentPlan, bool>> predicate)
    {
        return new CurrentPlanBLL().GetList(predicate);
    }
    class MenuStatis
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        /// <summary>
        /// 提交的数量
        /// </summary>
        public int MenuPlanCount { get; set; }
        /// <summary>
        /// 任务图标
        /// </summary>
        public string MenuImgUrl { get; set; }
        public MenuStatis(string MenuName, string MenuUrl, int MenuPlanCount, string MenuImgUrl)
        {
            this.MenuName = MenuName;
            this.MenuUrl = MenuUrl;
            this.MenuPlanCount = MenuPlanCount;
            this.MenuImgUrl = MenuImgUrl;
        }
    }
}