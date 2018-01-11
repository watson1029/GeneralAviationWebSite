using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileList : System.Web.UI.Page
{
    public string title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string type = Request.QueryString["Type"];
                switch (type)
                {
                    case "File":
                        title = "通航资料";
                        break;                    
                    default:
                        throw new Exception("参数[Type]无效！");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
                return;
            }
        }
    }
}