using DAL.FlightPlan;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FlightPlan
{
    public class LZCodeUnitity
    {
        RepetitivePlanDAL dal = new RepetitivePlanDAL();
        private static object _lock = new object();
        private static string[] array = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static ZHCC_GAPlanEntities context = new ZHCC_GAPlanEntities();
        public static string GetLZCode()
        {
            lock (_lock)
            {
                var dt = DateTime.Now.Date;
                var data = context.Set<LZCodeGenerate>().Where(m => m.CurrentDate >= dt).FirstOrDefault();
                var _code = "A";
                if (data != null)
                {
                    if (data.LZCode.Length > 7)
                    {
                        var _tempCode = data.LZCode.Substring(9, 1).ToUpper();
                        if (array.Contains(_tempCode))
                        {
                            var index = array.ToList().IndexOf(_tempCode);
                            _code = array[index + 1];
                        }
                    }
                    data.LZCode = $"{dt.ToString("yyyyMMdd")}_{_code}_KGB";
                    context.Entry(data).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    LZCodeGenerate entity = new LZCodeGenerate()
                    {
                        LZCode = $"{dt.ToString("yyyyMMdd")}_{_code}_KGB",
                        CurrentDate = dt
                    };
                    context.Entry(entity).State = EntityState.Added;
                     context.SaveChanges();
                }

                return $"{dt.ToString("yyyyMMdd")}_{_code}_KGB";
            }
        }
    }
}
