using DAL.FlightPlan;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FlightPlan
{
    public class FlyPlanDemoBLL
    {
        private RepetitivePlanNewDAL repetdal = new RepetitivePlanNewDAL();
        private FlyPlanDemoDAL flydal = new FlyPlanDemoDAL();
        private BusyTimeDAL busydal = new BusyTimeDAL();
        /// <summary>
        /// 生成飞行计划
        /// </summary>
        /// <returns></returns>
        public bool GeneralFlyData()
        {
            try
            {
                //获取有效的长期计划
                var repetList = repetdal.GetAuditRepetPlan();
                foreach (var repet in repetList)
                {
                    //没生成过明天的飞行计划
                    if (!flydal.IsExist(repet.RepetPlanID.ToString()))
                    {
                        //生成避开繁忙时间的时间段
                        var busy = busydal.Get(new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day));
                        DateTime begintime = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);
                        DateTime endtime = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);
                        if (busy == null)
                        {
                            begintime = begintime.AddHours(8);
                            endtime = endtime.AddHours(10);
                        }
                        else
                        {
                            Random rd = new Random();
                            while (true)
                            {
                                DateTime rdtime = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, rd.Next(0, 22), 0, 0);
                                if (rdtime <= busy.BusyBeginTime.Value.AddHours(-2) || rdtime >= busy.BusyEndTime.Value)
                                {
                                    begintime = rdtime;
                                    endtime = rdtime.AddHours(2);
                                    break;
                                }
                            }
                        }
                        //生成明天的飞行计划
                        FlyPlanDemo model = new FlyPlanDemo()
                        {
                            FlyPlanID = Guid.NewGuid().ToString("N").ToUpper(),
                            RepetPlanID = repet.RepetPlanID.ToString(),
                            PlanDate = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day),
                            PlanBeginTime = begintime,
                            PlanEndTime = endtime,
                            CreateTime = DateTime.Now
                        };
                        //写入数据库
                        flydal.Add(model);
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<FlyPlanDemo> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<FlyPlanDemo, bool>> where)
        {
            return flydal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.PlanDate, false);
        }

        public FlyPlanDemo GetFlyPlan(string flyId)
        {
            return flydal.GetFlyPlan(flyId);
        }

        public RepetPlanNew GetRepetPlan(string flyId)
        {
            return flydal.GetRepetPlan(flyId);
        }

        public bool Add(FlyPlanDemo model)
        {
            return flydal.Add(model) > 0;
        }
        public bool Update(FlyPlanDemo model)
        {
            return flydal.Update(model) > 0;
        }
    }
}
