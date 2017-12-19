using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untity
{
    public class OrderHelper
    {
        private static readonly object locker = new object();
        public static string GenerateId(OrderTypeEnum orderType, string code3)
        {
            lock (locker)
            {
                return "GA_" +code3 +"_" +orderType.ToString() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1000, 9999);

            }
        }
    }
    public enum OrderTypeEnum
    {
        /// <summary>
        /// 长期飞行计划
        /// </summary>
        RP,
        /// <summary>
        /// 飞行计划
        /// </summary>
        FP,
        /// <summary>
        /// 当日飞行计划
        /// </summary>
        CP,
        /// <summary>
        /// 供求信息审批
        /// </summary>
        SD,
        /// <summary>
        /// 通航公司介绍
        /// </summary>
        CS 
    }
}
