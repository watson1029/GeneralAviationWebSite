using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.FlightPlan;

public partial class Collect : System.Web.UI.Page
{
    private RepetitivePlanDAL dao = new RepetitivePlanDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
       // List<Untity.TemplateClass4StatisticResult> list=dao.getStatisticResult();
        
    }
}