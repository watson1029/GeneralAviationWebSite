using BLL.BasicData;
using BLL.Log;
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

    public static LoginResultEnum GALogin(string userName, string password, bool rememberme, out string msg)
    {
        UserInfoBLL bll = new UserInfoBLL();
        msg = "登录成功！";
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
                IsGeneralAviation = user.IsGeneralAviation,
                CompanyCode3 = user.CompanyCode3,
                RoleName = bll.GetRoleNameList(user.ID)
            };
            if (!string.IsNullOrEmpty(user.CompanyCode3))
            {
                var com = bll.GetCompany(user.CompanyCode3);
                if (com != null)
                {
                    userInfoCookie.CompanyName = com.CompanyName;
                }
            }


            if (!UserLoginService.Instance.InsertOrUpdateLoginInfo(userInfoCookie, rememberme))
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
            LoginLogBLL loginbll = new LoginLogBLL();
            //记用户登录日志
            LoginLog entity = new LoginLog()
            {
                Msg = msg,
                UserName = userName,
                LoginTime = DateTime.Now,
                IPAddress = IPAddressHelper.GetClientIp()
            };
            loginbll.Add(entity);
        }
    }


}
