using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

public class IdentityService
{
    private IdentityService()
    {

    }

    private static IdentityService _userLoginService;
    public void SignOut()
    {
        if (HttpContext.Current.Session != null)
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }

        HttpContext.Current.Response.Cookies.Clear();
        FormsAuthentication.SignOut();
    }
    public void SignIn(string userName, bool createPersistentCookie)
    {
        FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
    }
    public static IdentityService Instance
    {
        get { return _userLoginService ?? (_userLoginService = new IdentityService()); }
    }
    public IIdentity GetCurrentIdentity()
    {
        return HttpContext.Current.User.Identity;
    }
    public bool IsSignedIn()
    {
        return HttpContext.Current.User.Identity.IsAuthenticated;
    }
}