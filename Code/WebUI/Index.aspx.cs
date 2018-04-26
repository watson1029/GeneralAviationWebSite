﻿using BLL.FlightPlan;
using BLL.SystemManagement;
using Model.EF;
using Model.SystemManagement;
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

public partial class Index : BasePage
{
    protected string UserName = string.Empty;
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            UserName = User.UserName;


        }
    }
    protected string GetMenuJson()
    {
        return new MenuBLL().CreateMenuJson(User.ID);

    }
    protected string GetUserDataJson()
    {
        var menuListJson=new MenuBLL().CreateMenuJson(User.ID);
        var currDate = DateTime.Now.Date;
        List<MenuStatis> StatisList = new List<MenuStatis>();
        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        List<RepetitivePlan> RepetitivePlanList = GetMenuStatisData(predicate);
        Expression<Func<FlightPlan, bool>> Fpredicate = PredicateBuilder.True<FlightPlan>();
        List<FlightPlan> FlightPlanList= GetFlightPlanData(Fpredicate);
        Expression<Func<V_CurrentPlan, bool>> vcpredicate = PredicateBuilder.True<V_CurrentPlan>();
        List<V_CurrentPlan> VCurrentPlanList = GetCurrentPlanData(vcpredicate);
        if (menuListJson.Contains("MyUnSubmitRepetPlan.aspx")) //长期计划列表(待提交)
        {
            MenuStatis statis = new MenuStatis("待提交长期计划", "MyUnSubmitRepetPlan.aspx", 0, "Rpundo.jpg");
            statis.MenuPlanCount= RepetitivePlanList.Where(a => a.PlanState == "0" && a.Creator == User.ID).Count();     
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MySubmitRepetPlan.aspx")) //长期计划列表(已提交)
        {
            MenuStatis statis = new MenuStatis("已提交长期计划", "MySubmitRepetPlan.aspx", 0, "havedo.jpg");
            statis.MenuPlanCount = RepetitivePlanList.Where(a => a.PlanState != "0" && a.Creator == User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyAuditRepetPlan.aspx"))//长期计划列表(待审核)
        {
            MenuStatis statis = new MenuStatis("待审核长期计划", "MyAuditRepetPlan.aspx",0, "Psubmit.jpg");
            statis.MenuPlanCount = RepetitivePlanList.Where(a => a.ActorID == User.ID && a.Creator != User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyFinishAuditRepetPlan.aspx"))//长期计划列表(已审核)
        {
            MenuStatis statis = new MenuStatis("已审核长期计划", "MyFinishAuditRepetPlan.aspx", 0, "havesubmit.jpg");
            statis.MenuName = "已审核长期计划";
            statis.MenuUrl = "MyFinishAuditRepetPlan.aspx";
            Expression<Func<vGetRepetitivePlanNodeInstance, bool>> Apredicate = PredicateBuilder.True<vGetRepetitivePlanNodeInstance>();
            Apredicate = Apredicate.And(m => m.ActorID != m.Creator);
            Apredicate = Apredicate.And(m => m.ActorID == User.ID);
            Apredicate = Apredicate.And(m => m.State == 2 || m.State == 3);
            Expression<Func<vGetRepetitivePlanNodeInstance, bool>> strWhere = Apredicate;
            statis.MenuPlanCount = bll.GetNodeInstanceList(strWhere).Count;
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyUnSubmitFlightPlan.aspx"))//飞行计划列表(待提交)
        {
            MenuStatis statis = new MenuStatis("待提交飞行计划", "MyUnSubmitFlightPlan.aspx", 0, "unsubmit.jpg");
            statis.MenuPlanCount=FlightPlanList.Where(m=>m.PlanState == "0"&& m.Creator == User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MySubmitFlightPlan.aspx"))//飞行计划列表(已提交)
        {
            MenuStatis statis = new MenuStatis("已提交飞行计划", "MySubmitFlightPlan.aspx", 0, "PlanSubmit.jpg");           
            statis.MenuPlanCount = FlightPlanList.Where(m => m.PlanState != "0" && m.Creator == User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyAuditFlightPlan.aspx"))//飞行计划列表(待审核)
        {
            MenuStatis statis = new MenuStatis("待审核飞行计划", "MyAuditFlightPlan.aspx", 0, "RpPlan.jpg");
            statis.MenuPlanCount = FlightPlanList.Where(m => m.ActorID == User.ID && m.Creator != User.ID).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyFinishAuditFlightPlan.aspx"))//飞行计划列表(已审核)
        {
            MenuStatis statis = new MenuStatis("已审核飞行计划", "MyFinishAuditFlightPlan.aspx", 0, "Rpsubmit.jpg");
            Expression<Func<vGetFlightPlanNodeInstance, bool>> vpredicate = PredicateBuilder.True<vGetFlightPlanNodeInstance>();
            vpredicate = vpredicate.And(m => m.ActorID != m.Creator);
            vpredicate = vpredicate.And(m => m.ActorID == User.ID);
            vpredicate = vpredicate.And(m => m.State == 2 || m.State == 3);
            statis.MenuPlanCount = new FlightPlanBLL().GetNodeInstanceList(vpredicate).Count;
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyUnSubmitCurrentPlan.aspx"))
        {
            MenuStatis statis = new MenuStatis("待提交当日起飞申请", "MyUnSubmitCurrentPlan.aspx", 0, "uncurrent.jpg");
            //vcpredicate = vcpredicate.And(m => m.CurrentFlightPlanID == null && DbFunctions.TruncateTime(m.SOBT) == currDate);
            statis.MenuPlanCount = VCurrentPlanList.Where(m => m.CurrentFlightPlanID == null && m.SOBT.ToString("yyyy-MM-dd") == currDate.ToString("yyyy-MM-dd")).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MySubmitCurrentPlan.aspx"))
        {
            MenuStatis statis = new MenuStatis("已提交当日起飞申请", "MySubmitCurrentPlan.aspx", 0, "currentdo.jpg");
            //vcpredicate = vcpredicate.And(m => m.CurrentFlightPlanID == null && DbFunctions.TruncateTime(m.SOBT) == currDate);
            statis.MenuPlanCount = VCurrentPlanList.Where(m => m.PlanState != "0" && m.Creator == User.ID && m.SOBT.ToString("yyyy-MM-dd") == currDate.ToString("yyyy-MM-dd")).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyAuditCurrentPlan.aspx"))
        {
            MenuStatis statis = new MenuStatis("待审核当日起飞申请", "MyAuditCurrentPlan.aspx", 0, "unAudit.jpg");
            //(m => m.ActorID == User.ID && DbFunctions.TruncateTime(m.SOBT) == currDate);
            statis.MenuPlanCount = VCurrentPlanList.Where(m => m.ActorID == User.ID && m.SOBT.ToString("yyyy-MM-dd") == currDate.ToString("yyyy-MM-dd")).Count();
            StatisList.Add(statis);
        }
        if (menuListJson.Contains("MyAuditCurrentPlanOdy.aspx"))
        {
            MenuStatis statis = new MenuStatis("已审核当日起飞申请", "MyAuditCurrentPlanOdy.aspx", 0, "Audit.jpg");
            //m => m.ActorID == null && m.PlanState == "end"
            statis.MenuPlanCount = VCurrentPlanList.Where(m => m.ActorID == null && m.PlanState == "end").Count();
            StatisList.Add(statis);
        }
        return JsonConvert.SerializeObject(StatisList);
    }
    List<RepetitivePlan> GetMenuStatisData(Expression<Func<RepetitivePlan, bool>> predicate)
    {
        List<RepetitivePlan> FlightPlanList = bll.GetList(predicate);
        return FlightPlanList;
    }
    List<FlightPlan> GetFlightPlanData( Expression<Func<FlightPlan, bool>> predicate)
    {
        var FlightPlanList = new FlightPlanBLL().GetList(predicate);
        return FlightPlanList;
    }
    List<V_CurrentPlan> GetCurrentPlanData(Expression<Func<V_CurrentPlan, bool>> predicate)
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
        public MenuStatis(string MenuName,string MenuUrl,int MenuPlanCount,string MenuImgUrl)
        {
            this.MenuName = MenuName;
            this.MenuUrl = MenuUrl;
            this.MenuPlanCount = MenuPlanCount;
            this.MenuImgUrl = MenuImgUrl;
        }
    }
}