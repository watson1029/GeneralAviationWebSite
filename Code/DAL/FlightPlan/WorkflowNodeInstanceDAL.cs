using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Untity.DB;
using Model.EF;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL.FlightPlan
{
    public class WorkflowNodeInstanceDAL : DBHelper<ActualSteps>
    {
        WorkflowTplNodeDAL _dal = new WorkflowTplNodeDAL();
        public bool UpdateNodeInstance(List<WorkflowNodeInstance> nodesInstance)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var tempINst in nodesInstance)
                    {
                        Update(new ActualSteps { PrevID = tempINst.PrevId, NextID = tempINst.NextId, ID = tempINst.Id }, "PrevID", "NextID");
                    }
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }
        public bool UpdateFirstNode(WorkflowNodeInstance firstNodeInst, int userID, string userName)
        {
            try
            {
                firstNodeInst.State = WorkflowNodeInstance.StepStateType.Processing;
                var entity = new ActualSteps { ActorID = userID, ActorName = userName, State = (byte)firstNodeInst.State, ApplyTime = DateTime.Now, ID = firstNodeInst.Id };
                return Update(entity, "ActorID", "ActorName", "State", "ApplyTime") > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkflowNodeInstance Submit(int planId, int twfid, string comments, Action<WorkflowPlan> action)
        {
            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId, twfid);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            WorkflowNodeInstance nextInst = null;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Processed, Comments = comments, ActorTime = DateTime.Now, ID = currInst.Id }, "State", "Comments", "ActorName");
                    currInst.State = WorkflowNodeInstance.StepStateType.Processed;
                    //更新下一个节点状态为处理中
                    if (currInst.NextId != Guid.Empty)
                    {
                        nextInst = GetNodeInstance(currInst.NextId);
                        WorkflowTplNode tnode = _dal.GetNode(nextInst.StepId);
                        var auhtor = int.Parse(tnode.AuthorType);
                        var userInfo = context.Set<UserInfo>().Where(u => u.ID == auhtor).FirstOrDefault();
                        int actor = userInfo.ID;
                        //判断节点的活动所有者类型

                        Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Processing, ApplyTime = DateTime.Now, ActorID = userInfo.ID, ActorName = userInfo.UserName, ID = currInst.NextId }, "State", "ApplyTime", "ActorID", "ActorName");

                        action(new WorkflowPlan { Actor = actor, PlanState = tnode.StepName, PlanID = planId });
                    }
                    else
                    {
                        action(new WorkflowPlan { Actor = null, PlanState = "end", PlanID = planId });
                    }
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
            return nextInst;
        }

        public void UpdateRepetPlan(WorkflowPlan plan)
        {
            //  context.Database.UseTransaction(plan.CurrentTransaction);
            var entity = new RepetitivePlan() { ActorID = plan.Actor, PlanState = plan.PlanState, RepetPlanID = plan.PlanID };
            var entry = context.Entry(entity);
            entry.State = EntityState.Unchanged;
            foreach (string propertyName in new string[] { "ActorID", "PlanState" })
            {
                entry.Property(propertyName).IsModified = true;
            }
            context.Configuration.ValidateOnSaveEnabled = false;
            context.SaveChanges();
        }
        public void UpdateFlightPlan(WorkflowPlan plan)
        {
            var entity = new Model.EF.FlightPlan() { ActorID = plan.Actor, PlanState = plan.PlanState, FlightPlanID = plan.PlanID };

            var entry = context.Entry(entity);
            entry.State = EntityState.Unchanged;
            foreach (string propertyName in new string[] { "ActorID", "PlanState" })
            {
                entry.Property(propertyName).IsModified = true;
            }
            context.Configuration.ValidateOnSaveEnabled = false;
            context.SaveChanges();
        }

        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        public int Terminate(int planId, int twfid, string comments, Action<WorkflowPlan> func)
        {
            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId, twfid);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            int result = 0;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Deserted, Comments = comments, ID = currInst.Id }, "State", "Comments");
                    func(new WorkflowPlan { Actor = null, PlanState = WorkflowNodeInstance.StepStateType.Deserted.ToString(), PlanID = planId });
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;

                }
            }
            return result;
        }
        public List<WorkflowNodeInstance> GetAllNodeInstance(int planId, int twfid)
        {
            ActualSteps _instance = Find(a => a.PlanID == planId && a.PrevID == Guid.Empty && a.TWFID == twfid);
            

            //将流程节点进行排序
            List<WorkflowNodeInstance> orderInstList = new List<WorkflowNodeInstance>();
            if (_instance != null)
            {
                
                WorkflowNodeInstance wfInst = ExecReader(_instance);
                orderInstList.Add(wfInst);
                int count = 0;
                WorkflowNodeInstance tempInst = GetNodeInstance(wfInst.NextId);
                while (true)
                {
                    orderInstList.Add(tempInst);
                    if (tempInst.NextId == Guid.Empty)
                    {
                        break;
                    }
                    else
                    {
                        tempInst = GetNodeInstance(tempInst.NextId);
                        count++;
                    }
                }
            }
            return orderInstList;
        }
        public WorkflowNodeInstance GetNodeInstance(Guid id)
        {
            ActualSteps _instance = Find(a => a.ID == id);
            return ExecReader(_instance);
        }

        private WorkflowNodeInstance ExecReader(ActualSteps entity)
        {
            WorkflowNodeInstance ninst = new WorkflowNodeInstance();
                ninst.Id = entity.ID;
                ninst.State = (WorkflowNodeInstance.StepStateType)entity.State;
            ninst.PlanID = entity.PlanID;
            ninst.StepId =entity.StepID;
            ninst.TWFID = entity.TWFID;
            if (entity.PrevID.HasValue)
                ninst.PrevId = entity.PrevID.Value;
            if (entity.NextID.HasValue)
                ninst.NextId = entity.NextID.Value;
            if (entity.ActorID.HasValue)
                ninst.ActorID = entity.ActorID.Value;
                ninst.ActorName =entity.ActorName??"";
            if (entity.ActorTime.HasValue)
                ninst.ActorTime = entity.ActorTime.Value;
                ninst.Comments =entity.Comments??"";
            if (entity.ApplyTime.HasValue)
                ninst.ApplyTime =entity.ApplyTime.Value;
            return ninst;
        }

        /// <summary>
        /// 删除流程实例，20171213 modified by seczhou
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="twfid"></param>
        /// <returns></returns>
        public int DeleteActualSteps(int planId, int twfid)
        {
            return BatchDelete(a => a.PlanID == planId && a.TWFID == twfid);
        }
    }
}
