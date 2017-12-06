using BLL.SystemManagement;
using Model;
using Model.EF;
using Model.SystemManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using Untity;


public class LoginUtil
{

    public static LoginResultEnum GALogin(string userName, string password, out string msg)
    {
        UserInfoBLL bll = new UserInfoBLL();
        msg = string.Empty;
        //登录结果
        LoginResultEnum loginResult = LoginResultEnum.LoginSuccess;
        try
        {
            UserInfo user = bll.Get(userName);
            if (user == null)
            {
                msg = "用户不存在！";
                loginResult = LoginResultEnum.NoUser;
                return loginResult;
            }

            if (password != user.Password)
            {
                msg = "密码错误！";
                loginResult = LoginResultEnum.PasswordError;
                return loginResult;
            }

            if (user.Status != 0)
            {
                msg = "用户被冻结或注销";
                loginResult = LoginResultEnum.LockUser;
                return loginResult;
            }
            var userInfoCookie = new UserInfoCookie
            {
                ID = user.ID,
                UserName = user.UserName,
                CreateTime = user.CreateTime,
                Status = user.Status,
                IsGeneralAviation=user.IsGeneralAviation,
                CompanyCode3=user.CompanyCode3

            };

            if (!UserLoginService.Instance.InsertOrUpdateLoginInfo(userInfoCookie))
            {
                throw new Exception();
            }

            return loginResult;
        }
        catch (Exception ex)
        {
            msg = "系统错误,无法登录";
            loginResult = LoginResultEnum.OtherError;
            return loginResult;
        }
        finally
        {
            //记用户登录日志
            //   UserLoginLogBLL.Log(userName, "-1", isSuccess, loginResult, DateTime.Now, msg);
        }
    }


}
