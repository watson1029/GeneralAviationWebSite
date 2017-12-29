using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BasicData;
using BLL.SupplyDemandInformation;
using Model.EF;

public partial class Detail : System.Web.UI.Page
{
    private List<DetailModel> listModel;
    private NewBLL newbll = new NewBLL();
    private CompanyBLL commmpanybll = new CompanyBLL();
    private SupplyDemandBLL demandBll = new SupplyDemandBLL();
    private int Id=0;
    protected DetailModel currModel = new DetailModel();
    protected DetailModel previousModel = new DetailModel();
    protected DetailModel nextModel = new DetailModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["Type"];
        Id = Request.QueryString["Id"]==null?0:Convert.ToInt32(Request.QueryString["Id"]); 
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
                break;
        }

        GetCurrent();
    }
    /// <summary>
    /// 新闻列表
    /// </summary>
    private void LoadNews()
    {
        listModel = newbll.GetTopList(6, u => 1 == 1).Select(m => new DetailModel
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
        listModel = demandBll.GetTopList(6, u => u.State == "end").Select(m => new DetailModel
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
        listModel = commmpanybll.GetTopList(6, u => u.State == "end").Select(m => new DetailModel
        {
            Title = m.CompanyName,
            Content = m.SummaryCode,
            Id = m.CompanyID,
            Type = "CompanyIntro",
            CreateTime = m.CreateTime,
            Creator = m.ModifiedByName
        }).ToList();
    }

    public void GetCurrent()
    {
        if (currModel.Id == 0) currModel = listModel.Find(m => m.Id == Id);
        if (currModel!=null){
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
    }
    //public void GetNext()
    //{
    //    int currIndex = listModel.IndexOf(listModel.Find(m => m.Id == currModel.Id));
    //    if (currIndex == listModel.Count())
    //        currModel = listModel[currIndex];
    //    else
    //        currModel = listModel[currIndex + 1];

    //}
    //public void GetPrevious()
    //{
    //    int currIndex = listModel.IndexOf(listModel.Find(m => m.Id == currModel.Id));
    //    if (currIndex == 1)
    //        currModel = listModel[currIndex];
    //    else
    //        currModel = listModel[currIndex - 1];
    //}
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