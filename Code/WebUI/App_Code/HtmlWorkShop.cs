using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// HtmlWorkShop 的摘要说明
/// </summary>
public class HtmlWorkShop
{
	public HtmlWorkShop()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string CutTitle(string title, int maxSize)
    {
        if (string.IsNullOrEmpty(title))
        {
            return "";
        }
        title = striphtml(ClearHtml(title)).Replace("&nbsp;", "").Replace("&#12288;", "").Replace("&#12288", "").Replace(" ", "").Trim();
        int leng = title.Length;
        int totalLength = 0;
        int currentIndex = 0;
        while (totalLength < maxSize * 2 && currentIndex < leng)
        {
            if (title[currentIndex] < 0 || title[currentIndex] > 255)
                totalLength += 2;
            else
                totalLength++;
            currentIndex++;
        }
        if (currentIndex < leng && currentIndex != leng - 1)
        {
            title = title.Substring(0, currentIndex) + "...";
        }

        return title;
    }
    // 除去所有在html元素中标记
    public static string striphtml(string strhtml)
    {
        string stroutput = strhtml;
        Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
        stroutput = regex.Replace(stroutput, "");
        return stroutput;
    }
    /// <summary>
    /// 清除html标签。
    /// </summary>
    /// <param name="orgin">要清除html标签的字符串。</param>
    /// <returns></returns>
    public static string ClearHtml(string orgin)
    {

        orgin = orgin.Replace("\r", "");
        orgin = orgin.Replace("\n", "");
        orgin = orgin.Replace("<br>", "");
        orgin = orgin.Replace("<BR>", "").Replace("&nbsp;", "").Replace("&nbsp", "");
        string m_pattern = @"<.*?>";
        Regex r = new Regex(m_pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        MatchCollection mc = r.Matches(orgin);
        if (mc.Count > 0)
        {
            for (int i = 0; i < mc.Count; i++)
            {
                string m_pattern2 = @"<[\\s]*>";
                string group = mc[i].Groups[0].ToString();
                if (null != group && ("<br>".Equals(group.ToLower()) || "<br/>".Equals(group.ToLower())))
                { continue; }
                Regex r2 = new Regex(m_pattern2, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc2 = r.Matches(group);
                if (mc2.Count > 0)
                {
                    for (int j = 0; j < mc2.Count; j++)
                    {
                        string rep = mc2[j].Groups[0].ToString();
                        orgin = orgin.Replace(rep, string.Empty);
                    }
                }
            }
        }
        return orgin;
    }
    public string getFirstImgSrc(string urlEncodeString)
    {
        if (!string.IsNullOrEmpty(urlEncodeString))
        {
            string urlDecodeString = HttpUtility.UrlDecode(urlEncodeString);          
            int startPos = urlDecodeString.IndexOf("<img");
            if (startPos >= 0)
            {
                string img = urlDecodeString.Substring(startPos);

                int endPos = img.IndexOf("/>");
                if (endPos >= 0)
                {
                    img = img.Substring(0, endPos + 2);

                    startPos = img.IndexOf("src=\"");
                    img = img.Substring(startPos + 5);

                    endPos = img.IndexOf("\"");

                    return img.Substring(0, endPos);
                }
                return "";
            }
            return "";
        }
        return "";
    }
    /// <summary>
    /// 获取内容简介并关键字高亮
    /// </summary>
    /// <param name="des"></param>
    /// <param name="MaxNum"></param>
    /// <param name="cont"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string GetCutDes(string des, int MaxNum, string cont, string color)
    {
        try
        {
            string[] strs = cont.Split(' ');
            string dess = striphtml(ClearHtml(des)).Replace("&nbsp;", "").Replace("&#12288;", "").Replace("&#12288", "").Replace(" ", "").Trim();
            List<string> nstr = new List<string>();
            foreach (string str in strs)//处理数组空元素
            {
                if (str != null && str != " " && str != "")
                {
                    nstr.Add(str);
                }
            }
            int prelength = 0;
            int i = 0;
            foreach (string st in nstr)//获取最前关键字下标
            {
                if (i == 0 && dess.IndexOf(st) >= 0)
                {
                    prelength = dess.IndexOf(st, StringComparison.CurrentCultureIgnoreCase);
                    i = 1;
                }
                else
                {
                    if (dess.IndexOf(st, StringComparison.CurrentCultureIgnoreCase) < prelength && dess.IndexOf(st, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        prelength = dess.IndexOf(st);
                    }
                }

            }

            string predes = dess.Substring(0, prelength);//获取关键字前面内容
            if (prelength > 30)
            {
                predes = predes.Substring(0, 30) + "...";
            }
            dess = predes + dess.Substring(prelength);
            int leng = dess.Length;
            int totalLength = 0;
            int currentIndex = 0;
            while (totalLength < MaxNum * 2 && currentIndex < leng)
            {
                if (dess[currentIndex] < 0 || dess[currentIndex] > 255)
                    totalLength += 2;
                else
                    totalLength++;
                currentIndex++;
            }
            if (currentIndex < leng)
            {
                dess = dess.Substring(0, currentIndex) + "...";
            }
            dess = dess.ToLower();
            foreach (string rstr in nstr)//改变关键字样式
            {
                dess = dess.Replace(rstr.ToLower(), "<label style='color:" + color + ";'>" + rstr + "</label>");
            }
            return dess;
        }
        catch
        {
            return "";
        }
    }


}