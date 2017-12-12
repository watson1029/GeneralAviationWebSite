using DAL.SystemManagement;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untity.DB;
using Model.EF;

namespace DAL.FlightPlan
{
    public class WorkflowNodeInstanceDAL
    {
        private DBHelper<ActualSteps> dbHelper = new DBHelper<ActualSteps>();
 

        public bool UpdateNodeInstance(List<WorkflowNodeInstance> nodesInstance)
        {
            //SqlDbHelper dao = new SqlDbHelper();
            //try
            //{
            //    dao.BeginTran();
            //    var sql = "update ActualSteps set PrevID=@prevId, NextID=@nextId where ID=@id";
            //    foreach (var tempInst in nodesInstance)
            //    {
            //        SqlParameter[] parameters = {
            //        new SqlParameter("@prevId", tempInst.PrevId),
            //        new SqlParameter("@nextId", tempInst.NextId),
            //         new SqlParameter("@id", tempInst.Id)};
            //        dao.ExecNonQuery(sql, parameters);
            //    }
            //    dao.CommitTran();
            //    return true;

            //}
            //catch (Exception e)
            //{
            //    dao.RollBackTran();
            //    throw (e);

            //}

            try
            {
                foreach (var tempINst in nodesInstance)
                {
                    dbHelper.Update(new ActualSteps { PrevID = tempINst.PrevId, NextID = tempINst.NextId, ID = tempINst.Id }, "PrevID", "NextID");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateFirstNode(WorkflowNodeInstance firstNodeInst, int userID, string userName)
        {
            //SqlDbHelper dao = new SqlDbHelper();
            //firstNodeInst.State = WorkflowNodeInstance.StepStateType.Processing;
            //var sql = "update ActualSteps set State=@state, ActorID=@actID, ActorName=@actName, ApplyTime=@applyTime where ID=@id";

            //SqlParameter[] parameters = {
            //        new SqlParameter("@actID", userID),
            //        new SqlParameter("@actName", userName),
            //         new SqlParameter("@state", firstNodeInst.State),
            //        new SqlParameter("@applyTime",DateTime.Now),
            //         new SqlParameter("@id", firstNodeInst.Id)};
            //return dao.ExecNonQuery(sql, parameters) > 0;

            try
            {
                firstNodeInst.State = WorkflowNodeInstance.StepStateType.Processing;
                var entity = new ActualSteps { ActorID = userID, ActorName = userName, State = (byte)firstNodeInst.State, ApplyTime = DateTime.Now, ID = firstNodeInst.Id };
                return dbHelper.Update(entity, "ActorID", "ActorName", "State", "ApplyTime") > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkflowNodeInstance Submit(int planId, int twfid, string comments, Action<WorkflowPlan> action)
        {
            #region 没用EF
            /*
            SqlDbHelper dao = new SqlDbHelper();
            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            WorkflowNodeInstance nextInst = null;
            var sql = "update ActualSteps set State=@state, Comments=@comment, ActorTime=@actTime where ID=@id";

            try
            {
                dao.BeginTran();
                //更新当前节点状态为完成
                SqlParameter[] parameters = {
                    new SqlParameter("@state", (byte)WorkflowNodeInstance.StepStateType.Processed),
                    new SqlParameter("@comment", comments),
                    new SqlParameter("@actTime", DateTime.Now),
                    new SqlParameter("@id", currInst.Id)};
                int result = dao.ExecNonQuery(sql, parameters);
                if (result > 0)
                {
                    currInst.State = WorkflowNodeInstance.StepStateType.Processed;
                    //更新下一个节点状态为处理中
                    if (currInst.NextId != Guid.Empty)
                    {
                        nextInst = GetNodeInstance(currInst.NextId);
                        WorkflowTplNode tnode = WorkflowTplNodeDAL.GetNode(nextInst.StepId);
                        sql = "update ActualSteps set State=@state, ActorID=@gid, ActorName=@actName, ApplyTime=@applyTime where ID=@id";


<<<<<<< HEAD
                        var userInfo = UserInfoDAL.Get(int.Parse(tnode.AuthorType));
                        int actor = userInfo.ID;
=======
                        var userInfo = new UserInfoDAL().Find(m=>m.ID==int.Parse(tnode.AuthorType));
                        int? actor = null;
>>>>>>> 24d7ba9c6f493d76125a74f83a1b23461ab56ef2
                        //判断节点的活动所有者类型

                        SqlParameter[] parameters1 = {
					        new SqlParameter("@state", (byte)WorkflowNodeInstance.StepStateType.Processing),
					        new SqlParameter("@applyTime", DateTime.Now),
                            new SqlParameter("@id", currInst.NextId),
                            new SqlParameter("@gid", userInfo.ID),
                            new SqlParameter("@actName", userInfo.UserName) };

                        result = dao.ExecNonQuery(sql, parameters1);

                        //更新申请单的状态
                        //sql = "update RepetitivePlan set ActorID=@actor, PlanState=@planState where RepetPlanID=@planId";
                        //SqlParameter[] parameters3 = {
                        //    new SqlParameter("@planState",tnode.StepName),
                        //    new SqlParameter("@actor", actor),
                        //    new SqlParameter("@planId", planId)};
                        //dao.ExecNonQuery(sql, parameters3);
                        //调用委托
                        action(new WorkflowPlan { Actor = actor, PlanState = tnode.StepName, PlanID= planId });
                    }
                    else
                    {

                        //已经是最后一个节点了，流程结束
                        //更新申请单的状态
                        //sql = "update RepetitivePlan set ActorID=@actor, PlanState=@planState where RepetPlanID=@planId";
                        //SqlParameter[] parameters2 = {
                        //    new SqlParameter("@actor", DBNull.Value),
                        //    new SqlParameter("@planState", "end"),
                        //    new SqlParameter("@planId", planId)};
                        //dao.ExecNonQuery(sql, parameters2);
                        action(new WorkflowPlan { Actor = null, PlanState = "end", PlanID = planId });
                    }
                }
                dao.CommitTran();
            }
            catch (Exception)
            {
                dao.RollBackTran();
                nextInst = null;
            }**/
            #endregion
            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId, twfid);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            WorkflowNodeInstance nextInst = null;

            try
            {
                //更新当前节点状态为完成
                int result = dbHelper.Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Processed, Comments = comments, ActorTime = DateTime.Now, ID = currInst.Id, TWFID = twfid }, "State", "Comments", "ActorTime", "TWFID");

                if (result > 0)
                {
                    currInst.State = WorkflowNodeInstance.StepStateType.Processed;
                    //更新下一个节点状态为处理中
                    if (currInst.NextId != Guid.Empty)
                    {
                        nextInst = GetNodeInstance(currInst.NextId);
                        WorkflowTplNode tnode = WorkflowTplNodeDAL.GetNode(nextInst.StepId);
                        var auhtor = int.Parse(tnode.AuthorType);
                        var userInfo = new UserInfoDAL().Find(u => u.ID == auhtor); //UserInfoDAL.Get(int.Parse(tnode.AuthorType));
                        int actor = userInfo.ID;
                        //判断节点的活动所有者类型

                        result = dbHelper.Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Processing, ApplyTime = DateTime.Now, ActorID = userInfo.ID, ActorName = userInfo.UserName, ID = currInst.NextId }, "State", "ApplyTime", "ActorID", "ActorName");
                        action(new WorkflowPlan { Actor = actor, PlanState = tnode.StepName, PlanID = planId, TWFID = twfid });
                    }
                    else
                    {
                        action(new WorkflowPlan { Actor = null, PlanState = "end", PlanID = planId, TWFID = twfid });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nextInst;
        }

        public void UpdateRepetPlan(WorkflowPlan plan)
        {
            var model = new RepetitivePlan() { ActorID = plan.Actor, PlanState = plan.PlanState, RepetPlanID = plan.PlanID };
            new DBHelper<RepetitivePlan>().Update(model, "ActorID", "PlanState");
        }
        public void UpdateFlightPlan(WorkflowPlan plan)
        {
            var model = new Model.EF.FlightPlan() { ActorID = plan.Actor, PlanState = plan.PlanState, FlightPlanID = plan.PlanID };
            new DBHelper<Model.EF.FlightPlan>().Update(model, "ActorID", "PlanState");
        }

        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        public int Terminate(int planId, int twfid, string comments, Action<WorkflowPlan> func)
        {
            #region 没用ef
            /*
            SqlDbHelper dao = new SqlDbHelper();
            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            var sql = "update ActualSteps set State=@state, Comments=@comments where ID=@id";
            int result = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@state",  (byte)WorkflowNodeInstance.StepStateType.Deserted),
					new SqlParameter("@comments", comments),
                     new SqlParameter("@id",  currInst.Id)};
               result= dao.ExecNonQuery(sql, parameters) ;
                if (result > 0)
                {
                    sql = "update RepetitivePlan set ActorID=@actor, PlanState=@planstate where RepetPlanID=@planid";

                    SqlParameter[] parameters1 = {
					new SqlParameter("@actor",   DBNull.Value),
					new SqlParameter("@planstate", WorkflowNodeInstance.StepStateType.Deserted.ToString()),
                     new SqlParameter("@planid",  planId)};
                    result += dao.ExecNonQuery(sql, parameters1);
                }**/
            #endregion

            List<WorkflowNodeInstance> ninstList = GetAllNodeInstance(planId, twfid);
            var currInst = ninstList.First(item => item.State == WorkflowNodeInstance.StepStateType.Processing);
            int result = 0;
            result = dbHelper.Update(new ActualSteps { State = (byte)WorkflowNodeInstance.StepStateType.Deserted, Comments = comments, ID = currInst.Id }, "State", "Comments");
            if (result > 0)
            {
                func(new WorkflowPlan { Actor = null, PlanState = WorkflowNodeInstance.StepStateType.Deserted.ToString(), PlanID = planId, TWFID = twfid });
            }
            return result;
        }
        public List<WorkflowNodeInstance> GetAllNodeInstance(int planId, int twfid)
        {
            SqlDbHelper dao = new SqlDbHelper();
            var sql = "select * from ActualSteps where PlanID=@planId and PrevID=@prevId and TWFID=@twfid";
            SqlParameter[] parameters = {
					new SqlParameter("@planId",planId),
                    new SqlParameter("@prevId",Guid.Empty),
                    new SqlParameter("@twfid",twfid),
			};
            WorkflowNodeInstance wfInst = dao.ExecSelectSingleCmd<WorkflowNodeInstance>(ExecReader, sql, parameters);
            //将流程节点进行排序
            List<WorkflowNodeInstance> orderInstList = new List<WorkflowNodeInstance>();
            if (wfInst != null)
            {
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
            SqlDbHelper dao = new SqlDbHelper();
            string sql = "select * from ActualSteps where ID=@id";

            SqlParameter[] parameters = {
					new SqlParameter("@id",id)
			};
            parameters[0].Value = id;
            return dao.ExecSelectSingleCmd<WorkflowNodeInstance>(ExecReader, sql, parameters);
        }

        private WorkflowNodeInstance ExecReader(SqlDataReader dr)
        {
            WorkflowNodeInstance ninst = new WorkflowNodeInstance();
            if (!dr["Id"].Equals(DBNull.Value))
                ninst.Id = new Guid(dr["Id"].ToString());
            if (!dr["State"].Equals(DBNull.Value))
                ninst.State = (WorkflowNodeInstance.StepStateType)Convert.ToByte(dr["State"]);
            ninst.PlanID = Convert.ToInt32(dr["PlanID"]);
            ninst.StepId = Convert.ToInt32(dr["StepId"]);
            ninst.TWFID = Convert.ToInt32(dr["TWFID"]);
            if (!dr["PrevID"].Equals(DBNull.Value))
                ninst.PrevId = new Guid(dr["PrevID"].ToString());
            if (!dr["NextID"].Equals(DBNull.Value))
                ninst.NextId = new Guid(dr["NextID"].ToString());
            if (!dr["ActorID"].Equals(DBNull.Value))
                ninst.ActorID = Convert.ToInt32(dr["ActorID"]);
            if (!dr["ActorName"].Equals(DBNull.Value))
                ninst.ActorName = Convert.ToString(dr["ActorName"]);
            if (!dr["ActorTime"].Equals(DBNull.Value))
                ninst.ActorTime = Convert.ToDateTime(dr["ActorTime"]);
            if (!dr["Comments"].Equals(DBNull.Value))
                ninst.Comments = Convert.ToString(dr["Comments"]);
            if (!dr["ApplyTime"].Equals(DBNull.Value))
                ninst.ApplyTime = Convert.ToDateTime(dr["ApplyTime"]);
            return ninst;
        }

    }
}
