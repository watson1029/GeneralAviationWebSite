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

public partial class Charts_FlyNum : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string GetData()
    {
        var flyList = new FlyNumModel();
        if (string.IsNullOrEmpty(User.CompanyCode3))
        {
            var companyList = new CompanyBLL().GetAllCode3();
            flyList.NameItem = companyList;
            foreach (var company in companyList)
            {
                var fly = new FlyNumData();
                fly.name = company;
                fly.value = new CurrentPlanBLL().GetFlyNum(company);
                flyList.FlyNumData.Add(fly);
            }
        }
        else
        {
            for (int i = 1; i <= 6; i++)
            {
                var begin = new DateTime(DateTime.Now.AddMonths(-i).Year, DateTime.Now.AddMonths(-i).Month, DateTime.Now.AddMonths(-i).Day);
                var end = new DateTime(begin.AddMonths(1).Year, begin.AddMonths(1).Month, begin.AddMonths(1).Day);
                var fly = new FlyNumData();
                fly.name = begin.ToString("yyyy年MM月dd号") + "-" + end.ToString("yyyy年MM月dd号");
                fly.value = new CurrentPlanBLL().GetFlyNum(User.CompanyCode3, begin, end);
                flyList.NameItem.Add(fly.name);
                flyList.FlyNumData.Add(fly);
            }
        }
        return JsonConvert.SerializeObject(flyList);
    }
}