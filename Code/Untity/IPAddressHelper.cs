using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Untity
{
    public class IPAddressHelper
    {
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            var address = string.Empty;

            var x_forwarded_for = HttpContext.Current.Request.Headers["x-forwarded-for"];
            if (!string.IsNullOrEmpty(x_forwarded_for))
            {
                var arr = x_forwarded_for.Split(new[] { ',' });
                address = arr.Length > 1 ? arr[arr.Length - 2] : arr[0];
            }
            else
            {
                address = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return address;
        }
    }
}
