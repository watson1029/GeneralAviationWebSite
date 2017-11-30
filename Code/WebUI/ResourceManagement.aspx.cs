using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Untity;
using DAL;
using DAL.SystemManagement;
public partial class ResourceManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void bt_submit_Click(object sender, EventArgs e)
    {
        string Title = Request["Title"];
        string DealUser = Request["DealUser"];
        int ResourceType = Convert.ToInt16( Request["ResourceType"]);
        string UsefulTime = Request["UsefulTime"];
        
        Resource resource = new Resource();
        resource.Title = Title;
        resource.DealUser = DealUser;
        resource.ResourceType = ResourceType;
        resource.UsefulTime = UsefulTime;
        resource.SenderId = 123;
        ResourceDAL.Add(resource);
    }
}