using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untity
{
    public static class StringExtension
    {
        public static string Substring(this string str, int length, string endOf)
        {
            if (!string.IsNullOrEmpty(str) && ((length > 0) && (length < str.Length)))
            {
                return (str.Substring(0, length) + endOf);
            }
            return str;
        }
        public static string FormatTo(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }


        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 截取方法
        /// </summary>
        /// <param name="obj">截取对象</param>
        /// <param name="n">最大字节数</param>
        /// <param name="bh">尾巴</param>
        /// <returns></returns>
        public static string StringFormat(object obj, int n, string bh)
        {
            if (obj == null)
                return "";
            else
            {
                string str = obj.ToString();
                string temp = string.Empty;
                if (System.Text.Encoding.Default.GetByteCount(str) <= n) //如果长度比需要的长度n小,返回原字符串
                {
                    return str;
                }
                else
                {
                    int t = 0;
                    char[] q = str.ToCharArray();
                    for (int i = 0; i < q.Length; i++)
                    {
                        if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5) //是否汉字
                        {
                            temp += q[i];
                            t += 2;
                        }
                        else
                        {
                            temp += q[i];
                            t += 1;
                        }
                        if (t >= n)
                        {
                            break;
                        }
                    }
                    return (temp + bh);
                }
            }
        }
    }
}
