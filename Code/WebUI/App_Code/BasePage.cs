using BLL.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage : Page
{
    public BasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    private const string userOwnRightKey = "UserOwnRightKey";
    private string m_PageRightCode = string.Empty;

    /// <summary>
    /// 页的权限编码
    /// </summary>
    public virtual string PageRightCode
    {
        get { return m_PageRightCode; }
        set { m_PageRightCode = value; }
    }


    /// <summary>
    /// 当前登录的用户
    /// </summary>
    protected new UserInfoCookie User
    {
        get
        {
            var userInfo = UserLoginService.Instance.GetUser();
            if (userInfo == null)
            {
                IdentityService.Instance.SignOut();
            }
            return userInfo;
        }
    }
    protected override void OnLoad(EventArgs e)
    {
        var user = UserLoginService.Instance.GetUser();
        if (user == null)
        {
            //跳转到登录页面
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            if (!string.IsNullOrEmpty(PageRightCode))
            {
                List<string> hasRightCodes = GetOwnRights(user.ID);
                if (!hasRightCodes.Contains(PageRightCode))
                {
                    Response.Redirect("~/NoAuthority.html");
                }
            }
        }

         base.OnLoad(e);

    }


    /// <summary>
    /// 获取用户权限
    /// </summary>
    /// <returns></returns>
    protected List<string> GetOwnRights(int userID)
    {
        if (HttpContext.Current.Items[userOwnRightKey] == null)
        {
            var list = new UserInfoBLL().GetUserPermissions(userID);
            HttpContext.Current.Items[userOwnRightKey] = list;
        }
        return (List<string>)HttpContext.Current.Items[userOwnRightKey];
    }
}