using DAL.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Model.EF;
using System.Transactions;
using Untity;
using RIPS.Util.Collections;
using Newtonsoft.Json;
using Model.FlightPlan;

namespace BLL.FlightPlan
{
    public class CurrentPlanBLL
    {
        CurrentFlightPlanDAL dal = new CurrentFlightPlanDAL();
        vCurrentPlanDAL vdal = new vCurrentPlanDAL();
        WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
        WorkflowNodeInstanceDAL instal = new WorkflowNodeInstanceDAL();

        public void ActionWithTrans(Action act)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                try
                {
                    act();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
        public bool Add(CurrentFlightPlan model)
        {
            return dal.Add(model) > 0;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(CurrentFlightPlan model)
        {
            return dal.Update(model, false, "DeleteFlag") > 0;
        }
        /// <summary>
        /// 更新某些字段
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public bool Update(CurrentFlightPlan model, params string[] propertyNames)
        {
            return dal.Update(model, propertyNames) > 0;
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Submit(Guid planid, int userid, string username)
        {
            //ActionWithTrans(() =>
            //{
            CurrentFlightPlan model = new CurrentFlightPlan();
            model.FlightPlanID = planid.ToString();
            model.ActorID = userid;
            model.PlanState = "";
            dal.Add(model);

            var currPlanId = model.CurrentFlightPlanID;
            wftbll.CreateWorkflowInstance((int)TWFTypeEnum.CurrentPlan, currPlanId, userid, username);
            instal.Submit(currPlanId, (int)TWFTypeEnum.CurrentPlan, userid, username, "", workPlan =>
            {
                dal.Update(new CurrentFlightPlan { ActorID = workPlan.Actor.Value, PlanState = workPlan.PlanState, CurrentFlightPlanID = workPlan.PlanID }, "ActorID", "PlanState");
            });
            //});

            return true;
        }
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Audit(Guid planid, string comment, int userid, string userName)
        {
            instal.Submit(planid, (int)TWFTypeEnum.CurrentPlan, userid, userName, comment, workPlan =>
            {
                dal.Update(new CurrentFlightPlan { ActorID = workPlan.Actor, PlanState = workPlan.PlanState, CurrentFlightPlanID = workPlan.PlanID, Creator = userid, CreateTime = DateTime.Now }, "ActorID", "PlanState", "CreateUserId", "CreateTime");
            });
            return true;
        }
        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Terminate(Guid planid, string comment, int userid, string userName)
        {
            instal.Terminate(planid, (int)TWFTypeEnum.CurrentPlan, userid, userName, comment, workPlan =>
            {
                dal.Update(new CurrentFlightPlan { ActorID = workPlan.Actor, PlanState = workPlan.PlanState, CurrentFlightPlanID = workPlan.PlanID, Creator = userid, CreateTime = DateTime.Now }, "ActorID", "PlanState", "CreateUserId", "CreateTime");
            });
            return true;
        }
        /// <summary>
        /// 按分页获取记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="rowCount"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<vCurrentPlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<vCurrentPlan, bool>> where)
        {
            return vdal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.FlightPlanID, true);
        }
        /// <summary>
        /// 按条件获取记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<vCurrentPlan> GetList(Expression<Func<vCurrentPlan, bool>> where)
        {
            return vdal.FindList(where, m => m.CreateTime, true);
        }
        /// <summary>
        /// 获取单行记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public vCurrentPlan Get(Guid id)
        {
            return vdal.Find(u => u.CurrentFlightPlanID == id);
        }
        public vCurrentPlan GetByCurrid(Guid id)
        {
            return vdal.Find(u => u.CurrentFlightPlanID == id);
        }
        public CurrentFlightPlan GetEntity(Guid id)
        {
            return dal.Find(u => u.CurrentFlightPlanID == id);
        }

        public int GetCurrentUnSubmitNum(int userId)
        {
            return dal.GetCurrentUnSubmitNum(userId);
        }

        public int GetCurrentUnAuditNum(int userId)
        {
            return dal.GetCurrentUnAuditNum(userId);
        }

        public int GetCurrentSubmitNum(int userId)
        {
            return dal.GetCurrentSubmitNum(userId);
        }

        public int GetCurrentAuditNum(int userId)
        {
            return dal.GetCurrentAuditNum(userId);
        }

        public int GetCurrentSubmitNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetCurrentSubmitNum(userId, beginTime, endTime);
        }

        public int GetCurrentAuditNum(int userId, DateTime beginTime, DateTime endTime)
        {
            return dal.GetCurrentAuditNum(userId, beginTime, endTime);
        }
        public List<FlightPlanStatistics> GetList(int pageIndex, int pageSize, out int pageCount, DateTime started, DateTime ended)
        {
            try
            {
                List<FlightPlanStatistics> fplist = JsonConvert.DeserializeObject<List<FlightPlanStatistics>>(JsonConvert.SerializeObject(new FlightPlanDAL().GetFullTimeFlightStatistics(started, ended)));
                fplist = JsonConvert.DeserializeObject<List<FlightPlanStatistics>>(JsonConvert.SerializeObject(fplist.GroupBy(x => new { x.Creator, x.CompanyName }).Select(group => new {
                    Creator = group.Key.Creator,
                    CompanyName = group.Key.CompanyName,
                    AircraftNum = group.Sum(p => p.AircraftNum),
                    SecondDiff = group.Sum(p => p.SecondDiff)
                }).ToList()));
                pageCount = fplist.Count;
                fplist = fplist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return fplist;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }
        public List<vGetCurrentPlanNodeInstance> GetList(int pageIndex, int pageSize, out int rowCount, int Creator, DateTime started, DateTime ended)
        {
            return new FlightPlanDAL().GetCurrentPlanNodeInstanceList(pageIndex, pageSize, out rowCount, Creator, started, ended);
        }

        public int GetFlyNum(string company)
        {
            return dal.GetFlyNum(company);
        }

        public int GetFlyNum(string company, DateTime begin, DateTime end)
        {
            return dal.GetFlyNum(company, begin, end);
        }

        public int GetFlyTime(string company)
        {
            return dal.GetFlyTime(company);
        }

        public int GetFlyTime(string company, DateTime begin, DateTime end)
        {
            return dal.GetFlyTime(company, begin, end);
        }
    }
}
