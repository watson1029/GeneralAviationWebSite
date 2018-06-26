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
    public class SupplyDemandBLL
    {
        SupplyDemandDAL dal = new SupplyDemandDAL();
        WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
        WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
        public bool Delete(string ids)
        {
            return dal.BatchDelete(ids) > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SupplyDemandInfo model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SupplyDemandInfo model)
        {
            return dal.Update(model) > 0;
        }

        public bool Submit(int id, int userid, string username)
        {
            //try
            //{
            //    wftbll.CreateWorkflowInstance((int)TWFTypeEnum.SupplyDemand, id, userid, username);
            //    insdal.Submit(id, (int)TWFTypeEnum.SupplyDemand, "", t =>
            //    {
            //        dal.Update(new Model.EF.SupplyDemandInfo { ActorID = t.Actor, State = t.PlanState, ID = t.PlanID }, "ActorID", "State");
            //    });

                return true;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Audit(int id, string comment)
        {
            //try
            //{
            //    insdal.Submit(id, (int)TWFTypeEnum.SupplyDemand, comment ?? "", t =>
            //    {
            //        dal.Update(new Model.EF.SupplyDemandInfo { ActorID = t.Actor, State = t.PlanState, ID = t.PlanID }, "ActorID", "State");
            //    });
                return true;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Terminate(int id, string comment)
        {
            //try
            //{
            //    insdal.Terminate(id, (int)TWFTypeEnum.SupplyDemand, comment, t =>
            //    {
            //        dal.Update(new Model.EF.SupplyDemandInfo { ActorID = t.Actor, State = t.PlanState, ID = t.PlanID }, "ActorID", "State");
            //    });
                return true;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public List<SupplyDemandInfo> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<SupplyDemandInfo, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.CreateTime, false);
        }
        public List<SupplyDemandInfo> GetList(Expression<Func<SupplyDemandInfo, bool>> where)
        {
            return dal.FindList(where, m => m.CreateTime, false);
        }

        public SupplyDemandInfo Get(int id)
        {
            return dal.Find(u => u.ID == id);
        }
        public List<SupplyDemandInfo> GetTopList(int top, Expression<Func<SupplyDemandInfo, bool>> where)
        {
            ZHCC_GAPlanEntities context = new ZHCC_GAPlanEntities();
            return context.Set<SupplyDemandInfo>().Where(where).OrderByDescending(m => m.CreateTime).AsNoTracking().Take(top).ToList();
        }
    }
}
