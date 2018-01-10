using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BLL.BasicData;
using BLL.SupplyDemandInformation;

public partial class Detail : System.Web.UI.Page
{
    private List<DetailModel> listModel;
    private NewBLL newbll = new NewBLL();
    private CompanySummaryBLL commmpanybll;
    private SupplyDemandBLL demandBll;
    private int id=0;
    protected string type;
    protected string parentTitle = "";
    protected DetailModel currModel;
    protected DetailModel previousModel;
    protected DetailModel nextModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            commmpanybll = new CompanySummaryBLL();
            demandBll = new SupplyDemandBLL();
            currModel = new DetailModel();
            previousModel = new DetailModel();
            nextModel = new DetailModel();
        }

        try
        {
            id = Request.QueryString["Id"] == null ? 0 : Convert.ToInt32(Request.QueryString["Id"]);
            if (id < 1) throw new Exception("参数[Id]无效！");
            type = Request.QueryString["Type"];

            switch (type)
            {
                case "News":
                    parentTitle = "新闻中心";
                    LoadNews();
                    break;
                case "SupplyDemand":
                    parentTitle = "供求信息";
                    LoadSupplyDemand();
                    break;
                case "CompanyIntro":
                    parentTitle = "通航企业";
                    LoadCompanyIntro();
                    break;
                default:
                    throw new Exception("参数[Type]无效！");
            }
            GetCurrent();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('"+ ex.Message +"')</script>");
            return;
        }        
    }
    /// <summary>
    /// 新闻列表
    /// </summary>
    private void LoadNews()
    {
        listModel = newbll.GetList(u => 1 == 1).Select(m => new DetailModel
        {
            Title = m.NewTitle,
            Content = m.NewContent,
            Id = m.NewID,
            Type = "News",
            CreateTime = m.CreateTime,
            Creator = m.CreateUser
        }).ToList();
    }
    private void LoadSupplyDemand()
    {
        listModel = demandBll.GetList(u => u.State == "end").Select(m => new DetailModel
        {
            Title = m.Title,
            Content = m.Summary,
            Id = m.ID,
            Type = "SupplyDemand",
            CreateTime = m.CreateTime,
            Creator = m.CreateName
        }).ToList();
    }
    private void LoadCompanyIntro()
    {
        listModel = commmpanybll.GetList(u => u.State == "end").Select(m => new DetailModel
        {
            Title = m.Title,
            Content = m.SummaryCode,
            Id = m.ID,
            Type = "CompanyIntro",
            CreateTime = m.ModifiedTime,
            Creator = m.ModifiedByName
        }).ToList();
    }

    public void GetCurrent()
    {
        if (currModel.Id==0) currModel = listModel.Find(m => m.Id == id);
        if (currModel == null){
            currModel = new DetailModel();
            throw new Exception("参数[Id]无效！");
        }    

        int currIndex = listModel.IndexOf(listModel.Find(m => m.Id == currModel.Id));
        if (currIndex == 0)
            previousModel = new DetailModel();
        else
            previousModel = listModel[currIndex - 1];

        if (currIndex == listModel.Count() - 1)
            nextModel =  new DetailModel();
        else
            nextModel = listModel[currIndex + 1];      
    }

    public string SetURL(string rawURL)
    {
        //http://localhost:24612/Detail.aspx?Type=CompanyIntro&Id=10
        if (!string.IsNullOrEmpty(rawURL))
        {
            rawURL = rawURL.Replace("Detail", "List");
            int pos = rawURL.IndexOf("?");
            if (pos >= 0)
            {
                return rawURL.Substring(0, pos + 1) + "Type=" + Request.QueryString["Type"] + "&PageIndex=1";
            }
            return "#";
        }
        return "#";
    }
}
public class DetailModel
{
    public string Title;
    public string Content;
    public int Id;
    public string Type;
    public DateTime? CreateTime;
    public string Creator;
}