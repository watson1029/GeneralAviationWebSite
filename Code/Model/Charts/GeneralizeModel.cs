using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Charts
{
    public class GeneralizeModel
    {
        public static string MyUnSubmitRepetPlanCheck = "长期计划提交数量";
        public static string MyAuditRepetPlanCheck = "长期计划审批数量";
        public static string MyUnSubmitFlightPlanCheck = "飞行计划提交数量";
        public static string MyAuditFlightPlanCheck = "飞行计划审批数量";
        public static string usrpColor = "#1B9AF7";
        public static string arpColor = "#FF4351";
        public static string usfpColor = "#FEAE1B";
        public static string afpColor = "#7B72E9";
        public List<string> NameItem { get; set; }
        public List<string> ColorItem { get; set; }
        public List<string> TimeItem { get; set; }
        public List<DateTime> TimeParse { get; set; }
        public List<GeneralizeData> BarData { get; set; }
        public GeneralizeModel()
        {
            this.NameItem = new List<string>();
            this.ColorItem = new List<string>();
            this.TimeItem = new List<string>();
            this.BarData = new List<GeneralizeData>();
            this.TimeParse = new List<DateTime>();
            TimeItem.Add($"{DateTime.Now.AddDays(-27).ToString("MM月dd日")} - {DateTime.Now.AddDays(-21).ToString("MM月dd日")}");
            TimeItem.Add($"{DateTime.Now.AddDays(-20).ToString("MM月dd日")} - {DateTime.Now.AddDays(-14).ToString("MM月dd日")}");
            TimeItem.Add($"{DateTime.Now.AddDays(-13).ToString("MM月dd日")} - {DateTime.Now.AddDays(-7).ToString("MM月dd日")}");
            TimeItem.Add($"{DateTime.Now.AddDays(-6).ToString("MM月dd日")} - {DateTime.Now.ToString("MM月dd日")}");
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeParse.Add(date.AddDays(-27));
            TimeParse.Add(date.AddDays(-20));
            TimeParse.Add(date.AddDays(-13));
            TimeParse.Add(date.AddDays(-6));
        }
    }

    public class GeneralizeData
    {
        public List<int> data { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public GeneralizeData()
        {
            this.data = new List<int>();
            this.type = "bar";
        }
    }
}
