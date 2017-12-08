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

namespace BLL.SupplyDemandInformation
{
    public class SupplyDemandBLL
    {
        SupplyDemandDAL dal = new SupplyDemandDAL();
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
            try
            {
                WorkflowTemplateBLL.CreateWorkflowInstance((int)TWFTypeEnum.SupplyDemand, id, userid, username);
                WorkflowNodeInstanceDAL.Submit(id, "", t =>
                {
                    dal.Update(new Model.EF.SupplyDemandInfo { ActorID = t.Actor, State = t.PlanState, ID = t.PlanID }, "ActorID", "PlanState");
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SupplyDemandInfo> GetList(int pageIndex, int pageSize, out int pageCount, out int rowCount, Expression<Func<SupplyDemandInfo, bool>> where)
        {
            return dal.FindPagedList(pageIndex, pageSize, out pageCount, out rowCount, where, m => m.ID, true);
        }

        public SupplyDemandInfo Get(int id)
        {
            return dal.Find(u => u.ID == id);
        }
    }
}
