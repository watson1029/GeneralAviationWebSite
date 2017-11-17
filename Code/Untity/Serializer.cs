using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Untity
{
    public class Serializer
    {     
        /// <summary>
        /// 返回处理过的时间的Json字符串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string JsonDate(object date)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(date, Formatting.Indented, timeConverter);
        }
    }



}