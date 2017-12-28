using BLL.FlightPlan;
using DAL.BasicData;
using DAL.FlightPlan;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Untity;
using System.Linq;
using System.Data.Entity;
namespace BLL.BasicData
{
    public class CompanyBLL
    {
        private CompanyDAL _dal = new CompanyDAL();
        WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
        WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
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
        public List<Company> GetList(Expression<Func<Company, bool>> where)
        {
            return _dal.FindList(where ,m => m.CompanyID, false);
        }

        #region 审核流程
        public bool Submit(int id, int userid, string username)
        {
            try
            {
                insdal.DeleteActualSteps(id, (int)TWFTypeEnum.CompanySummary);
                wftbll.CreateWorkflowInstance((int)TWFTypeEnum.CompanySummary, id, userid, username);
                insdal.Submit(id, (int)TWFTypeEnum.CompanySummary, "", t =>
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
                insdal.Submit(id, (int)TWFTypeEnum.CompanySummary, comment, t =>
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
                insdal.Terminate(id, (int)TWFTypeEnum.CompanySummary, comment, t =>
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
        public List<Company> GetTopList(int top, Expression<Func<Company, bool>> where)
        {
            ZHCC_GAPlanEntities context = new ZHCC_GAPlanEntities();
            return context.Set<Company>().Where(where).OrderByDescending(m => m.Catalog==1&&m.State=="end").AsNoTracking().Take(top).ToList();
        }
    }
}