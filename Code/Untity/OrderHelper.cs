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
        public static string GenerateId(string code3)
        {
            lock (locker)
            {
                return "GA" + code3 + DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(1000, 9999);

            }
        }
    }
}
