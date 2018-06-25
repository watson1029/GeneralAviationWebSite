using BLL.BasicData;
using BLL.FlightPlan;
using Model.Charts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Charts_FlyTime : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string GetData()
    {
        var flyList = new FlyTimeModel();
        if (string.IsNullOrEmpty(User.CompanyCode3))
        {
            flyList.NameItem.Add("各公司飞行时长(分钟)");
            var companyList = new CompanyBLL().GetAllCode3();
            foreach (var company in companyList)
            {
                var fly = new FlyTimeData();
                fly.name = company;
                fly.data.Add(new CurrentPlanBLL().GetFlyTime(company));
                flyList.FlyTimeData.Add(fly);
            }
            flyList.FlyTimeData = flyList.FlyTimeData.OrderByDescending(o => o.data[0]).ToList();
        }
        else
        {
            flyList.NameItem.Add("各月份飞行时长(分钟)");
            for (int i = 1; i <= 6; i++)
            {
                var begin = new DateTime(DateTime.Now.AddMonths(-i).Year, DateTime.Now.AddMonths(-i).Month, DateTime.Now.AddMonths(-i).Day);
                var end = new DateTime(begin.AddMonths(1).Year, begin.AddMonths(1).Month, begin.AddMonths(1).Day);
                var fly = new FlyTimeData();
                fly.name = begin.ToString("yyyy年MM月dd号") + "-" + end.ToString("yyyy年MM月dd号");
                fly.data.Add(new CurrentPlanBLL().GetFlyTime(User.CompanyCode3, begin, end));
                flyList.FlyTimeData.Add(fly);
            }
        }
        return JsonConvert.SerializeObject(flyList);
    }
}