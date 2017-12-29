using BLL.BasicData;
using BLL.SupplyDemandInformation;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected List<News> newsModel;
    protected List<SupplyDemandInfo> demandModel;
    protected List<Company> companyModel;
    protected double rnd;
    protected int year;
    private NewBLL newbll = new NewBLL();
    private CompanyBLL commmpanybll = new CompanyBLL();
    private SupplyDemandBLL demandBll = new SupplyDemandBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次加载，初始化页面信息
            year = DateTime.Now.Year;
            rnd = (new Random()).NextDouble();
        }
        LoadNews();
        LoadSupplyDemand();
        LoadCompanyIntro();
    }
    /// <summary>
    /// 新闻列表
    /// </summary>
    private void LoadNews()
    {
        newsModel = newbll.GetTopList(5, u => 1 == 1);
    }
    private void LoadSupplyDemand()
    {
        demandModel = demandBll.GetTopList(5, u => u.State == "end");
    }
    private void LoadCompanyIntro()
    {
        companyModel = commmpanybll.GetTopList(5, u => u.State == "end");
    }
}