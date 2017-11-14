using BLL.SystemManagement;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using Untity;


    public class LoginUtil
    {   
        /// <summary>
        /// 系统用于存储的当前会话人的Session名称
        /// </summary>
        public static string LoginUserSessionName
        {
            get { return ConfigurationManager.AppSettings["LoginUserSessionName"]; }
        }
        public static LoginResultEnum GALogin(string userName, string password, out string msg)
        {
            msg = string.Empty;
            //是否登录成功
            bool isSuccess = false;
            //登录结果
            LoginResultEnum loginResult = LoginResultEnum.LoginSuccess;
            try
            {
                UserInfo user = GetUser(userName);
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
                    loginResult = LoginResultEnum.OtherError;
                    return loginResult;
                }

                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[LoginUserSessionName] = user;

                }
                //保存用户信息到Cookie
                SaveUserInfoToCookie(user.UserName, false);

                isSuccess = true;
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
        /// <summary>
        /// 将用户信息保存到Cookie中
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="isPersistent">是否持久化Cookie</param>
        protected static void SaveUserInfoToCookie(string userName, bool isPersistent)
        {

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                                             userName,
                                                                             DateTime.Now,
                                                                             DateTime.Now.AddMinutes(80),
                                                                             isPersistent,
                                                                             userName,
                                                                             FormsAuthentication.FormsCookiePath);
            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie myCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //如果用户使用域名访问，则在cookie上增加域名信息
            if (IsValidDomain(HttpContext.Current.Request.Url.Host))
            {
                myCookie.Domain = FormsAuthentication.CookieDomain;
            }
            if (isPersistent)
            {
                myCookie.Expires = ticket.Expiration;
            }

            // Create the cookie.
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        /// <summary>
        /// 是否为合法的域名
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        public static bool IsValidDomain(string domain)
        {
            if (string.IsNullOrEmpty(FormsAuthentication.CookieDomain))
                return false;

            return domain.IndexOf(FormsAuthentication.CookieDomain, StringComparison.CurrentCultureIgnoreCase) > -1;
        }
        /// <summary>
        /// 获取登录用户
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns></returns>
        public static UserInfo GetUser(string userName)
        {
            var userInfo = new UserInfo();
      //      var userInfo = UserInfoBLL.GetUserByName(userName);

            return userInfo;
        }
    }
