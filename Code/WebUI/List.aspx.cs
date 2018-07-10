using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BLL.BasicData;
using BLL.SupplyDemandInformation;
using DAL.SystemManagement;
using System.Linq.Expressions;
using Model.EF;
using Untity;

public partial class List : System.Web.UI.Page
{
    protected List<ListModel> listModel;
    private NewBLL newbll;
    private CompanySummaryBLL commmpanybll;
    private SupplyDemandBLL demandBll;
    private ResourceDAL resourceDAL;
    protected int pageIndex = -1;
    protected int totalPage = 0;
    protected int rowCount = 0;
    protected string type;
    private HtmlWorkShop htmlHelper;
    protected string title = "";
    private Expression<Func<Resource, bool>> predicate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次加载，初始化数据
            newbll = new NewBLL();
            commmpanybll = new CompanySummaryBLL();
            demandBll = new SupplyDemandBLL();
            htmlHelper = new HtmlWorkShop();
            resourceDAL = new ResourceDAL();
            predicate = PredicateBuilder.True<Resource>();
            predicate = predicate.And(m => m.Status == 3).And(m => m.IsDeleted == 0);
        }

        try
        {
            type = Request.QueryString["Type"];
            pageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
            if (pageIndex < 1 || (pageIndex > totalPage && totalPage > 0)) throw new Exception("参数[PageIndex]无效！");

            switch (type)
            {
                case "News":
                    title = "新闻中心";
                    LoadNews();
                    break;
                case "SupplyDemand":
                    title = "供求信息";
                    LoadSupplyDemand();
                    break;
                case "CompanyIntro":
                    title = "通航企业";
                    LoadCompanyIntro();
                    break;
                case "File":
                    title = "通航资料";
                    LoadFile();
                    break;
                case "Plan":
                    title = "飞行计划";

                    break;
                case "Weather":
                    title = "气象";

                    break;
                case "Information":
                    title = "情报";

                    break;
                case "Surveillance":
                    title = "监视";

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
    /// <summary>
    /// 新闻中心
    /// </summary>
    private void LoadNews()
    {
        listModel = newbll.GetList(pageIndex, 5, out totalPage, out rowCount, u => 1 == 1).Select(m => new ListModel
        {
            Title = m.NewTitle,
            Content = m.NewContent,
            Id = m.NewID,
            type = "News",
            CreateTime = m.CreateTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.NewContent) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.NewContent)
        }).ToList();
    }
    /// <summary>
    /// 供求信息
    /// </summary>
    private void LoadSupplyDemand()
    {
        listModel = demandBll.GetList(pageIndex, 5, out totalPage, out rowCount, u => u.State == "end").Select(m => new ListModel
        {
            Title = m.Title,
            Content = m.SummaryCode,
            Id = m.ID,
            type = "SupplyDemand",
            CreateTime = m.CreateTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.Summary) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.Summary)
        }).ToList();
    }
    /// <summary>
    /// 通航企业
    /// </summary>
    private void LoadCompanyIntro()
    {
        listModel = commmpanybll.GetList(pageIndex, 5, out totalPage, out rowCount, u => u.State == "end").Select(m => new ListModel
        {
            Title = m.Title,
            Content = m.SummaryCode,
            Id = m.ID,
            type = "CompanyIntro",
            CreateTime = m.ModifiedTime,
            ImgPath = htmlHelper.getFirstImgSrc(m.SummaryCode) == "" ? "/images/nofound.jpg" : htmlHelper.getFirstImgSrc(m.SummaryCode)
        }).ToList();
    }
    /// <summary>
    /// 通航资料
    /// </summary>
    private void LoadFile()
    {        
        listModel = resourceDAL.FindPagedList(pageIndex, 5, out totalPage, out rowCount, predicate, u => u.Created.Value, false).Select(m => new ListModel
        {
            //文件标题
            Title = m.Title,
            //有效时间段
            //Content = m.UsefulTime,
            Id = m.ID,
            type = "File",
            CreateTime = m.Created.Value,
            //文件路径
            ImgPath = m.FilePath.IndexOf(".doc")>-1? "/images/word.jpg": "/images/pdf.jpg",
            FilePath = m.FilePath
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
    public string FilePath;
}