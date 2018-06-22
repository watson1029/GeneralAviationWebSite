using BLL.FlightPlan;
using DAL.FlightPlan;
using DAL.SupplyDemandInformation;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Untity;
using System.Data.Entity;
namespace BLL.SupplyDemandInformation
{
    public class CompanySummaryBLL
    {
        //CompanySummaryDAL dal = new CompanySummaryDAL();
        //WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
        //WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
        //public bool Delete(string ids)
        //{
        //    return dal.BatchDelete(ids) > 0;
        //}
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public bool Add(CompanySummary model)
        //{
        //    return dal.Add(model) > 0;
        //}

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(CompanySummary model)
        //{
        //    return dal.Update(model) > 0;
        //}

        //public bool Submit(int id, int userid, string username)
        //{
        //    try
        //    {
        //        wftbll.CreateWorkflowInstance((int)TWFTypeEnum.CompanySummary, id, userid, username);
        //        insdal.Submit(id, (int)TWFTypeEnum.CompanySummary, "", t =>
        //        {
        //            dal.Update(new Model.EF.CompanySummary { ActorID = t.Actor, State = t.PlanState, ID = t.PlanID }, "ActorID", "State");
        //        });

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// 审核通过
        ///// </summary>
        ///// <param name="planid"></param>
        ///// <param name="comment"></param>
        ///// <returns></returns>
        //public bool Audit(int id, string comment)
        //{
        //    try
        //    {
        //        insdal.Submit(id, (int)TWFTypeEnum.CompanySummary, comment ?? "", t =>
        //        {
        //            dal.Update(new Model.EF.CompanySummary { ActorID = t.Actor, State = t.PlanState, ID = t.PlanID }, "ActorID", "State");
        //        });
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// 审核不通过
        ///// </summary>
        ///// <param name="planid"></param>
        ///// <param name="comment"></param>
        ///// <returns></returns>
        //public bool Terminate(int id, string comment)
        //{
        //    try
        //    {
        //        insdal.Terminate(id, (int)TWFTypeEnum.CompanySummary, comment, t =>
        //        {
        //            dal.Update(new Model.EF.CompanySummary { ActorID = t.Actor, State = t.PlanState, ID = t.PlanID }, "ActorID", "State");
        //        });
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<CompanySummary> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<CompanySummary, bool>> where)
        //{
        //    return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.ID, true);
        //}

        //public CompanySummary Get(int id)
        //{
        //    return dal.Find(u => u.ID == id);
        //}

        public List<CompanySummary> GetTopList(int top, Expression<Func<CompanySummary, bool>> where)
        {
            ZHCC_GAPlanEntities context = new ZHCC_GAPlanEntities();
            return context.Set<CompanySummary>().Where(where).OrderByDescending(m => m.ID).AsNoTracking().Take(top).ToList();
        }

        //public List<CompanySummary> GetList(Expression<Func<CompanySummary, bool>> where)
        //{
        //    return dal.FindList(where, m => m.ModifiedTime, true);
        //}
    }
}
