using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Linq;
namespace Untity
{
    public static class EntityExtension
    {
        public static void  GetEntitySearchPars<T>(this object thisObj, HttpContext context)
        {
            var request = context.Request.Form;
            var getRequst = context.Request.Params;
            var type = typeof(T);
            var pis = FastType.Get(type).Setters;
            foreach (var p in pis)
            {
                if (request.AllKeys.Contains(p.Name, StringComparer.Create(Thread.CurrentThread.CurrentCulture, true)))
                {
                    var requestValue = request[p.Name];
                    p.SetValue(thisObj, requestValue.ConventToType(p.Info.PropertyType));
                }
                else if (getRequst.AllKeys.Contains(p.Name, StringComparer.Create(Thread.CurrentThread.CurrentCulture, true)))
                {
                    var requestValue = getRequst[p.Name];
                    p.SetValue(thisObj, requestValue.ConventToType(p.Info.PropertyType));
                }
            }
        }
    }
}
