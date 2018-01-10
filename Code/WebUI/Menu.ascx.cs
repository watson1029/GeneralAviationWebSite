using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu : System.Web.UI.UserControl
{
    protected string names
    {
        get
        {
            var name = "default";
            var url = Request.RawUrl.ToLower();
            if (url.Contains("default.aspx"))
            {
                name = "default";
            }
            else if (url.Contains("/list.aspx?type=news"))
            {
                name = "news";
            }
            else if (url.Contains("/list.aspx?type=supplydemand"))
            {
                name = "supplydemand";
            }
            else if (url.Contains("/list.aspx?type=companyintro"))
            {
                name = "companyintro";
            }

            return name;
        }
    }
    protected string position
    {
        get
        {
            var left = "0px";
            var url = Request.RawUrl.ToLower();
            if (url.Contains("default.aspx"))
            {
                left = "0px";
            }
            else if (url.Contains("/list.aspx?type=news"))
            {
                left = "128px";
            }
            else if (url.Contains("/list.aspx?type=supplydemand"))
            {
                left = "256px";
            }
            else if (url.Contains("/list.aspx?type=companyintro"))
            {
                left = "384px";
            }

            return left;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}