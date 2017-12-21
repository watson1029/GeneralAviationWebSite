using DAL.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity;
using System.Linq.Expressions;
using Model.EF;
using System.Transactions;

namespace BLL.FlightPlan
{ 
    public class CurrentPlanBLL
    {
        CurrentFlightPlanDAL dal = new CurrentFlightPlanDAL();
        VCurrentPlanDAL vdal = new VCurrentPlanDAL();
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
            return dal.Update(model) > 0;
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
        public bool Submit(int planid,int userid, string username)
        {
            //ActionWithTrans(() =>
            //{
                CurrentFlightPlan model = new CurrentFlightPlan();
                model.FlightPlanID = planid;
                model.ActorID = userid;
                model.PlanState = "";
                dal.Add(model);

                int currPlanId = model.CurrentFlightPlanID;
                wftbll.CreateWorkflowInstance((int)TWFTypeEnum.CurrentPlan, currPlanId, userid, username);
                instal.Submit(currPlanId, (int)TWFTypeEnum.CurrentPlan, "", workPlan =>
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
        public bool Audit(int planid, string comment)
        {
            try
            {
                instal.Submit(planid, (int)TWFTypeEnum.CurrentPlan, comment, workPlan => { });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Terminate(int planid,string comment)
        {
            try
            {
                instal.Terminate(planid, (int)TWFTypeEnum.CurrentPlan, comment, workPlan => { });
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
        public List<V_CurrentPlan> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<V_CurrentPlan, bool>> where)
        {
            return vdal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.FlightPlanID, true);
        }
        /// <summary>
        /// 按条件获取记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<V_CurrentPlan> GetList(Expression<Func<V_CurrentPlan, bool>> where)
        {
            return vdal.FindList(where, m => m.FlightPlanID, true);
        }
        /// <summary>
        /// 获取单行记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public V_CurrentPlan Get(int id)
        {
            return vdal.Find(u => u.FlightPlanID == id);
        }
        public V_CurrentPlan GetByCurrid(int id)
        {
            return vdal.Find(u => u.CurrentFlightPlanID == id);
        }
    }
}
