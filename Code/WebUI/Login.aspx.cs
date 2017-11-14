using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void IBt_Submit_Click(object sender, ImageClickEventArgs e)
    {
        var userName = txtUserName.Text.Trim();
        var password = this.Request["htxtPassword"]; //登录密码改在客户用js 的DES加密

        if (string.IsNullOrEmpty(userName))
        {
            Response.Write("<script>alert('请输入用户名！')</script>");
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            Response.Write("<script>alert('请输入密码！')</script>");
            return;
        }
        //解密的密码
        var PPassword = DES.uncMe(password, userName);

        string msg;
        //将明文密码转化为MD5加密
        password = CryptTools.HashPassword(PPassword);
        LoginResultEnum loginResult = LoginUtil.GALogin(StringSafeFilter.Filter(userName), StringSafeFilter.Filter(password.ToUpper()), out msg);

        var user = HttpContext.Current.Session[LoginUtil.LoginUserSessionName] as UserInfo;
        // 记录用户登录操作日志
        if (user != null)
        {
            if (loginResult == LoginResultEnum.LoginSuccess)
            {
                //ActionLogBLL.LogUserAction(ActionLogType.Login, OperateResult.Succeed, user, "", "", user.UserID,
                //    string.Format("用户登录通航服务站。用户帐号：{0}，手机号码{1}。", user.UserName, user.Mobile));
            }
            else
            {
                //ActionLogBLL.LogUserAction(ActionLogType.Login, OperateResult.Failed, user, "", "", user.UserID,
                //    string.Format("用户登录通航服务站。用户帐号：{0}，手机号码{1}。", user.UserName, user.Mobile));
            }
        }

        if (loginResult == LoginResultEnum.NoUser ||
            loginResult == LoginResultEnum.OtherError || loginResult == LoginResultEnum.PasswordError || loginResult == LoginResultEnum.LockUser )
        {
            Response.Write("<script>alert('错误！')</script>");
            return;
        }

        //登录成功
        Response.Redirect("~/Index.aspx");
    }

}