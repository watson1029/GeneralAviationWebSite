using DAL.BasicData;
using System;
using System.Web;

public partial class _Default : System.Web.UI.MasterPage
{
    protected string summary;
    protected int year;
    private CompanyDAL companyDAL = new CompanyDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次加载，初始化页面信息
            year = DateTime.Now.Year;
            summary = HttpUtility.UrlDecode(companyDAL.Find(a => a.Catalog == 0).SummaryCode);
        }
    }
}
