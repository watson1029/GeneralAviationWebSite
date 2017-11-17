using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untity
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            this.Msg = "操作成功";
            this.Code = "200";
            this.IsSuccess = true;
            this.Attr = new Dictionary<string, string>();
        }
        /// <summary>
        /// 获取ajax返回的json字符串
        /// </summary>
        /// <returns></returns>
        public string ToJsonString()
        {
            string json = "{";
            json += "\"isSuccess\":" + IsSuccess.ToString().ToLower() + ",\"msg\":\"" + Msg + "\"";
            if (!string.IsNullOrEmpty(Code))
            {
                json += ",\"code\":\"" + Code + "\"";
            }
            foreach (string key in Attr.Keys)
            {
                string val = Attr[key];
                if (val == null)
                {
                    val = "";
                }
                if (val != null && val.ToLower() != "true" && val.ToLower() != "false" && !val.StartsWith("[") && !val.StartsWith("{"))
                {
                    val = "\"" + val + "\"";
                }
                json += ",\"" + key + "\":" + val + "";
            }
            json += "}";
            return json;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get;
            set;
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg
        {
            get;
            set;
        }
        /// <summary>
        /// ajax返回自定义业务编码
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        /// <summary>
        /// ajax自定义返回信息
        /// </summary>
        public Dictionary<string, string> Attr
        {
            get;
            set;
        }
    }
}
