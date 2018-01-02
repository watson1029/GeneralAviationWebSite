using BLL.Adv;
using BLL.BasicData;
using BLL.SupplyDemandInformation;
using DAL.SystemManagement;
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
    protected List<CompanySummary> companySummaryModel;
    protected double rnd;
    protected int year;
    protected List<Resource> resModel;
    protected List<string> picModel=new List<string>();
    private NewBLL newbll = new NewBLL();
    private AdvertismentBLL advbll = new AdvertismentBLL();
    private CompanySummaryBLL commmpanySummarybll = new CompanySummaryBLL();
    private SupplyDemandBLL demandBll = new SupplyDemandBLL();
    private ResourceDAL resDAL = new ResourceDAL();
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
        LoadCompanySummaryIntro();
        LoadResource();
        LoadPicture();
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
    private void LoadCompanySummaryIntro()
    {
        companySummaryModel = commmpanySummarybll.GetTopList(5, u => u.State == "end");
    }
    private void LoadResource()
    {
        resModel = resDAL.GetTopList(5, 2);
    }
    private void LoadPicture()
    {
        var model = advbll.Get(1);
        if (model != null)
        {
            var picarray = (model.PicPath ?? "").Split('|');
            foreach (var item in picarray)
            {
                var p1 = item.Split(',');
                picModel.Add(p1[0]);
            }
        }
    }
}