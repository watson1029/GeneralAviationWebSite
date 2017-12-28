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
}