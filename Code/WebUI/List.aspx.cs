using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BasicData;
using BLL.SupplyDemandInformation;
using Model.EF;

public partial class List : System.Web.UI.Page
{
    protected List<ListModel> listModel;
    private NewBLL newbll = new NewBLL();
    private CompanySummaryBLL commmpanybll = new CompanySummaryBLL();
    private SupplyDemandBLL demandBll = new SupplyDemandBLL();
    protected int PageIndex = 0;
    protected int TotalPage = 0;
    protected string Type;
    private HtmlWorkShop htmlHelper = new HtmlWorkShop();
    protected void Page_Load(object sender, EventArgs e)
    {
        Type = Request.QueryString["Type"];
        PageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
       // TotalPage = Convert.ToInt32(Request.QueryString["TotalPage"]);

        switch (Type)
        {
            case "News":
                LoadNews();
                break;
            case "SupplyDemand":
                LoadSupplyDemand();
                break;
            case "CompanyIntro":
                LoadCompanyIntro();
                break;
            default:
                break;
        }        
    }
    /// <summary>
    /// 新闻列表
    /// </summary>
    private void LoadNews()
    {
        int rowCount;

        listModel = newbll.GetList(PageIndex, 6, out TotalPage, out rowCount, u => 1 == 1).Select(m => new ListModel
        {
            Title = m.NewTitle,
            Content = m.NewContent,
            Id = m.NewID,
            Type = "News",
            CreateTime = m.CreateTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.NewContent) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.NewContent)
        }).ToList();
    }
    private void LoadSupplyDemand()
    {
        listModel = demandBll.GetTopList(6, u => u.State == "end").Select(m => new ListModel
        {
            Title = m.Title,
            Content = m.Summary,
            Id = m.ID,
            Type = "SupplyDemand",
            CreateTime = m.CreateTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.Summary) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.Summary)
        }).ToList();
    }
    private void LoadCompanyIntro()
    {
        listModel = commmpanybll.GetTopList(6, u => u.State == "end").Select(m => new ListModel
        {
            Title = m.Title,
            Content = m.SummaryCode,
            Id = m.ID,
            Type = "CompanyIntro",
            CreateTime = m.ModifiedTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.SummaryCode) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.SummaryCode)
        }).ToList();
    }
}

public class ListModel
{
    public string Title;
    public string Content;
    public int Id;
    public string Type;
    public DateTime? CreateTime;
    public string ImgPath;
}