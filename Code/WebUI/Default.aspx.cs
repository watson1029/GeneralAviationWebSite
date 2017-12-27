using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public double rnd;
    public int year;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次加载，初始化页面信息
            year = DateTime.Now.Year;
            rnd = (new Random()).NextDouble();
        }
    }
}