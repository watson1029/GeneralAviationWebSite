using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel.FlightPlan
{
    public class FileAnalysisReg
    {
        public static string dateReg = "([0-9]{4}\u5e74)?[0-9]{1,2}\u6708[0-9]{1,2}\u65e5";

        public static string companyReg = @"\w+(?=\u516c\u53f8\u5173\u4e8e)";

        public static string taskReg = "(?<=\u6267\u884c)[^\u98de\u884c\u7684\u8bf7\u793a]+";

        public static string airplaneTypeReg = @"(?<=(\u4f7f\u7528\u673a\u578b[\u3001\、]\u673a\u53f7[\uff1a\:]|\u4f7f\u7528\u673a\u578b[\uff1a\:]))[^\u3002]+";

        public static string areaReg = @"(?<=(\u5173\u4e8e\u5728))[.\s\S]*?(?=(\u5730\u533a))";
        public static string airportReg = @"(?<=(\uff08\u56db\u5b57\u7801\u6216\u7ecf\u7eac\u5ea6\uff09\uff1b))[.\s\S]*?(?=(\u56db\u3001\u822a\u7ebf\u53ca\u4f5c\u4e1a\u533a))";
        public static string airportReg1 = @"[\u4e00-\u9fa5]+[\uff08\(][^\uff09]+(?=[\uff09\)])";
        public static string airportName ="^[\u4e00-\u9fa5]+";
        public static string code4 = "[a-zA-Z]{4}$";
        public static string lngAndLat= "N[0-9]{2}(\u00b0)?[0-9]{2}[\u2032\u02ca]?([0-9]{2}[\u2033\u301e]?)?E[0-9]{3}(\u00b0)?[0-9]{2}[\u2032\u02ca]?([0-9]{2}[\u2033\u301e]?)?";

        public static string airlineworkReg = @"(?<=(\u822a\u7ebf\u53ca\u4f5c\u4e1a\u533a))[.\s\S]*?(?=(\u4e94\u3001\u5b89\u5168\u8d23\u4efb))";

        public static string radius = @"(?<=(\u4e3a\u5706\u5fc3\u534a\u5f84))[.\s\S]*?(?=(\u516c\u91cc\u8303\u56f4\u5185))";

        public static string height = @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))";
        public static string ailineName = "^[\u4e00-\u9fa5_a-zA-Z0-9]+";
    }
}