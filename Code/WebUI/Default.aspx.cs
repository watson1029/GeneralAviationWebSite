using BLL.Adv;
using BLL.BasicData;
using BLL.SupplyDemandInformation;
using DAL.SystemManagement;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Untity;

public partial class Default : System.Web.UI.Page
{
    protected List<News> newsModel;
    protected List<SupplyDemandInfo> demandModel;
    protected List<CompanySummary> companySummaryModel;
    protected double rnd;
    protected List<Resource> resModel;
    protected List<string> picModel=new List<string>();
    private NewBLL newbll;
    private AdvertismentBLL advbll;
    private CompanySummaryBLL commmpanySummarybll;
    private SupplyDemandBLL demandBll;
    private ResourceDAL resDAL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            newbll = new NewBLL();
            advbll = new AdvertismentBLL();
            commmpanySummarybll = new CompanySummaryBLL();
            demandBll = new SupplyDemandBLL();
            resDAL = new ResourceDAL();

            //首次加载，初始化页面信息
            rnd = (new Random()).NextDouble();
        }
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "submit":
                    GALogin();
                    break;
                default:
                    break;
            }
        }
        LoadNews();
        LoadSupplyDemand();
        LoadCompanySummaryIntro();
        LoadResource();
        LoadPicture();
    }

    private void GALogin()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "登录失败！";
        var password = Request.Form["htxtPassword"]; //登录密码改在客户用js 的DES加密
        var userName = Request.Form["txtUserName"];
        var vcode = Request.Form["txtCode"];
        //    var remember = Request.Form["rememberme"] == "on" ? true : false;
        string ssCode = string.Empty;
        if (Session["session_verifycode"] != null)
        {
            ssCode = Session["session_verifycode"].ToString();
            Session.Remove("session_verifycode");
        }
        else
        {
            Response.Write(result.ToJsonString());
            Response.ContentType = "application/json";
            Response.End();
        }
        if (!ssCode.Equals(vcode, StringComparison.CurrentCultureIgnoreCase))
        {
            result.Msg = "验证码错误，请重新输入！";
            Response.Write(result.ToJsonString());
            Response.ContentType = "application/json";
            Response.End();
        }
        //解密的密码
        var PPassword = DES.uncMe(password, userName);
        string msg;
        //将明文密码转化为MD5加密
        password = CryptTools.HashPassword(PPassword);
        LoginResultEnum loginResult = LoginUtil.GALogin(StringSafeFilter.Filter(userName), StringSafeFilter.Filter(password.ToUpper()), false, out msg);

        if (loginResult == LoginResultEnum.LoginSuccess)
        {
            result.IsSuccess = true;
            result.Msg = msg;
        }

        if (loginResult == LoginResultEnum.NoUser ||
            loginResult == LoginResultEnum.OtherError || loginResult == LoginResultEnum.PasswordError 
            || loginResult == LoginResultEnum.LockUser)
        {
            result.Msg = msg;
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
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
        resModel = resDAL.GetTopList(13, 3);
    }
    private void LoadPicture()
    {
        //var model = advbll.Get(1);
        //if (model != null)
        //{
        //    var picarray = (model.PicPath ?? "").Split('|');
        //    foreach (var item in picarray)
        //    {
        //        var p1 = item.Split(',');
        //        picModel.Add(p1[0]);
        //    }
        //}

        string dirPath = Server.MapPath("Files/AdvPic");
        if(System.IO.Directory.Exists(dirPath))
        {
            //获得目录信息
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            //获得目录文件列表
            FileInfo[] files = dir.GetFiles("*.jpg");
            foreach (var item in files)
            {
                picModel.Add("Files/AdvPic/" + item.Name);
            }
        }
    }
}