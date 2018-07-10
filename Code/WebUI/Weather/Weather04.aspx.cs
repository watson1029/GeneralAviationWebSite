using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Weather_Weather04 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = "57083";
        string city = "zhengzhou";
        if (Request.QueryString["code"] != null && Request.QueryString["city"] != null)
        {
            code = Request.QueryString["code"].ToString();
            city = Request.QueryString["city"].ToString();
        }

        if (CheckUrlVisit("http://www.nmc.cn/rest/real/" + code))
        {
            //创建对象
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;//网络凭证


            //加载实时气象信息json
            string htmlPath = "http://www.nmc.cn/rest/real/" + code;
            Byte[] pageData = webClient.DownloadData(htmlPath);
            string json_real = Encoding.UTF8.GetString(pageData);//UTF-8编码

            //加载温度曲线json
            htmlPath = "http://www.nmc.cn/rest/tempchart/" + code;
            pageData = webClient.DownloadData(htmlPath);
            string json_tempchart = Encoding.UTF8.GetString(pageData);//UTF-8编码

            //加载气象质量json
            htmlPath = "http://www.nmc.cn/rest/aqi/" + code;
            pageData = webClient.DownloadData(htmlPath);
            string json_aqi = Encoding.UTF8.GetString(pageData);//UTF-8编码

            //加载24h气象信息json
            htmlPath = "http://www.nmc.cn/rest/passed/" + code;
            pageData = webClient.DownloadData(htmlPath);
            string json_passed = Encoding.UTF8.GetString(pageData);//UTF-8编码

            //解析网页
            htmlPath = "http://www.nmc.cn/publish/forecast/AHA/" + city + ".html";
            pageData = webClient.DownloadData(htmlPath);
            string pageHtml = Encoding.UTF8.GetString(pageData);//UTF-8编码

            //用htmlagilitypack 解析网页内容
            //加载html
            htmlDocument.LoadHtml(pageHtml);
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            sb.Append("<html>");
            //加载head
            HtmlAgilityPack.HtmlNode htmlHead = htmlDocument.DocumentNode.SelectSingleNode("//head");
            sb.Append(htmlHead.OuterHtml.Replace("ctx = '/f'", "ctx = 'http://www.nmc.cn/f'"));
            //加载head中script
            HtmlAgilityPack.HtmlNode htmlScript = htmlDocument.DocumentNode.SelectSingleNode("//script");
            HtmlAgilityPack.HtmlNodeCollection scriptCollection = htmlScript.ChildNodes;
            for (int i = 0; i < scriptCollection.Count; i++)
            {
                sb.Append(scriptCollection[i].OuterHtml);
            }
            //通过xpath 选中指定元素；xpath 参考：http://www.w3school.com.cn/xpath/xpath_syntax.asp
            HtmlAgilityPack.HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='container']");
            sb.Append(htmlNode.OuterHtml);
            //加载body中script
            htmlScript = htmlDocument.DocumentNode.SelectSingleNode("//body");
            scriptCollection = htmlScript.ChildNodes;
            for (int i = 0; i < scriptCollection.Count; i++)
            {
                if (scriptCollection[i].Name.IndexOf("script") > -1)
                    sb.Append(scriptCollection[i].OuterHtml);
            }
            sb.Append("</html>");

            string str = sb.ToString();
            str = str.Replace("http://image.nmc.cn/static2/favicon.ico", "");
            str = str.Replace("/publish/forecast/china.html", "javascript:void();");
            str = str.Replace("/publish/forecast/AHA.html", "javascript:void();");
            str = str.Replace("http://image.nmc.cn/static2/site/nmc/themes/basic/js/weather_chart.js?v=2017112220180325", "weather_chart.js");
            str = str.Replace("initReal('" + code + "');", "initReal('" + json_real + "');");
            str = str.Replace("initAqi('" + code + "');", "initAqi('" + json_aqi + "');");
            str = str.Replace("drawTemperature('" + code + "');", "drawTemperature('" + json_tempchart + "'); json_passed='" + json_passed + "';");
            Response.Write(str);
        }
        else {
            Response.Write("<font color='red'>未能获取到气象信息，请联系系统管理员！</font>");
        }
    }

    private bool CheckUrlVisit(string url)
    {
        try
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            if (res.StatusCode == HttpStatusCode.OK)
            {
                res.Close();
                return true;
            }
        }
        catch (WebException webex)
        {
            return false;
        }
        return false;
    }
}