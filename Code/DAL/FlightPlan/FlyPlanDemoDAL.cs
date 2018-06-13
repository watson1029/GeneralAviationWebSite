using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FlightPlan
{
    public class FlyPlanDemoDAL : DBHelper<FlyPlanDemo>
    {
        /// <summary>
        /// 当天的飞行计划是否已经生成过？
        /// </summary>
        /// <param name="repetID"></param>
        /// <returns></returns>
        public bool IsExist(string repetID)
        {
            DateTime time = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);
            var linq = from t in context.FlyPlanDemo
                       where t.RepetPlanID == repetID
                       where t.PlanDate == time
                       select t;
            return linq.Count() > 0 ? true : false;
        }

        public FlyPlanDemo GetFlyPlan(string flyId)
        {
            var linq = from t in context.FlyPlanDemo
                       where t.FlyPlanID == flyId
                       select t;
            return linq.FirstOrDefault();
        }

        public RepetPlanNew GetRepetPlan(string flyId)
        {
            var linq = from t in context.RepetPlanNew
                       join m in context.FlyPlanDemo on t.RepetPlanID.ToString() equals m.RepetPlanID
                       where m.FlyPlanID == flyId
                       select t;
            return linq.FirstOrDefault();
        }
    }
}
