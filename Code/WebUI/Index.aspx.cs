using BLL.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : BasePage
{
    protected string UserName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            UserName = User.UserName;


        }
    }
    protected string GetMenuJson()
    {
        return MenuBLL.CreateMenuJson(User.ID);

    }
}