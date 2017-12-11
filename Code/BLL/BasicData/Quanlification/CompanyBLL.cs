using BLL.FlightPlan;
using DAL.BasicData;
using DAL.FlightPlan;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Untity;

namespace BLL.BasicData
{
    public class CompanyBLL
    {
        private CompanyDAL _dal = new CompanyDAL();

        public int Delete(string ids)
        {
            return _dal.BatchDelete(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Company model)
        {
            return _dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Company model)
        {
            return _dal.Update(model);
        }
        public Company Get(int id)
        {
            return _dal.Find(m => m.CompanyID == id);
        }
        public List<Company> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<Company, bool>> where)
        {
            return _dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.CompanyID, true);
        }
        public List<Company> GetList()
        {
            return _dal.FindList(m => m.CompanyID, false);
        }

        #region 审核流程
        public bool Submit(int id, int userid, string username)
        {
            try
            {
                WorkflowTemplateBLL.CreateWorkflowInstance((int)TWFTypeEnum.CompanySummary, id, userid, username);
                WorkflowNodeInstanceDAL.Submit(id,(int)TWFTypeEnum.SupplyDemand, "", t =>
                {
                    _dal.Update(new Model.EF.Company { ActorID = t.Actor, State = t.PlanState, CompanyID = t.PlanID }, "ActorID", "State");
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Audit(int id, string comment)
        {
            try
            {
                WorkflowNodeInstanceDAL.Submit(id,(int)TWFTypeEnum.CompanySummary,comment, t =>
                {
                    _dal.Update(new Model.EF.Company { ActorID = t.Actor, State = t.PlanState, CompanyID = t.PlanID }, "ActorID", "State");
                });
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
        public bool Terminate(int id, string comment)
        {
            try
            {
                WorkflowNodeInstanceDAL.Terminate(id,(int)TWFTypeEnum.CompanySummary, comment, t =>
                {
                    _dal.Update(new Model.EF.Company { ActorID = t.Actor, State = t.PlanState, CompanyID = t.PlanID }, "ActorID", "State");
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}