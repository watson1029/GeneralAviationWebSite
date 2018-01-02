using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BasicData;
using BLL.SupplyDemandInformation;
using Model.EF;
using Untity;

public partial class List : System.Web.UI.Page
{
    protected List<ListModel> listModel;
    private NewBLL newbll = new NewBLL();
    private CompanySummaryBLL commmpanybll = new CompanySummaryBLL();
    private SupplyDemandBLL demandBll = new SupplyDemandBLL();
    protected int pageIndex = -1;
    protected int totalPage = 0;
    protected int rowCount = 0;
    protected string type;
    private HtmlWorkShop htmlHelper = new HtmlWorkShop();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            type = Request.QueryString["Type"];
            pageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
            if (pageIndex < 1 || (pageIndex > totalPage && totalPage > 0)) throw new Exception("参数[PageIndex]无效！");

            switch (type)
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
                    throw new Exception("参数[Type]无效！");
            } 
        }
        catch(Exception ex)
        {
            Response.Write("<script language='javascript'>alert('"+ ex.Message +"');</script>");
            return;
        }           
    }
    /// <summary>
    /// 新闻列表
    /// </summary>
    private void LoadNews()
    {
        listModel = newbll.GetList(pageIndex, 6, out totalPage, out rowCount, u => 1 == 1).Select(m => new ListModel
        {
            Title = m.NewTitle,
            Content = m.NewContent,
            Id = m.NewID,
            type = "News",
            CreateTime = m.CreateTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.NewContent) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.NewContent)
        }).ToList();
    }
    private void LoadSupplyDemand()
    {
        listModel = demandBll.GetList(pageIndex, 6, out totalPage, out rowCount, u => u.State == "end").Select(m => new ListModel
        {
            Title = m.Title,
            Content = m.Summary,
            Id = m.ID,
            type = "SupplyDemand",
            CreateTime = m.CreateTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.Summary) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.Summary)
        }).ToList();
    }
    private void LoadCompanyIntro()
    {
        listModel = commmpanybll.GetList(pageIndex, 6, out totalPage, out rowCount, u => u.State == "end").Select(m => new ListModel
        {
            Title = m.Title,
            Content = m.SummaryCode,
            Id = m.ID,
            type = "CompanyIntro",
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
    public string type;
    public DateTime? CreateTime;
    public string ImgPath;
}