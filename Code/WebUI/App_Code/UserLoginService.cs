using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Untity;

public class UserLoginService
{
    private static UserLoginService _instance;
    public static UserLoginService Instance
    {
        get { return _instance ?? (_instance = new UserLoginService()); }
    }
    public UserInfoCookie GetUser()
    {
        var token = IdentityService.Instance.GetCurrentIdentity().Name;
        return GetLoginInfo(token);
    }

    /// <summary>
    /// 获取登陆的持久化数据
    /// </summary>
    /// <returns></returns>
    public UserInfoCookie GetLoginInfo(string token)
    {
        try
        {
            if (!string.IsNullOrEmpty(token))
            {
                var fpUserInfoCookieStr = DES.DecryptString(token);

                if (!string.IsNullOrEmpty(fpUserInfoCookieStr))
                {
                    return JsonConvert.DeserializeObject<UserInfoCookie>(fpUserInfoCookieStr);

                }
            }
        }
        catch (Exception ex)
        {
            //  Log.Error(ex, "获取登录用户信息失败", ex.Message);
        }

        return null;
    }
    /// <summary>
    /// 登陆并记录持久化信息
    /// </summary>
    /// <returns></returns>
    public bool InsertOrUpdateLoginInfo(UserInfoCookie userInfoCookie)
    {
        try
        {
            var fpUserInfoCookieStr = JsonConvert.SerializeObject(userInfoCookie);
            fpUserInfoCookieStr = DES.EncryptString(fpUserInfoCookieStr);
            FormsAuthentication.SetAuthCookie(fpUserInfoCookieStr, true);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
    /// <summary>
    /// 清除当前用户Cookie
    /// </summary>
    public void CleanUserCookie()
    {
        IdentityService.Instance.SignOut();
    }
}