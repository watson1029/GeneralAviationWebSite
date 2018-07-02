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
        public bool UpdateComment(Guid id, string auditComment)
        {
            try
            {
                var entity = new ActualSteps { ID = id, Comments = auditComment };
                return Update(entity, "Comments") > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public WorkflowNodeInstance Submit(Guid planId, int twfid, int userID, string userName,string roleName, string comments, Action<WorkflowPlan> action, string controlDepList=null)
        {
            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId, twfid);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            WorkflowNodeInstance nextInst = null;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (currInst.IsParallel.HasValue && currInst.IsParallel.Value)
                    {       
                        //根据登陆角色获取流程实例
                        var entity = context.SubActualSteps.FirstOrDefault(u => u.ParentStepID == currInst.Id.ToString() && roleName.Contains(u.ActorName) && u.State == (int)WorkflowNodeInstance.StepStateType.Processing);
                        if (entity != null)
                        {
                            entity.State = (byte)WorkflowNodeInstance.StepStateType.Processed;
                            entity.Comments = comments;
                            entity.ActorID = userID;
                            entity.ActorName = userName;
                            entity.ActorTime = DateTime.Now;
                            context.SaveChanges();
                            if (context.SubActualSteps.Count(u => u.ParentStepID == currInst.Id.ToString() && u.State == (int)WorkflowNodeInstance.StepStateType.Processing) != 0)
                            {
                                var repplan = context.RepetitivePlan.Find(planId);
                                if (repplan != null)
                                {
                                    var list = new List<string>(repplan.ActorName.Split(','));
                                    list.Remove(roleName);
                                    repplan.ActorName = string.Join(",", list);
                                    context.SaveChanges();
                                }
                                dbContextTransaction.Commit();
                                return null;
                            }
                         }
                        else
                        {
                            throw new Exception("找不到流程实例！");
                        }
                    }
                        Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Processed, Comments = comments, ActorID = userID, ActorName = userName, ActorTime = DateTime.Now, ID = currInst.Id }, "State", "ActorID", "ActorName", "Comments", "ActorTime");
                        currInst.State = WorkflowNodeInstance.StepStateType.Processed;
                    //更新下一个节点状态为处理中
                    if (currInst.NextId != Guid.Empty)
                    {
                        nextInst = GetNodeInstance(currInst.NextId);
                        WorkflowTplNode tnode = _dal.GetNode(nextInst.StepId);
                        #region 如果是并行流程
                        if (tnode.IsParallel.HasValue && tnode.IsParallel.Value)
                        {
                            if (controlDepList != null && !string.IsNullOrEmpty(controlDepList))
                            {
                                var controlDepArray = controlDepList.Split(',');
                                var list = context.SubActualSteps.Where(u => u.ParentStepID == currInst.NextId.ToString());
                                foreach (var item in list)
                                {
                                    if (controlDepArray.Contains(item.ActorName))
                                    {
                                        item.State = (byte)WorkflowNodeInstance.StepStateType.Processing;
                                        item.ApplyTime = DateTime.Now;

                                    }
                                    else
                                    {
                                        item.State = (byte)WorkflowNodeInstance.StepStateType.NoValid;
                                    }
                                }
                                tnode.AuthorType = controlDepList;
                                context.SaveChanges();
                            }
                            else   //将下个节点全部设为无效
                            {

                                var list = context.SubActualSteps.Where(u => u.ParentStepID == currInst.NextId.ToString());
                                foreach (var item in list)
                                {
                                    item.State = (byte)WorkflowNodeInstance.StepStateType.NoValid;
                                }
                                    context.SaveChanges();
                                
                                Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.NoValid, ID = currInst.NextId }, "State");
                                action(new WorkflowPlan { Actor = null, ActorName = null, PlanState = "end", PlanID = planId });

                                dbContextTransaction.Commit();
                                return null;
                            }
                        }
                            #endregion

                            //判断节点的活动所有者类型
                            Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Processing, ApplyTime = DateTime.Now, ActorName = tnode.AuthorType, ID = currInst.NextId }, "State", "ApplyTime", "ActorID", "ActorName");
                            action(new WorkflowPlan { ActorName = tnode.AuthorType, PlanState = tnode.AuthorType, PlanID = planId });

                            nextInst = GetNodeInstance(currInst.NextId);
                        }
                        else
                        {
                            action(new WorkflowPlan { Actor = null, ActorName = null, PlanState = "end", PlanID = planId });
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
            var entity = new RepetitivePlan() { ActorName = plan.ActorName, PlanState = plan.PlanState, RepetPlanID = plan.PlanID };
            var entry = context.Entry(entity);
            entry.State = EntityState.Unchanged;
            foreach (string propertyName in new string[] {  "ActorName", "PlanState" })
            {
                entry.Property(propertyName).IsModified = true;
            }
            context.Configuration.ValidateOnSaveEnabled = false;
            context.SaveChanges();
        }
        public void UpdateFlightPlan(WorkflowPlan plan)
        {
            var entity = new Model.EF.FlightPlan() { ActorName = plan.ActorName, PlanState = plan.PlanState, FlightPlanID = plan.PlanID };

            var entry = context.Entry(entity);
            entry.State = EntityState.Unchanged;
            foreach (string propertyName in new string[] { "ActorName", "PlanState" })
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
        public int Terminate(Guid planId, int twfid, int userID, string userName,string roleName, string comments, Action<WorkflowPlan> func)
        {
            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId, twfid);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            int result = 0;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (currInst.IsParallel.HasValue && currInst.IsParallel.Value)
                    {

                        //根据登陆角色获取流程实例
                        var entity = context.SubActualSteps.FirstOrDefault(u => u.ParentStepID == currInst.Id.ToString() && roleName.Contains(u.ActorName) && u.State == (int)WorkflowNodeInstance.StepStateType.Processing);
                        if (entity != null)
                        {
                            entity.State = (byte)WorkflowNodeInstance.StepStateType.Deserted;
                            entity.Comments = comments;
                            entity.ActorID = userID;
                            entity.ActorTime = DateTime.Now;
                            context.SaveChanges();          
                        }
                        else
                        {
                            throw new Exception("找不到流程实例！");
                        }
                    }

                    Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Deserted, ActorID = userID, ActorName = userName, ActorTime = DateTime.Now, Comments = comments, ID = currInst.Id }, "State", "ActorID", "ActorName", "ActorTime", "Comments");
                    func(new WorkflowPlan { Actor = null, ActorName = null, PlanState = WorkflowNodeInstance.StepStateType.Deserted.ToString(), PlanID = planId });
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
        public List<WorkflowNodeInstance> GetAllNodeInstance(Guid planId, int twfid)
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
        public ActualSteps GetNodeInstance(int actorID, int twfID, Guid planID)
        {
            ActualSteps _instance = Find(a => a.ActorID == actorID && a.TWFID == twfID && a.PlanID == planID);
            return _instance;
        }
        private WorkflowNodeInstance ExecReader(ActualSteps entity)
        {
            WorkflowNodeInstance ninst = new WorkflowNodeInstance();
            ninst.Id = entity.ID;
            ninst.State = (WorkflowNodeInstance.StepStateType)entity.State;
            ninst.PlanID = entity.PlanID;
            ninst.StepId = entity.StepID;
            ninst.TWFID = entity.TWFID;
            if (entity.PrevID.HasValue)
                ninst.PrevId = entity.PrevID.Value;
            if (entity.NextID.HasValue)
                ninst.NextId = entity.NextID.Value;
            if (entity.ActorID.HasValue)
                ninst.ActorID = entity.ActorID.Value;
            ninst.ActorName = entity.ActorName ?? "";
            if (entity.ActorTime.HasValue)
                ninst.ActorTime = entity.ActorTime.Value;
            ninst.Comments = entity.Comments ?? "";
            if (entity.ApplyTime.HasValue)
                ninst.ApplyTime = entity.ApplyTime.Value;
            if (entity.IsParallel.HasValue)
            {
                ninst.IsParallel = entity.IsParallel.Value;
                if(ninst.IsParallel.Value)
                {
                    ninst.SubActualStepsList = context.SubActualSteps.Where(u=>u.ParentStepID== entity.ID.ToString()&&u.State!=4 && u.State != 0).ToList();
                }
            }
            return ninst;
        }

        /// <summary>
        /// 删除流程实例，20171213 modified by seczhou
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="twfid"></param>
        /// <returns></returns>
        public int DeleteActualSteps(Guid planId, int twfid)
        {
            return BatchDelete(a => a.PlanID == planId && a.TWFID == twfid);
        }
    }
}
