using BLL.Log;
using Model.EF;
using Model.SystemManagement;
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
            result.Msg = "证码错误，请重新输入！";
            Response.Write(result.ToJsonString());
            Response.ContentType = "application/json";
            Response.End();
        }
        //解密的密码
        var PPassword = DES.uncMe(password, userName);
        string msg;
        //将明文密码转化为MD5加密
        password = CryptTools.HashPassword(PPassword);
        LoginResultEnum loginResult = LoginUtil.GALogin(StringSafeFilter.Filter(userName), StringSafeFilter.Filter(password.ToUpper()),false, out msg);

        if (loginResult == LoginResultEnum.LoginSuccess)
        {
            result.IsSuccess = true;
            result.Msg = msg;

        }

        if (loginResult == LoginResultEnum.NoUser ||
            loginResult == LoginResultEnum.OtherError || loginResult == LoginResultEnum.PasswordError || loginResult == LoginResultEnum.LockUser)
        {
            result.Msg = msg;
        }

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }


}